using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public static class PokerHandEvaluator
{
    public static Enum_PokerHandType EvaluateHand(List<Card> p_Cards, List<Card> p_PublicCards = null)
    {
        List<Card> t_Cards = p_Cards ?? new List<Card>();
        if (p_PublicCards != null)
        {
            t_Cards.AddRange(p_PublicCards);
        }

        var t_TortedCards = t_Cards.OrderBy(c => c.Rank).ToList();
        Dictionary<Card, Card> t_CardPairs;
        if (m_IsRoyalFlush(t_TortedCards, out t_CardPairs)) return Enum_PokerHandType.RoyalFlush;
        if (m_IsStraightFlush(t_TortedCards)) return Enum_PokerHandType.StraightFlush;
        if (m_IsFourOfAKind(t_TortedCards)) return Enum_PokerHandType.FourOfAKind;
        if (m_IsFullHouse(t_TortedCards)) return Enum_PokerHandType.FullHouse;
        if (m_IsFlush(t_TortedCards)) return Enum_PokerHandType.Flush;
        if (m_IsStraight(t_TortedCards)) return Enum_PokerHandType.Straight;
        if (m_IsThreeOfAKind(t_TortedCards)) return Enum_PokerHandType.ThreeOfAKind;
        if (m_IsTwoPairs(t_TortedCards)) return Enum_PokerHandType.TwoPairs;
        if (m_IsOnePair(t_TortedCards)) return Enum_PokerHandType.OnePair;

        return Enum_PokerHandType.HighCard;
    }

    /// <summary>
    /// 皇家同花顺
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <param name="p_CardPiars"></param>
    /// <returns></returns>
    private static bool m_IsRoyalFlush(List<Card> p_Cards, out Dictionary<Card,Card> p_CardPiars)
    {
        bool t_Result = false;
        p_CardPiars = null;
        Dictionary<Card, Card> t_CardPairs = new();
        (List<Card> t_RegularCards, List<Card> t_JokerCards) = m_SplitAndSortCards(p_Cards);
        
        //  遍历每种目标
        List<List<Card>> t_TargetList = m_GetSingleStraightFlushCombinations(Enum_CardRank.Ace);
        foreach(var t_Target in t_TargetList)
        {
            (t_Result, t_CardPairs) = m_CheckTargetCombination(t_Target, t_RegularCards, t_JokerCards);
            if (t_Result)
            {
                p_CardPiars = t_CardPairs;
                return t_Result;
            }
        }
        return t_Result;
    }

    /// <summary>
    /// 同花顺
    /// </summary>
    /// <param name="p_Cards"></param>
    /// <returns></returns>
    private static bool m_IsStraightFlush(List<Card> p_Cards)
    {
        return m_IsFlush(p_Cards) && m_IsStraight(p_Cards);
    }

    private static bool m_IsFlush(List<Card> p_Cards)
    {
        return p_Cards.GroupBy(c => c.Suit).Count() == 1;
    }

    private static bool m_IsStraight(List<Card> p_Cards)
    {
        // 分离普通牌和王牌
        var (t_RegularCards, t_Jokers) = m_SplitAndSortCards(p_Cards);
        var t_Ranks = t_RegularCards.Select(c => (int)c.Rank).OrderBy(r => r).ToList();
    
        // 检查所有可能的顺子模式
        int t_Missing = 5 - t_RegularCards.Count;
        if (t_Jokers.Count < t_Missing) return false;
    
        // 生成可能的顺子组合
        List<List<int>> t_PossibleStraights = new() {
            new List<int> {1,2,3,4,5}, // A-2-3-4-5
            new List<int> {10,11,12,13,1} // 10-J-Q-K-A
        };
        for (int i=2; i<=9; i++) {
            t_PossibleStraights.Add(Enumerable.Range(i,5).ToList());
        }
    
        // 检查每个可能的顺子模式
        foreach (var straight in t_PossibleStraights) {
            int t_Gap = straight.Count(r => !t_Ranks.Contains(r));
            if (t_Gap <= t_Jokers.Count) {
                return true;
            }
        }
        return false;
    }

    

    private static bool m_IsFourOfAKind(List<Card> p_Cards)
    {
        var (regular, jokers) = m_SplitAndSortCards(p_Cards);
        var groups = regular.GroupBy(c => c.Rank);
        return groups.Any(g => g.Count() + jokers.Count >= 4);
    }

    private static bool m_IsFullHouse(List<Card> p_Cards)
    {
        var (regular, jokers) = m_SplitAndSortCards(p_Cards);
        var groups = regular.GroupBy(c => c.Rank).OrderByDescending(g => g.Count()).ToList();
        
        // Case 1: 3+2自然组合
        if (groups.Count >= 2 && groups[0].Count() >=3 && groups[1].Count() >=2)
            return true;
            
        // Case 2: 使用万能牌补全
        return (groups.Count > 0 && groups[0].Count() + jokers.Count >= 5);
    }

    private static bool m_IsThreeOfAKind(List<Card> p_Cards)
    {
        var (regular, jokers) = m_SplitAndSortCards(p_Cards);
        return regular.GroupBy(c => c.Rank).Any(g => g.Count() + jokers.Count >= 3);
    }

    private static bool m_IsTwoPairs(List<Card> p_Cards)
    {
        var (regular, jokers) = m_SplitAndSortCards(p_Cards);
        var pairs = regular.GroupBy(c => c.Rank).Count(g => g.Count() >= 2);
        return (pairs + jokers.Count) >= 2;
    }

    private static bool m_IsOnePair(List<Card> p_Cards)
    {
        var (regular, jokers) = m_SplitAndSortCards(p_Cards);
        return regular.GroupBy(c => c.Rank).Any(g => g.Count() + jokers.Count >= 2);
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
            c.Rank == Enum_CardRank.LittleJoker).OrderBy(c => c.Rank)
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
    /// 查找凑齐牌型缺失的牌
    /// </summary>
    /// <param name="targetCombination"></param>
    /// <param name="currentCards"></param>
    /// <returns></returns>
    public static List<Card> m_FindMissingCards(List<Card> targetCombination, List<Card> currentCards)
    {
        // 创建哈希表加速查找（考虑花色和点数）
        var currentSet = new HashSet<(Enum_CardSuit, Enum_CardRank)>(
            currentCards.Select(c => (c.Suit, c.Rank))
        );

        return targetCombination
            .Where(t => !currentSet.Contains((t.Suit, t.Rank)))
            .OrderByDescending(c => c.Rank)  // 按点数降序
            .ThenByDescending(c => c.Suit)   // 同点数按花色排序
            .ToList();
    }

    /// <summary>
    /// 判断目标牌型是否可以用现有牌凑齐，以及凑齐后王牌对应的牌
    /// </summary>
    /// <param name="p_TargetCombination"></param>
    /// <param name="p_RegularCards"></param>
    /// <param name="p_Jokers"></param>
    /// <returns></returns>
    public static (bool Matched, Dictionary<Card,Card> WildcardMatches) m_CheckTargetCombination(
    List<Card> p_TargetCombination,
    List<Card> p_RegularCards,
    List<Card> p_Jokers)
    {
        var t_Missing = m_FindMissingCards(p_TargetCombination, p_RegularCards);
        var t_Replacements = new Dictionary<Card,Card>();

        // 分离大小王
        var t_BigJokers = p_Jokers.Where(j => j.Rank == Enum_CardRank.BigJoker).ToList();
        var t_LittleJokers = p_Jokers.Where(j => j.Rank == Enum_CardRank.LittleJoker).ToList();

        foreach (var t_Card in t_Missing)
        {
            // 优先使用大王（可替代任何牌）
            if (t_BigJokers.Any())
            {
                t_Replacements.Add(t_BigJokers[0], new Card(t_Card.Suit, t_Card.Rank));
                t_BigJokers.RemoveAt(0);
                continue;
            }

            // 使用小王（仅替代10及以下）
            if (t_LittleJokers.Any() && (int)t_Card.Rank <= 10)
            {
                t_Replacements.Add(t_LittleJokers[0], new Card(t_Card.Suit, t_Card.Rank));
                t_LittleJokers.RemoveAt(0);
                continue;
            }

            return (false, null); // 无法匹配
        }

        return (t_Missing.Count <= p_Jokers.Count, t_Replacements);
    }

    /// <summary>
    /// 生成所有可能的同花顺组合
    /// </summary>
    /// <returns></returns>
    private static List<List<Card>> m_GetAllStraightFlushCombinations()
    {
        var t_Combinations = new List<List<Card>>();

        // 生成该花色所有可能的顺子组合
        var t_Ranks = new[] 
        {
            Enum_CardRank.Ace,
            Enum_CardRank.King,
            Enum_CardRank.Queen,
            Enum_CardRank.Jack,
            Enum_CardRank.Ten,
            Enum_CardRank.Nine,
            Enum_CardRank.Eight,
            Enum_CardRank.Seven,
            Enum_CardRank.Six,
            Enum_CardRank.Five
        };

        foreach (var t_Rank in t_Ranks)
        {
            var t_List = m_GetSingleStraightFlushCombinations(t_Rank);
            t_Combinations.AddRange(t_List);
        }

        return t_Combinations;
    }

    /// <summary>
    /// 获取所有花色的单个点位的同花顺
    /// </summary>
    /// <param name="p_Rank"></param>
    /// <returns></returns>
    private static List<List<Card>> m_GetSingleStraightFlushCombinations(Enum_CardRank p_Rank)
    {
        var t_Combinations = new List<List<Card>>();
        var t_AllSuits = new[]
        {
            Enum_CardSuit.Spades,
            Enum_CardSuit.Hearts,
            Enum_CardSuit.Clubs,
            Enum_CardSuit.Diamonds
        };

        switch (p_Rank)
        {
            case Enum_CardRank.Ace:
            case Enum_CardRank.King:
            case Enum_CardRank.Queen:
            case Enum_CardRank.Jack:
            case Enum_CardRank.Ten:
            case Enum_CardRank.Nine:
            case Enum_CardRank.Eight:
            case Enum_CardRank.Seven:
            case Enum_CardRank.Six:
                {
                    foreach (var t_Suit in t_AllSuits)
                    {
                        var t_List = new List<Card>();
                        for (int i = 0; i < 5; i++)
                        {
                            t_List.Add(new Card(t_Suit, (Enum_CardRank)((int)p_Rank-i)));
                        }
                        t_Combinations.Add(t_List);
                    }
                }
                break;
            case Enum_CardRank.Five:
                {
                    foreach (var t_Suit in t_AllSuits)
                    {
                        // 添加5-4-3-2-A特殊组合
                        t_Combinations.Add(new List<Card> {
                            new(t_Suit, Enum_CardRank.Ace),
                            new(t_Suit, Enum_CardRank.Five),
                            new(t_Suit, Enum_CardRank.Four),
                            new(t_Suit, Enum_CardRank.Three),
                            new(t_Suit, Enum_CardRank.Two)
                        });
                    }
                }
                break;
        }

        return t_Combinations;
    }
}