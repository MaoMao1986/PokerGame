using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;

public static class PokerHandEvaluator
{


    public static (Enum_PokerHandType Type, Dictionary<Card, Card> MatchedCards) EvaluateHand(List<Card> p_Cards, List<Card> p_PublicCards = null)
    {
        List<Card> t_Cards = p_Cards;
        // 如果有公共牌，则将其与手牌合并
        if (p_PublicCards != null)
        {
            t_Cards = p_Cards.Concat(p_PublicCards).ToList();
        }

        // 将所有牌分为王牌和非王牌，并按大小排序，并按花色分类
        (List<Card> t_RegularCards, List<Card> t_JokerCards) = m_SplitAndSortCards(t_Cards);

        // 牌型判断逻辑
        Dictionary<Card, Card> t_MatchedCards;
        if (m_IsRoyalFlush(t_RegularCards, out t_MatchedCards, t_JokerCards)) return (Enum_PokerHandType.RoyalFlush, t_MatchedCards);
        if (m_IsStraightFlush(t_RegularCards, out t_MatchedCards, t_JokerCards)) return (Enum_PokerHandType.StraightFlush, t_MatchedCards);
        if (m_IsFourOfAKind(t_RegularCards, out t_MatchedCards, t_JokerCards)) return (Enum_PokerHandType.FourOfAKind, t_MatchedCards);
        if (m_IsFullHouse(t_RegularCards, out t_MatchedCards, t_JokerCards)) return (Enum_PokerHandType.FullHouse, t_MatchedCards);
        if (m_IsFlush(t_RegularCards, out t_MatchedCards, t_JokerCards)) return (Enum_PokerHandType.Flush, t_MatchedCards);
        if (m_IsStraight(t_RegularCards, out t_MatchedCards, t_JokerCards)) return (Enum_PokerHandType.Straight, t_MatchedCards);
        if (m_IsThreeOfAKind(t_RegularCards, out t_MatchedCards, t_JokerCards)) return (Enum_PokerHandType.ThreeOfAKind, t_MatchedCards);
        if (m_IsTwoPairs(t_RegularCards, out t_MatchedCards, t_JokerCards)) return (Enum_PokerHandType.TwoPairs, t_MatchedCards);
        if (m_IsOnePair(t_RegularCards, out t_MatchedCards, t_JokerCards)) return (Enum_PokerHandType.OnePair, t_MatchedCards);

        // 如果是高牌，则代表其中没有王牌（有王牌的情况下无论如何都不可能匹配到高牌）
        t_MatchedCards = new(){ { t_RegularCards[0], null } };
        return (Enum_PokerHandType.HighCard, t_MatchedCards);
    }

    /// <summary>
    /// 皇家同花顺
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <param name="p_TransCards"></param>
    /// <returns></returns>
    private static bool m_IsRoyalFlush(List<Card> p_Cards, out Dictionary<Card,Card> p_MatchedCards, List<Card> p_JokerCards = null)
    {
        bool t_Result = false;
        //  遍历每种目标
        List<List<Card>> t_TargetList = m_GetSingleStraightFlush(Enum_CardRank.Ace);
        foreach (var t_Target in t_TargetList)
        {
            (t_Result, p_MatchedCards) = m_CheckTarget(t_Target, p_Cards, 5 , p_JokerCards);
            if (t_Result){ return t_Result; }
        }
        p_MatchedCards = null;
        return t_Result;
    }

    /// <summary>
    /// 同花顺
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <returns></returns>
    private static bool m_IsStraightFlush(List<Card> p_Cards, out Dictionary<Card, Card> p_MatchedCards, List<Card> p_JokerCards = null)
    {
        bool t_Result = false;
        //  遍历每种目标
        List<List<Card>> t_TargetList = m_GetAllStraightFlush();
        foreach (var t_Target in t_TargetList)
        {
            (t_Result, p_MatchedCards) = m_CheckTarget(t_Target, p_Cards,5, p_JokerCards);
            if (t_Result) { return t_Result; }
        }
        p_MatchedCards = null;
        return t_Result;
    }

    /// <summary>
    /// 顺子
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <returns></returns>
    private static bool m_IsFlush(List<Card> p_Cards, out Dictionary<Card, Card> p_MatchedCards, List<Card> p_JokerCards = null)
    {
        return
    }

