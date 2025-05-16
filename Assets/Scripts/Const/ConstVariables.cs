using System.Collections.Generic;

public static class Consts
{
    public static Dictionary<Enum_PokerHandType,string> PokerHandTypeName = new()
    {
        { Enum_PokerHandType.HighCard, "高牌" },
        { Enum_PokerHandType.OnePair, "一对" },
        { Enum_PokerHandType.TwoPairs, "两对" },
        { Enum_PokerHandType.ThreeOfAKind, "三条" },
        { Enum_PokerHandType.Flush, "顺子" },
        { Enum_PokerHandType.Straight, "同花" },
        { Enum_PokerHandType.FullHouse, "葫芦" },
        { Enum_PokerHandType.FourOfAKind, "四条" },
        { Enum_PokerHandType.StraightFlush, "同花顺" },
        { Enum_PokerHandType.RoyalFlush, "皇家同花顺" }
    };
}