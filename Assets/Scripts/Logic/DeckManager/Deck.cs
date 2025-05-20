using System.Collections.Generic;
using System.Linq;

public class Deck
{
    // 活跃牌堆：当前可抽取的卡牌队列
    private List<Card> m_ActiveCardList = new();
    // 弃牌堆：已使用卡牌的临时存放栈（先进后出）
    private Stack<Card> m_DiscardPile = new();

    /// <summary>
    /// 初始化标准牌堆（排除大小王）
    /// </summary>
    public void InitializeBaseDeck()
    {
        // 加上大小王
        m_ActiveCardList.Add(new Card(Enum_CardSuit.Joker, Enum_CardRank.BigJoker));
        m_ActiveCardList.Add(new Card(Enum_CardSuit.Joker, Enum_CardRank.LittleJoker));

        foreach (Enum_CardSuit t_Suit in System.Enum.GetValues(typeof(Enum_CardSuit)))
        {
            if(t_Suit != Enum_CardSuit.Joker)
            {
                foreach (Enum_CardRank t_Rank in System.Enum.GetValues(typeof(Enum_CardRank)))
                {
                    if(t_Rank != Enum_CardRank.LittleJoker && t_Rank != Enum_CardRank.BigJoker)
                    {
                        m_ActiveCardList.Add(new Card(t_Suit, t_Rank));
                    }
                }
            }
        }
    }

    public int GetLeftCardNum()
    {
        return m_ActiveCardList.Count;
    }

    /// <summary>
    /// 抽牌方法
    /// </summary>
    /// <param name="p_Count">需要抽取的卡牌数量</param>
    /// <returns>成功抽取的卡牌列表（可能少于请求数量）</returns>
    public List<Card> DrawCards(int p_Count)
    {
        List<Card> t_Drawn = new();
        var t_Random = new System.Random();
        for (int i = 1; i <= p_Count; i++)
        {
            if(m_ActiveCardList.Count > 0)
            {
                int t_Index = t_Random.Next(1, m_ActiveCardList.Count);
                t_Drawn.Add(m_ActiveCardList[t_Index - 1]);
                m_ActiveCardList.RemoveAt(t_Index - 1);
            }
        }        
        return t_Drawn;
    }

    /// <summary>
    /// 弃牌处理：将使用后的卡牌转为公共牌存入弃牌堆
    /// </summary>
    /// <param name="p_Cards">需要弃置的卡牌集合</param>
    public void DiscardCards(IEnumerable<Card> p_Cards)
    {
        
    }
}