    /// <summary>
    /// 同花
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <returns></returns>
    private static bool m_IsStraight(List<Card> p_Cards, out Dictionary<Card, Card> p_MatchedCards, List<Card> p_JokerCards = null)
    {
        bool t_Result = false;
        //  遍历每种目标
        List<List<Card>> t_TargetList = m_GetAllSuit();
        foreach (var t_Target in t_TargetList)
        {
            (t_Result, p_MatchedCards) = m_CheckTarget(t_Target, p_Cards, 5, p_JokerCards);
            if (t_Result) { return t_Result; }
        }
        p_MatchedCards = null;
        return t_Result;
    }

    /// <summary>
    /// 四条
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <returns></returns>
    private static bool m_IsFourOfAKind(List<Card> p_Cards, out Dictionary<Card, Card> p_MatchedCards, List<Card> p_JokerCards = null)
    {
        bool t_Result = false;
        //  遍历每种目标
        List<List<Card>> t_TargetList = m_GetAllFour();
        foreach (var t_Target in t_TargetList)
        {
            (t_Result, p_MatchedCards) = m_CheckTarget(t_Target, p_Cards, 4, p_JokerCards);
            if (t_Result) { return t_Result; }
        }
        p_MatchedCards = null;
        return t_Result;
    }

    /// <summary>
    /// 葫芦，三带二
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <returns></returns>
    private static bool m_IsFullHouse(List<Card> p_Cards, out Dictionary<Card, Card> p_MatchedCards, List<Card> p_JokerCards = null)
    {
        var (regular, jokers) = m_SplitAndSortCards(p_Cards);
        var groups = regular.GroupBy(c => c.Rank).OrderByDescending(g => g.Count()).ToList();

        // Case 1: 3+2自然组合
        if (groups.Count >= 2 && groups[0].Count() >= 3 && groups[1].Count() >= 2)
            return true;

        // Case 2: 使用万能牌补全
        return (groups.Count > 0 && groups[0].Count() + jokers.Count >= 5);
    }

    /// <summary>
    /// 三条
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <returns></returns>
    private static bool m_IsThreeOfAKind(List<Card> p_Cards, out Dictionary<Card, Card> p_MatchedCards, List<Card> p_JokerCards = null)
    {
        bool t_Result = false;
        //  遍历每种目标
        List<List<Card>> t_TargetList = m_GetAllFour();
        foreach (var t_Target in t_TargetList)
        {
            (t_Result, p_MatchedCards) = m_CheckTarget(t_Target, p_Cards, 3, p_JokerCards);
            if (t_Result) { return t_Result; }
        }
        p_MatchedCards = null;
        return t_Result;
    }

    /// <summary>
    /// 两对
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <returns></returns>
    private static bool m_IsTwoPairs(List<Card> p_Cards, out Dictionary<Card, Card> p_MatchedCards, List<Card> p_JokerCards = null)
    {
        var (regular, jokers) = m_SplitAndSortCards(p_Cards);
        var pairs = regular.GroupBy(c => c.Rank).Count(g => g.Count() >= 2);
        return (pairs + jokers.Count) >= 2;
    }

    /// <summary>
    /// 对子
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <returns></returns>
    private static bool m_IsOnePair(List<Card> p_Cards, out Dictionary<Card, Card> p_MatchedCards, List<Card> p_JokerCards = null)
    {
        bool t_Result = false;
        //  遍历每种目标
        List<List<Card>> t_TargetList = m_GetAllFour();
        foreach (var t_Target in t_TargetList)
        {
            (t_Result, p_MatchedCards) = m_CheckTarget(t_Target, p_Cards, 2, p_JokerCards);
            if (t_Result) { return t_Result; }
        }
        p_MatchedCards = null;
        return t_Result;
    }


    /// <summary>
    /// 按照花色分类
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <returns></returns>
    private static Dictionary<Enum_CardSuit, List<Card>> m_ClassifySuitAndSortCards(List<Card> p_Cards)
    {
        return p_Cards
            .GroupBy(c => c.Suit)
            .ToDictionary(
                g => g.Key,
                g => g.OrderByDescending(c => c.Rank).ToList()
            );
    }

    /// <summary>
    /// 按照点数分类
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <returns></returns>
    private static Dictionary<Enum_CardRank, List<Card>> m_ClassifyRankAndSortCards(List<Card> p_Cards)
    {
        return p_Cards
            .GroupBy(c => c.Rank)
            .ToDictionary(
                g => g.Key,
                g => g.OrderByDescending(c => c.Suit).ToList()
            );
    }

