/// <summary>
/// 扑克花色
/// </summary>
public enum Enum_CardSuit
{
    /// <summary>
    /// 大小王
    /// </summary>
    大小王 = 1,
    /// <summary>
    /// 方块
    /// </summary>
    方块 = 2,
    /// <summary>
    /// 梅花
    /// </summary>
    梅花 = 3,
    /// <summary>
    /// 红桃
    /// </summary>
    红桃 = 4,
    /// <summary>
    /// 黑桃
    /// </summary>
    黑桃 = 5
}

/// <summary>
/// 扑克大小
/// </summary>
public enum Enum_CardRank 
{ 
    Two = 2, 
    Three = 3, 
    Four = 4, 
    Five = 5, 
    Six = 6, 
    Seven = 7, 
    Eight = 8, 
    Nine = 9, 
    Ten = 10, 
    Jack = 11, 
    Queen = 12, 
    King = 13,
    Ace = 14,
    LittleJoker = 15,
    BigJoker​ = 16
}

/// <summary>
/// 牌型
/// </summary>
public enum Enum_PokerHandType
{
    /// <summary>
    /// 高牌，单张最大
    /// </summary>
    HighCard = 1,
    /// <summary>
    /// 对子
    /// </summary>
    OnePair = 2,
    /// <summary>
    /// 两对
    /// </summary>
    TwoPairs = 3,
    /// <summary>
    /// 三张
    /// </summary>
    ThreeOfAKind = 4,
    /// <summary>
    /// 同花
    /// </summary>
    Straight = 5,
    /// <summary>
    /// 顺子
    /// </summary>
    Flush = 6,
    /// <summary>
    /// 三带二
    /// </summary>
    FullHouse = 7,
    /// <summary>
    /// 四条
    /// </summary>
    FourOfAKind = 8,
    /// <summary>
    /// 同花顺
    /// </summary>
    StraightFlush = 9,
    /// <summary>
    /// 皇家同花顺
    /// </summary>
    RoyalFlush = 10
}
