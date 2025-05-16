using System.Collections.Generic;

public static class Consts
{
    public static Dictionary<Enum_PokerHandType,string> PokerHandTypeName = new()
    {
        { Enum_PokerHandType.HighCard, "����" },
        { Enum_PokerHandType.OnePair, "һ��" },
        { Enum_PokerHandType.TwoPairs, "����" },
        { Enum_PokerHandType.ThreeOfAKind, "����" },
        { Enum_PokerHandType.Flush, "˳��" },
        { Enum_PokerHandType.Straight, "ͬ��" },
        { Enum_PokerHandType.FullHouse, "��«" },
        { Enum_PokerHandType.FourOfAKind, "����" },
        { Enum_PokerHandType.StraightFlush, "ͬ��˳" },
        { Enum_PokerHandType.RoyalFlush, "�ʼ�ͬ��˳" }
    };
}