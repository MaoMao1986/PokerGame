using System;

public class Card : IComparable<Card>
{
    public string Icon
    {
        get
        {
            DRPokercard t_Row = ConfigManager.GetRow<DRPokercard>(ID);
            return "UI/Card/" + t_Row.Icon;
        }
    }
    public string ID { get { return ((int)Suit * 100 + (int)Rank).ToString(); } }
    public Enum_CardSuit Suit { get; private set; }
    public Enum_CardRank Rank { get; private set; }
    public UI_Card UICard { get; set; }
    public int Attack { get; private set; }

    public Card(Enum_CardSuit p_Suit, Enum_CardRank p_Rank)
    {
        Suit = p_Suit;
        Rank = p_Rank;
    }

    public int CompareTo(Card p_Other)
    {
        int t_Compare = 0;
        if (Rank == p_Other.Rank)
        {
            t_Compare = Suit.CompareTo(p_Other.Suit);
        }
        else
        {
            t_Compare = Rank.CompareTo(p_Other.Rank);
        }
        return t_Compare;
    }
}