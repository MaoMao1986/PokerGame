using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private int m_CurrentRound = 0;
    private int m_RoundMax = 4;
    private int currentRound {
        get 
        {
            return m_CurrentRound;
        }
        set
        {
            if(m_CurrentRound != value)
            {
                m_CurrentRoundChange();
            }
            m_CurrentRound = value;
        } 
    }
    private bool playerFirst;
    private bool battleEnded = false;

    private Deck m_Deck;
    private CardCollection PublicCards;

    public GameObject HandCardGroup;
    public GameObject PublicCardGroup;
    public GameObject DawnCardGroup;
    public GameObject DropCardDeckGroup;
    public GameObject CardDeckGroup;

    private UI_PokerGroup m_HandPokerGroup;
    private UI_PokerGroup m_PublicPokerGroup;
    private UI_PokerGroup m_DawnPokerGroup;
    private UI_PokerGroup m_PokerDeckGroup;
    private UI_PokerGroup m_DropPokerDeckGroup;

    public Button DawnCard;
    public Button DropCard;
    public Button Reset;
    public Button RemoveLight;
    public Button DropDeckBack;
    public Button DropCardUI;

    public TextMeshProUGUI CardType;
    public TextMeshProUGUI LeftCardNum;
    public TextMeshProUGUI CurrentRound;

    public GameObject CardTypeUI;

    void Start() {
        //InitializeBattle();
        StartCoroutine(BattleLoop());
    }

    private IEnumerator BattleLoop()
    {
        // 初始化战斗
        yield return InitializeCards();

        // 战斗回合流程
        while (currentRound <= 4 && !battleEnded)
        {
            Debug.Log($"第 {currentRound} 回合开始");            

            // 1. 显示回合UI，允许玩家操作（弃牌是可选按钮，不阻塞流程）
            // uiRoundPanel.Show($"Round {currentRound}");
            yield return StartCoroutine(m_PlayerTurnCoroutine()); // 直接进入出牌阶段

            // 2. 怪物回合和结束逻辑
            yield return StartCoroutine(MonsterTurnCoroutine());
            yield return StartCoroutine(EndRoundCoroutine());

            currentRound++;

            if (CheckBattleEnd())
            {
                break;
            }
        }
        EndBattle();
    }

    private IEnumerator InitializeCards()
    {
        Init();
        yield return StartCoroutine(Test());
        //// 1. 初始化54张牌（假设已预先加载）
        //List<Card> allCards = CreateDeck(); // 生成牌堆

        //// 2. 随机抽取5张公共牌
        //publicCards = DrawCards(allCards, 5);

        //// 3. 抽取5张玩家手牌
        //playerHand = DrawCards(allCards, 5);

        //// 4. 剩余牌放入弃牌堆（用于后续补充）
        //discardPile = allCards;

        yield return null; // 可在此处播放发牌动画
    }

    private void Init()
    {
        currentRound = 1;

        m_PublicPokerGroup = PublicCardGroup.GetComponent<UI_PokerGroup>();
        m_HandPokerGroup = HandCardGroup.GetComponent<UI_PokerGroup>();
        m_DawnPokerGroup = DawnCardGroup.GetComponent<UI_PokerGroup>();
        m_PokerDeckGroup = CardDeckGroup.GetComponent<UI_PokerGroup>();
        m_DropPokerDeckGroup = DropCardDeckGroup.GetComponent<UI_PokerGroup>();

        DropCard.onClick.AddListener(m_DropCardOnClick);
        Reset.onClick.AddListener(m_ResetOnClick);
        RemoveLight.onClick.AddListener(m_RemoveLight);
        DropDeckBack.onClick.AddListener(() => DropCardUI.gameObject.SetActive(true));
        DropCardUI.onClick.AddListener(() => DropCardUI.gameObject.SetActive(false));
    }

    public IEnumerator Test()
    {
        // 测试代码
        m_Deck = new Deck();
        m_Deck.InitializeBaseDeck();

        // 初始化出牌区
        List<Poker> t_Blank = new();
        m_DawnPokerGroup.InitData();
        m_PublicPokerGroup.InitData();
        m_HandPokerGroup.InitData(true);

        // 初始化公共牌
        yield return DealingCards(m_PublicPokerGroup, 5); 

        //
        CardTypeUI.GetComponent<Button>().onClick.AddListener(m_CloseCardTypeUI);
    }

    private void m_UpdateLeftCardNum()
    {
        LeftCardNum.text = m_Deck.GetLeftCardNum().ToString();
    }

    private void m_CloseCardTypeUI()
    {
        CardTypeUI.gameObject.SetActive(false);
    }

    private void m_ResetOnClick()
    {
        Test();
    }

    private void m_RemoveLight()
    {
        foreach (var t_Card in m_DawnPokerGroup.GetCardDatas())
        {
            t_Card?.CardLight(false);
        }
        foreach (var t_Card in m_PublicPokerGroup.GetCardDatas())
        {
            t_Card?.CardLight(false);
        }
    }

    private void m_CurrentRoundChange()
    {
        CurrentRound.text = (m_CurrentRound+1).ToString() + " / " + m_RoundMax ;
    }

    /// <summary>
    /// 弃牌按钮点击事件，将手牌中的选中牌移动到弃牌区，并重新发牌到手牌区。
    /// </summary>
    private void m_DropCardOnClick()
    {
        List<UI_Poker> t_Pokers = m_HandPokerGroup.GetCardDatas(true);
        if (t_Pokers.Count > 0)
        {
            StartCoroutine(m_DropPokerDeckGroup.MovePokers(t_Pokers, DropDeckBack.transform.position));
            StartCoroutine(DealingCards(m_HandPokerGroup, t_Pokers.Count));
        }
        else
        {
            Debug.Log("没有选择任何牌");
        }
    }

    /// <summary>
    /// 发牌，在牌堆生成牌，然后移动到手牌
    /// </summary>
    /// <param name="p_Group"></param>
    /// <param name="p_Num"></param>
    /// <returns></returns>
    private IEnumerator DealingCards(UI_PokerGroup p_Group, int p_Num)
    {
        // 发牌
        List<Poker> t_HandCards = m_Deck.DrawCards(p_Num);
        List<UI_Poker> t_UIHands = m_PokerDeckGroup.CreatePoker(t_HandCards);
        m_UpdateLeftCardNum();
        yield return StartCoroutine(p_Group.MovePokers(t_UIHands));
    } 

    /// <summary>
    /// 玩家回合
    /// </summary>
    /// <returns></returns>
    IEnumerator m_PlayerTurnCoroutine()
    {
        yield return DealingCards(m_HandPokerGroup, 5);

        // 3. 等待玩家出牌（监听"出牌"按钮）
        bool hasPlayed = false;
        List<UI_Poker> selectedCards = null;
        DawnCard.onClick.AddListener(() => {
            if(m_HandPokerGroup.GetCardDatas(true).Count < 5) {
                Debug.Log("请至少选择5张牌出牌");
                return;
            }
            selectedCards = m_HandPokerGroup.GetCardDatas(true); // 假设玩家选了5张牌
            hasPlayed = true;
        });

        yield return new WaitUntil(() => hasPlayed);

        // 4. 处理出牌逻辑
        yield return StartCoroutine(ResolvePlayerCards(selectedCards));
    }

    IEnumerator ResolvePlayerCards(List<UI_Poker> playedCards)
    {
        // 1. 移动牌到出牌区域并等待2秒
        yield return StartCoroutine(m_HandPokerGroup.MovePokers(m_HandPokerGroup.GetCardDatas(true), m_DawnPokerGroup));
        yield return new WaitForSeconds(2f);

        List<Poker> t_DawnCards = m_DawnPokerGroup.GetCardDatas().GetCardLists();
        List<Poker> t_PublicCards = m_PublicPokerGroup.GetCardDatas().GetCardLists();

        // 2. 结合公共牌判定牌型（假设TexasPokerRule是牌型判定工具类）
        var (t_Type, t_MatchedCards) = PokerHandEvaluator.EvaluateHand(t_DawnCards, t_PublicCards);
        Dictionary<Poker, UI_Poker> t_Pair = m_DawnPokerGroup.GetCardDatas().GetCardPair(m_PublicPokerGroup.GetCardDatas());
        
        foreach (var t_Card in t_MatchedCards.Keys)
        {
            if (t_Pair.ContainsKey(t_Card))
            {
                t_Pair[t_Card].CardLight();
            }
        }
        CardType.text = Consts.PokerHandTypeName[t_Type];
        // 让Text可见
        CardTypeUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        CardTypeUI.gameObject.SetActive(false);

        // 3.清理出牌区（将出的牌移动到弃牌区）
        m_RemoveLight();
        Debug.Log("清理出牌区和公共牌高亮");
        yield return m_DawnPokerGroup.MovePokers(m_DawnPokerGroup.GetCardDatas());        
        yield return new WaitForSeconds(2f);

        // 4. 释放技能攻击怪物
        //PlaySkillEffect(handType);
        Debug.Log("释放技能攻击怪物");
        yield return new WaitForSeconds(2f); // 等待技能特效完成

        // 5. 怪物受击反馈
        //monster.TakeDamage(handType.Damage);
        Debug.Log("怪物受击反馈");
        yield return new WaitForSeconds(2f); // 怪物受击动画
    }

    IEnumerator MonsterTurnCoroutine()
    {
        // 1. 怪物直接攻击玩家
        //monster.PlayAttackAnimation();
        Debug.Log("怪物直接攻击玩家");
        yield return new WaitForSeconds(1f);

        // 2. 玩家受击
        //player.TakeDamage(monster.Damage);
        //isPlayerDead = player.IsDead;
        Debug.Log("玩家受击");
        yield return new WaitForSeconds(2f); // 受击动画
    }

    IEnumerator EndRoundCoroutine()
    {
        // 播放回合结束动画
        //PlayRoundEndAnimation();
        Debug.Log("播放回合结束动画");
        yield return new WaitForSeconds(1.5f);

        // 检查怪物是否死亡
        //isMonsterDead = monster.IsDead;
        Debug.Log("检查怪物是否死亡");
    }

    private bool CheckBattleEnd() {
        //if (Player.Instance.IsDead || Enemy.Instance.IsDead) {
        //    battleEnded = true;
        //    return true;
        //}
        return false;
    }

    private void EndBattle() {
        //Debug.Log(Player.Instance.IsDead ? "战斗失败" : "战斗胜利");
    }
}