    /// <summary>
    /// 将普通牌和王牌分离，然后将普通牌先按大小排序，再按花色排序
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <returns></returns>
    private static (List<Card> Regular, List<Card> Jokers) m_SplitAndSortCards(List<Card> p_Cards)
    {
        // 分离普通牌和王牌
        var t_Jokers = p_Cards.Where(c =>
            c.Rank == Enum_CardRank.BigJoker ||
            c.Rank == Enum_CardRank.LittleJoker).OrderByDescending(c => c.Rank)
            .ToList();

        var t_Regular = p_Cards.Except(t_Jokers)
            // 先按照大小排序
            .OrderByDescending(c => (int)c.Rank)
            // 再按花色排序（黑桃 > 红心 > 梅花 > 方块）
            .ThenByDescending(c => (int)c.Suit)
            .ToList();

        return (t_Regular, t_Jokers);
    }

    /// <summary>
    /// 判断目标牌型是否可以用现有牌凑齐，以及凑齐后王牌对应的牌
    /// </summary>
    /// <param name="p_Target"></param>
    /// <param name="p_Regulars"></param>
    /// <param name="p_Jokers"></param>
    /// <returns></returns>
    public static (bool Matched, Dictionary<Card, Card> ResultCards) m_CheckTarget(List<Card> p_Target, List<Card> p_Regulars, int p_Number,List<Card> p_Jokers = null)
    {
        bool t_Result = false;
        Dictionary<Card, Card> t_MatchedCards = new();
        List<Card> t_Target = p_Target.OrderByDescending(c => c.Rank).ThenByDescending(c => c.Suit).ToList();
        int t_JokerIndex = 0;
        foreach (var t_Card in t_Target)
        {
            List<Card> t_Temp = p_Regulars.Where(t => (t.Rank == t_Card.Rank) && (t.Suit == t_Card.Suit)).ToList();
            if (t_Temp.Count() > 0)
            {
                t_MatchedCards.Add(t_Temp[0], null);
                continue;
            }
            else
            {
                if (p_Jokers == null) { continue; }
                if (t_JokerIndex >= p_Jokers.Count()) { continue; }
                // 使用大王（替代任意牌），如果是小王，则替代10以下
                if ((p_Jokers[t_JokerIndex].Rank == Enum_CardRank.BigJoker) || (p_Jokers[t_JokerIndex].Rank == Enum_CardRank.LittleJoker && (int)t_Card.Rank <= 10))
                {
                    t_MatchedCards.Add(p_Jokers[t_JokerIndex], t_Card);
                    t_JokerIndex++;
                }
            }
        }
        if(t_MatchedCards.Count >= p_Number)
        {
            t_Result = true;
            // 如果匹配到的牌大于目标牌数，则删除多余的牌，由于是按大小排序，所以删除最后的牌
            for(int i=0;i<t_MatchedCards.Count-p_Number;i++)
            {
                t_MatchedCards.Remove(t_MatchedCards.Last().Key);
            }
        }
        return (t_Result, t_MatchedCards);
    }

    /// <summary>
    /// 判断目标牌型是否可以用现有牌凑齐，以及凑齐后王牌对应的牌
    /// </summary>
    /// <param name="p_Target"></param>
    /// <param name="p_Regulars"></param>
    /// <param name="p_Jokers"></param>
    /// <returns></returns>
    public static (bool Matched, Dictionary<Card, Card> ResultCards) m_CheckTarget(List<Enum_CardRank> p_Target, List<Card> p_Regulars, int p_Number, List<Card> p_Jokers = null)
    {
        bool t_Result = false;
        Dictionary<Card, Card> t_MatchedCards = new();
        List<Enum_CardRank> t_Target = p_Target.OrderByDescending(c => c).ToList();
        int t_JokerIndex = 0;
        foreach (var t_CardRank in t_Target)
        {
            List<Card> t_Temp = p_Regulars.Where(t => t.Rank == t_CardRank).ToList();
            if (t_Temp.Count() > 0)
            {
                t_MatchedCards.Add(t_Temp[0], null);
            }
            else
            {
                if (p_Jokers == null) { continue; }
                if (t_JokerIndex >= p_Jokers.Count()) { continue; }
                // 使用大王（替代任意牌），如果是小王，则替代10以下
                if ((p_Jokers[t_JokerIndex].Rank == Enum_CardRank.BigJoker) || (p_Jokers[t_JokerIndex].Rank == Enum_CardRank.LittleJoker && (int)t_CardRank <= 10))
                {
                    t_MatchedCards.Add(p_Jokers[t_JokerIndex], new Card(Enum_CardSuit.Spades, t_CardRank));
                    t_JokerIndex++;
                }
            }
        }
        if (t_MatchedCards.Count >= p_Number)
        {
            t_Result = true;
            // 如果匹配到的牌大于目标牌数，则删除多余的牌，由于是按大小排序，所以删除最后的牌
            for (int i = 0; i < t_MatchedCards.Count - p_Number; i++)
            {
                t_MatchedCards.Remove(t_MatchedCards.Last().Key);
            }
        }
        return (t_Result, t_MatchedCards);
    }

