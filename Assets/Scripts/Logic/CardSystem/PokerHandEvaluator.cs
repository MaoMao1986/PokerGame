using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;

public static class PokerHandEvaluator
{
    public static (Enum_PokerHandType Type, Dictionary<Card, Card> MatchedCards) EvaluateHand(List<Card> p_Cards, List<Card> p_PublicCards = null)
    {
        // 如果有公共牌，则将其与手牌合并
        List<Card> t_Cards = p_PublicCards == null ? p_Cards : p_Cards.Concat(p_PublicCards).ToList();

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
            m_MatchTargets<m_MatchTargetCard> t_TargetArray = m_MatchTargets<m_MatchTargetCard>.GetCardTargets(t_Target,5);
            (t_Result, p_MatchedCards) = m_CheckTarget(t_TargetArray, p_Cards , p_JokerCards);
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
            m_MatchTargets<m_MatchTargetCard> t_TargetArray = m_MatchTargets<m_MatchTargetCard>.GetCardTargets(t_Target,5);
            (t_Result, p_MatchedCards) = m_CheckTarget(t_TargetArray, p_Cards, p_JokerCards);
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
        bool t_Result = false;
        //  遍历每种目标
        List<List<Enum_CardRank>> t_TargetList = m_GetAllRank();
        foreach (var t_Target in t_TargetList)
        {
            m_MatchTargets<m_MatchTargetEnum> t_TargetArray = m_MatchTargets<m_MatchTargetEnum>.GetRankTargets(t_Target,5);
            (t_Result, p_MatchedCards) = m_CheckTarget(t_TargetArray, p_Cards, p_JokerCards);
            if (t_Result) { return t_Result; }
        }
        p_MatchedCards = null;
        return t_Result;
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
            m_MatchTargets<m_MatchTargetCard> t_TargetArray = m_MatchTargets<m_MatchTargetCard>.GetCardTargets(t_Target, 5);
            (t_Result, p_MatchedCards) = m_CheckTarget(t_TargetArray, p_Cards, p_JokerCards);
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
            m_MatchTargets<m_MatchTargetCard> t_TargetArray = m_MatchTargets<m_MatchTargetCard>.GetCardTargets(t_Target, 4);
            (t_Result, p_MatchedCards) = m_CheckTarget(t_TargetArray, p_Cards, p_JokerCards);
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
        return m_IsTwoKind(p_Cards, 3, 2, out p_MatchedCards, p_JokerCards);
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
            m_MatchTargets<m_MatchTargetCard> t_TargetArray = m_MatchTargets<m_MatchTargetCard>.GetCardTargets(t_Target, 3);
            (t_Result, p_MatchedCards) = m_CheckTarget(t_TargetArray, p_Cards, p_JokerCards);
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
        return m_IsTwoKind(p_Cards, 2, 2, out p_MatchedCards, p_JokerCards);
    }

