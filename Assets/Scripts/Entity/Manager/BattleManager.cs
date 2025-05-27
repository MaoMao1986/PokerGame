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
    private BattleUser Player;
    private BattleUser Enemy;

    public GameObject HandCardGroup;
    public GameObject PublicCardGroup;
    public GameObject DawnCardGroup;
    public GameObject DropCardDeckGroup;
    public GameObject CardDeckGroup;

    private UI_CardGroup m_HandCardGroup;
    private UI_CardGroup m_PublicCardGroup;
    private UI_CardGroup m_DawnCardGroup;
    private UI_CardGroup m_CardDeckGroup;
    private UI_CardGroup m_DropCardDeckGroup;

    public Button DawnCard;
    public Button DropCard;
    public Button Reset;
    public Button RemoveLight;
    public Button DropDeckBack;

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
            yield return StartCoroutine(PlayerTurnCoroutine()); // 直接进入出牌阶段

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

        m_PublicCardGroup = PublicCardGroup.GetComponent<UI_CardGroup>();
        m_HandCardGroup = HandCardGroup.GetComponent<UI_CardGroup>();
        m_DawnCardGroup = DawnCardGroup.GetComponent<UI_CardGroup>();
        m_CardDeckGroup = CardDeckGroup.GetComponent<UI_CardGroup>();
        m_DropCardDeckGroup = DropCardDeckGroup.GetComponent<UI_CardGroup>();

        DropCard.onClick.AddListener(m_DropCardOnClick);
        Reset.onClick.AddListener(m_ResetOnClick);
        RemoveLight.onClick.AddListener(m_RemoveLight);
    }

    public IEnumerator Test()
    {
        // 测试代码
        m_Deck = new Deck();
        m_Deck.InitializeBaseDeck();

        // 初始化出牌区
        List<Card> t_Blank = new();
        m_DawnCardGroup.InitData(t_Blank);
        m_PublicCardGroup.InitData(t_Blank);
        m_HandCardGroup.InitData(t_Blank, true);

        // 初始化公共牌
        yield return DealingCards(m_PublicCardGroup, 5); 

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
        foreach (var t_Card in m_DawnCardGroup.GetCardDatas())
        {
            t_Card?.CardLight(false);
        }
        foreach (var t_Card in m_PublicCardGroup.GetCardDatas())
        {
            t_Card?.CardLight(false);
        }
    }

    private void m_CurrentRoundChange()
    {
        CurrentRound.text = (m_CurrentRound+1).ToString() + " / " + m_RoundMax ;
    }

    /// <summary>
    /// 弃牌按钮点击事件
    /// </summary>
    private void m_DropCardOnClick()
    {
        List<UI_Card> t_Cards = m_HandCardGroup.GetCardDatas(true);
        if (t_Cards.Count > 0)
        {
            m_HandCardGroup.DestroyCards(t_Cards);
            StartCoroutine(DealingCards(m_HandCardGroup, t_Cards.Count));
        }
        else
        {
            Debug.Log("没有选择任何牌");
        }
    }

    private IEnumerator DealingCards(UI_CardGroup p_Group, int p_Num)
    {
        // 发牌
        List<Card> t_HandCards = m_Deck.DrawCards(p_Num);
        List<UI_Card> t_UIHands = m_CardDeckGroup.InitData(t_HandCards);
        m_UpdateLeftCardNum();
        yield return StartCoroutine(m_CardDeckGroup.MoveToOtherGroup(t_UIHands, p_Group));
    } 

    IEnumerator PlayerTurnCoroutine()
    {
        yield return DealingCards(m_HandCardGroup, 5);

        // 3. 等待玩家出牌（监听"出牌"按钮）
        bool hasPlayed = false;
        List<UI_Card> selectedCards = null;
        DawnCard.onClick.AddListener(() => {
            if(m_HandCardGroup.GetCardDatas(true).Count < 5) {
                Debug.Log("请至少选择5张牌出牌");
                return;
            }
            selectedCards = m_HandCardGroup.GetCardDatas(true); // 假设玩家选了5张牌
            hasPlayed = true;
        });

        yield return new WaitUntil(() => hasPlayed);

        // 4. 处理出牌逻辑
        yield return StartCoroutine(ResolvePlayerCards(selectedCards));
    }

    IEnumerator ResolvePlayerCards(List<UI_Card> playedCards)
    {
        // 1. 移动牌到出牌区域并等待2秒
        yield return StartCoroutine(m_HandCardGroup.MoveToOtherGroup(m_HandCardGroup.GetCardDatas(true), m_DawnCardGroup));
        yield return new WaitForSeconds(2f);

        List<Card> t_DawnCards = m_DawnCardGroup.GetCardDatas().GetCardLists();
        List<Card> t_PublicCards = m_PublicCardGroup.GetCardDatas().GetCardLists();

        // 2. 结合公共牌判定牌型（假设TexasPokerRule是牌型判定工具类）
        var (t_Type, t_MatchedCards) = PokerHandEvaluator.EvaluateHand(t_DawnCards, t_PublicCards);
        foreach (var t_Card in t_MatchedCards.Keys)
        {
            t_Card.UICard?.CardLight();
        }
        CardType.text = Consts.PokerHandTypeName[t_Type];
        // 让Text可见
        CardTypeUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        CardTypeUI.gameObject.SetActive(false);

        // 3.清理出牌区
        m_DawnCardGroup.DestroyCards(m_DawnCardGroup.GetCardDatas());
        m_RemoveLight();
        Debug.Log("清理出牌区和公共牌高亮");
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