    #region 生成匹配库
    private static List<List<Card>> m_GetAllSuit()
    {
        List<List<Card>> t_Cards = new();
        for (int i = (int)Enum_CardSuit.Spades; i >= (int)Enum_CardSuit.Diamonds; i--)
        {
            var t_Suit = (Enum_CardSuit)i;
            var t_List = m_GetSingleSuit(t_Suit);
            t_Cards.Add(t_List);
        }
        return t_Cards;
    }

    private static List<Card> m_GetSingleSuit(Enum_CardSuit p_Suit)
    {
        List<Card> t_Cards = new();
        for (int i = (int)Enum_CardRank.Ace; i >= (int)Enum_CardRank.Two; i--)
        {
            var t_Rank = (Enum_CardRank)i;
            t_Cards.Add(new(p_Suit, t_Rank));
        }
        return t_Cards;
    }

    /// <summary>
    /// 获取所有四条的匹配库
    /// </summary>
    /// <returns></returns>
    private static List<List<Card>> m_GetAllFour()
    {
        var t_Cards = new List<List<Card>>();
        // 生成该花色所有可能的顺子组合
        for (int i = (int)Enum_CardRank.Ace; i >= (int)Enum_CardRank.Five; i--)
        {
            var t_List = m_GetSingleFour((Enum_CardRank)i);
            t_Cards.Add(t_List);
        }
        return t_Cards;
    }

    /// <summary>
    /// 获取单个四条的匹配库
    /// </summary>
    /// <param name="p_Rank"></param>
    /// <returns></returns>
    private static List<Card> m_GetSingleFour(Enum_CardRank p_Rank)
    {
        List<Card> t_Cards = new();
        for (int i = (int)Enum_CardSuit.Spades; i >= (int)Enum_CardSuit.Diamonds; i--)
        {
            var t_Suit = (Enum_CardSuit)i;
            t_Cards.Add(new(t_Suit, p_Rank));
        }
        return t_Cards;
    }

    /// <summary>
    /// 生成所有可能的同花顺组合
    /// </summary>
    /// <returns></returns>
    private static List<List<Card>> m_GetAllStraightFlush()
    {
        var t_Cards = new List<List<Card>>();
        // 生成该花色所有可能的顺子组合
        for(int i=(int)Enum_CardRank.Ace;i>= (int)Enum_CardRank.Five; i--)
        {
            var t_List = m_GetSingleStraightFlush((Enum_CardRank)i);
            t_Cards.AddRange(t_List);
        }
        return t_Cards;
    }

    /// <summary>
    /// 获取所有花色的单个点位的同花顺
    /// </summary>
    /// <param name="p_Rank"></param>
    /// <returns></returns>
    private static List<List<Card>> m_GetSingleStraightFlush(Enum_CardRank p_Rank)
    {
        var t_Cards = new List<List<Card>>();
        int t_Rank = (int)p_Rank;
        if(t_Rank >= (int)Enum_CardRank.Six && t_Rank <= (int)Enum_CardRank.Ace)
        {
            for (int i = (int)Enum_CardSuit.Spades; i >= (int)Enum_CardSuit.Diamonds; i--)
            {
                var t_List = new List<Card>();
                var t_Suit = (Enum_CardSuit)i;
                for (int j = 0; j < 5; j++)
                {
                    t_List.Add(new Card(t_Suit, (Enum_CardRank)(t_Rank - j)));
                }
                t_Cards.Add(t_List);
            }
        }
        else if(p_Rank == Enum_CardRank.Five)
        {
            for (int i = (int)Enum_CardSuit.Spades; i >= (int)Enum_CardSuit.Diamonds; i--)
            {
                var t_Suit = (Enum_CardSuit)i;
                t_Cards.Add(new List<Card> {
                    new(t_Suit, Enum_CardRank.Ace),
                    new(t_Suit, Enum_CardRank.Five),
                    new(t_Suit, Enum_CardRank.Four),
                    new(t_Suit, Enum_CardRank.Three),
                    new(t_Suit, Enum_CardRank.Two)
                });
            }
        }
        return t_Cards;
    }
    #endregion


}