    private static bool m_IsTwoKind(List<Card> p_Cards,int p_Number1, int p_Number2, out Dictionary<Card, Card> p_MatchedCards, List<Card> p_JokerCards = null)
    {
        bool t_Result = false;
        //  将p_Cards按Card.Rank分类，然后转化成List<Enum_CardRank>，并按大小排序
        var t_Temps = m_ClassifyRankAndSortCards(p_Cards);
        var t_TargetRanks = t_Temps.Keys.OrderByDescending(c => (int)c).ToList();
        foreach (var t_Rank1 in t_TargetRanks)
        {
            foreach (var t_Rank2 in t_TargetRanks)
            {
                if (t_Rank1 == t_Rank2) { continue; }
                m_MatchTargets<m_MatchTargetCard> t_TargetArray = m_MatchTargets<m_MatchTargetEnum>.GetCardTargets2(t_Rank1, p_Number1, t_Rank2, p_Number2);
                (t_Result, p_MatchedCards) = m_CheckTarget(t_TargetArray, p_Cards, p_JokerCards);
                if (t_Result) { return t_Result; }
            }
        }

        p_MatchedCards = null;
        return t_Result;
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
            m_MatchTargets<m_MatchTargetCard> t_TargetArray = m_MatchTargets<m_MatchTargetCard>.GetCardTargets(t_Target, 2);
            (t_Result, p_MatchedCards) = m_CheckTarget(t_TargetArray, p_Cards, p_JokerCards);
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
        var t_Regular = p_Cards.Where(c =>c.Suit != Enum_CardSuit.Joker)
            .OrderByDescending(c => c.Rank)
            .ThenByDescending(c => (int)c.Suit)
            .ToList();

        var t_Jokers = p_Cards.Except(t_Regular)
            // 先按照大小排序
            .OrderByDescending(c => (int)c.Rank)
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
    private static (bool Matched, Dictionary<Card, Card> ResultCards) m_CheckTarget(m_MatchTargets<m_MatchTargetCard> p_Target, List<Card> p_Regulars, List<Card> p_Jokers = null)
    {
        bool t_Result = true;
        Dictionary<Card, Card> t_MatchedCards = new();
        int t_JokerIndex = 0;

        foreach (var t_List in p_Target.Targets)
        {
            List<Card> t_Target = t_List.TargetCards.OrderByDescending(c => c).ToList();
            Dictionary<Card, Card> t_SubMatchedCards = new();
            foreach (var t_Card in t_Target)
            {
                List<Card> t_Temp = p_Regulars.Where(t => (t.Rank == t_Card.Rank) && (t.Suit == t_Card.Suit)).ToList();
                if (t_Temp.Count() > 0)
                {
                    t_SubMatchedCards.Add(t_Temp[0], null);
                    continue;
                }
                else
                {
                    if (p_Jokers == null) { continue; }
                    if (t_JokerIndex >= p_Jokers.Count()) { continue; }
                    // 使用大王（替代任意牌），如果是小王，则替代10以下
                    if ((p_Jokers[t_JokerIndex].Rank == Enum_CardRank.BigJoker) || (p_Jokers[t_JokerIndex].Rank == Enum_CardRank.LittleJoker && (int)t_Card.Rank <= 10))
                    {
                        t_SubMatchedCards.Add(p_Jokers[t_JokerIndex], t_Card);
                        t_JokerIndex++;
                    }
                }
            }
            if (t_SubMatchedCards.Count >= t_List.MatchNumber)
            {
                // 如果匹配到的牌大于目标牌数，则删除多余的牌，由于是按大小排序，所以删除最后的牌
                for (int i = 0; i < t_SubMatchedCards.Count - t_List.MatchNumber; i++)
                {
                    t_SubMatchedCards.Remove(t_SubMatchedCards.Last().Key);
                }
            }
            else
            {
                t_Result = false;
                break;
            }
            t_MatchedCards.AddRange(t_SubMatchedCards);
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
    private static (bool Matched, Dictionary<Card, Card> ResultCards) m_CheckTarget(m_MatchTargets<m_MatchTargetEnum> p_Target, List<Card> p_Regulars, List<Card> p_Jokers = null)
    {
        bool t_Result = true;
        Dictionary<Card, Card> t_MatchedCards = new();
        
        int t_JokerIndex = 0;

        foreach(var t_List in p_Target.Targets)
        {
            List<Enum_CardRank> t_Target = t_List.TargetRanks.OrderByDescending(c => c).ToList();
            Dictionary<Card, Card> t_SubMatchedCards = new();
            foreach (var t_CardRank in t_Target)
            {
                List<Card> t_Temp = p_Regulars.Where(t => t.Rank == t_CardRank).ToList();
                if (t_Temp.Count() > 0)
                {
                    t_SubMatchedCards.Add(t_Temp[0], null);
                }
                else
                {
                    if (p_Jokers == null) { continue; }
                    if (t_JokerIndex >= p_Jokers.Count()) { continue; }
                    // 使用大王（替代任意牌），如果是小王，则替代10以下
                    if ((p_Jokers[t_JokerIndex].Rank == Enum_CardRank.BigJoker) || (p_Jokers[t_JokerIndex].Rank == Enum_CardRank.LittleJoker && (int)t_CardRank <= 10))
                    {
                        t_SubMatchedCards.Add(p_Jokers[t_JokerIndex], new Card(Enum_CardSuit.Spades, t_CardRank));
                        t_JokerIndex++;
                    }
                }
            }
            if (t_SubMatchedCards.Count >= t_List.MatchNumber)
            {
                // 如果匹配到的牌大于目标牌数，则删除多余的牌，由于是按大小排序，所以删除最后的牌
                for (int i = 0; i < t_SubMatchedCards.Count - t_List.MatchNumber; i++)
                {
                    t_SubMatchedCards.Remove(t_SubMatchedCards.Last().Key);
                }
            }
            else
            {
                t_Result = false;
                break;
            }
            t_MatchedCards.AddRange(t_SubMatchedCards);
        }

        return (t_Result, t_MatchedCards);
    }

    #region 生成匹配库
    /// <summary>
    /// 生成所有可能的同花顺组合
    /// </summary>
    /// <returns></returns>
    private static List<List<Enum_CardRank>> m_GetAllRank()
    {
        var t_Ranks = new List<List<Enum_CardRank>>();
        // 生成该花色所有可能的顺子组合
        for (int i = (int)Enum_CardRank.Ace; i >= (int)Enum_CardRank.Five; i--)
        {
            var t_List = m_GetSingleRank((Enum_CardRank)i);
            t_Ranks.Add(t_List);
        }
        return t_Ranks;
    }

    /// <summary>
    /// 获取所有花色的单个点位的同花顺
    /// </summary>
    /// <param name="p_Rank"></param>
    /// <returns></returns>
    private static List<Enum_CardRank> m_GetSingleRank(Enum_CardRank p_Rank)
    {
        var t_Ranks = new List<Enum_CardRank>();
        int t_Rank = (int)p_Rank;
        if (t_Rank >= (int)Enum_CardRank.Six && t_Rank <= (int)Enum_CardRank.Ace)
        {
            for (int j = 0; j < 5; j++)
            {
                t_Ranks.Add((Enum_CardRank)(t_Rank - j));
            }
        }
        else if (p_Rank == Enum_CardRank.Five)
        {
            t_Ranks.Add(Enum_CardRank.Ace);
            t_Ranks.Add(Enum_CardRank.Five);
            t_Ranks.Add(Enum_CardRank.Four);
            t_Ranks.Add(Enum_CardRank.Three);
            t_Ranks.Add(Enum_CardRank.Two);
        }
        return t_Ranks;
    }

    /// <summary>
    /// 获取所有花色的所有牌
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// 获取单个花色的所有牌
    /// </summary>
    /// <param name="p_Suit"></param>
    /// <returns></returns>
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
            for (int i = (int)Enum_CardSuit.Spades ; i >= (int)Enum_CardSuit.Diamonds; i--)
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

    #region 匹配结构
    private abstract class m_MatchTarget
    {
        public int MatchNumber;
    }

    /// <summary>
    /// 单一匹配目标
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private class m_MatchTargetCard : m_MatchTarget 
    {
        public List<Card> TargetCards { get; set; }
    }

    private class m_MatchTargetEnum : m_MatchTarget
    {
        public List<Enum_CardRank> TargetRanks { get; set; }
    }

    /// <summary>
    /// 匹配目标集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private class m_MatchTargets<T> where T : m_MatchTarget
    {
        public List<T> Targets { get; set; } = new List<T>();

        public static m_MatchTargets<m_MatchTargetCard> GetCardTargets(List<Card> p_Cards, int p_Number)
        {
            m_MatchTargets<m_MatchTargetCard> t_Targets = new m_MatchTargets<m_MatchTargetCard>();
            m_MatchTargetCard t_Target = new m_MatchTargetCard
            {
                TargetCards = p_Cards,
                MatchNumber = p_Number
            };
            t_Targets.Targets.Add(t_Target);
            return t_Targets;
        }

        public static m_MatchTargets<m_MatchTargetEnum> GetRankTargets(List<Enum_CardRank> p_Ranks, int p_Number)
        {
            m_MatchTargets<m_MatchTargetEnum> t_Targets = new m_MatchTargets<m_MatchTargetEnum>();
            m_MatchTargetEnum t_Target = new m_MatchTargetEnum
            {
                TargetRanks = p_Ranks,
                MatchNumber = p_Number
            };
            t_Targets.Targets.Add(t_Target);
            return t_Targets;
        }

        public static m_MatchTargets<m_MatchTargetCard> GetCardTargets2(Enum_CardRank p_Rank1, int p_Number1, Enum_CardRank p_Rank2, int p_Number2)
        {
            m_MatchTargetCard t_Target1 = new m_MatchTargetCard
            {
                TargetCards = m_GetSingleFour(p_Rank1),
                MatchNumber = p_Number1
            };
            m_MatchTargetCard t_Target2 = new m_MatchTargetCard
            {
                TargetCards = m_GetSingleFour(p_Rank2),
                MatchNumber = p_Number2
            };
            m_MatchTargets<m_MatchTargetCard> t_Targets = new m_MatchTargets<m_MatchTargetCard>
            {
                Targets = new List<m_MatchTargetCard>() { t_Target1, t_Target2 }
            };
            return t_Targets;
        }
    }

    #endregion

}