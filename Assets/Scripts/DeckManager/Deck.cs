using System.Collections.Generic;
using System.Linq;

public class Deck
{
    // 活跃牌堆：当前可抽取的卡牌队列
    private Queue<Card> m_ActivePile = new();
    // 弃牌堆：已使用卡牌的临时存放栈（先进后出）
    private Stack<Card> m_DiscardPile = new();
    // 基础牌组：存储初始生成的52张卡牌配置
    private List<Card> m_BaseCards = new();

    /// <summary>
    /// 初始化标准牌堆（排除大小王）
    /// </summary>
    public void InitializeBaseDeck()
    {
        foreach (Enum_CardSuit t_Suit in System.Enum.GetValues(typeof(Enum_CardSuit)))
        {
            foreach (Enum_CardRank t_Rank in System.Enum.GetValues(typeof(Enum_CardRank)))
            {
                // 排除大小王枚举值（根据Enum_CardRank定义）
                if (t_Rank != Enum_CardRank.LittleJoker && t_Rank != Enum_CardRank.BigJoker) 
                {
                    m_BaseCards.Add(new Card(t_Suit, t_Rank));
                }
            }
        }
        Shuffle();
    }

    /// <summary>
    /// 洗牌逻辑：合并活跃牌堆和弃牌堆后随机排序
    /// </summary>
    public void Shuffle()
    {
        var t_Random = new System.Random();
        // 使用LINQ随机排序实现近似Fisher-Yates洗牌算法
        var t_Shuffled = m_ActivePile.Concat(m_DiscardPile).OrderBy(x => t_Random.Next()).ToList();
        
        m_ActivePile = new Queue<Card>(t_Shuffled);
        m_DiscardPile.Clear();
    }

    /// <summary>
    /// 抽牌方法
    /// </summary>
    /// <param name="p_Count">需要抽取的卡牌数量</param>
    /// <returns>成功抽取的卡牌列表（可能少于请求数量）</returns>
    public List<Card> DrawCards(int p_Count)
    {
        List<Card> t_Drawn = new();
        
        while (t_Drawn.Count < p_Count)
        {
            if (!m_ActivePile.Any())
            {
                // 当活跃牌堆为空时，检查弃牌堆是否可用
                if (!m_DiscardPile.Any()) break; 
                Shuffle(); // 重新洗牌合并牌堆
            }
            
            t_Drawn.Add(m_ActivePile.Dequeue());
        }
        
        return t_Drawn;
    }

    /// <summary>
    /// 弃牌处理：将使用后的卡牌转为公共牌存入弃牌堆
    /// </summary>
    /// <param name="p_Cards">需要弃置的卡牌集合</param>
    public void DiscardCards(IEnumerable<Card> p_Cards)
    {
        foreach (var t_Card in p_Cards)
        {
            // 克隆卡牌为公共牌（避免修改原始卡牌数据）
            m_DiscardPile.Push(t_Card.CloneAsPublicCard()); 
        }
    }

    /// <summary>
    /// 剩余卡牌总数（活跃牌堆 + 弃牌堆）
    /// </summary>
    public int RemainingCards => m_ActivePile.Count + m_DiscardPile.Count;
}