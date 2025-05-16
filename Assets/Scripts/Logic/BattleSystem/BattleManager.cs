using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private int currentRound = 0;
    private bool playerFirst;
    private bool battleEnded = false;

    private Deck m_Deck;
    private CardCollection PublicCards;
    private BattleUser Player;
    private BattleUser Enemy;
    public GameObject HandCardGroup;
    public GameObject PublicCardGroup;
    public GameObject DawnCardGroup;
    private UI_CardGroup m_HandCardGroup;
    private UI_CardGroup m_PublicCardGroup;
    private UI_CardGroup m_DawnCardGroup;
    public Button DawnCard;
    public Button DropCard;
    public Button Reset;
    public Button RemoveLight;
    public TextMeshProUGUI CardType;
    public GameObject CardTypeUI;

    void Start() {
        Init();
        Test();
        //InitializeBattle();
        //StartCoroutine(BattleLoop());
    }

    private void Init()
    {
        m_PublicCardGroup = PublicCardGroup.GetComponent<UI_CardGroup>();
        m_HandCardGroup = HandCardGroup.GetComponent<UI_CardGroup>();
        m_DawnCardGroup = DawnCardGroup.GetComponent<UI_CardGroup>();

        DawnCard.onClick.AddListener(m_DawnCardOnClick);
        DropCard.onClick.AddListener(m_DropCardOnClick);
        Reset.onClick.AddListener(m_ResetOnClick);
        RemoveLight.onClick.AddListener(m_RemoveLight);
    }

    public void Test()
    {
        // 测试代码
        m_Deck = new Deck();
        m_Deck.InitializeBaseDeck();

        // 初始化公共牌
        List<Card> t_PublicCards = m_Deck.DrawCards(5);
        m_PublicCardGroup.InitData(t_PublicCards, false);

        // 初始化玩家手牌
        List<Card> t_HandCards = m_Deck.DrawCards(5);
        m_HandCardGroup.InitData(t_HandCards, true);

        // 初始化出牌区
        List<Card> t_DawnCards = new();
        m_DawnCardGroup.InitData(t_DawnCards, false);
    }

    private void m_ResetOnClick()
    {
        Test();
    }

    private void m_RemoveLight()
    {
        foreach (var t_Card in m_DawnCardGroup.GetCardDatas())
        {
            t_Card.UICard?.CardUnLight();
        }
        foreach (var t_Card in m_PublicCardGroup.GetCardDatas())
        {
            t_Card.UICard?.CardUnLight();
        }
    }

    /// <summary>
    /// 出牌按钮点击事件
    /// </summary>
    private void m_DawnCardOnClick()
    {
        List<GameObject> t_Cards = m_HandCardGroup.GetUICardDatas(true);
        if(t_Cards.Count > 0)
        {
            m_DawnCardGroup.MoveCards(t_Cards);
            m_HandCardGroup.RemoveCards(t_Cards);
            // 检测牌型
            List<Card> t_DawnCards = m_DawnCardGroup.GetCardDatas();
            List<Card> t_PublicCards = m_PublicCardGroup.GetCardDatas();
            var(t_Type, t_MatchedCards) = PokerHandEvaluator.EvaluateHand(t_DawnCards, t_PublicCards);
            foreach(var t_Card in t_MatchedCards.Keys)
            {
                t_Card.UICard?.CardLight();
            }
            CardType.text = Consts.PokerHandTypeName[t_Type];
            // 让Text可见
            CardTypeUI.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("没有选择任何牌");
        }
    }

    /// <summary>
    /// 弃牌按钮点击事件
    /// </summary>
    private void m_DropCardOnClick()
    {
        List<GameObject> t_Cards = m_HandCardGroup.GetUICardDatas(true);
        if (t_Cards.Count > 0)
        {
            m_HandCardGroup.DestroyCards(t_Cards);
        }
        else
        {
            Debug.Log("没有选择任何牌");
        }
    }



    private void InitializeBattle() {
        // 初始敌人
        Enemy.Init();
        // 初始化玩家
        Player.Init();
        // 初始化公共牌
        
        // 确定出手顺序
        DetermineBattleOrder();
    }

    /// <summary>
    /// 确定出手顺序
    /// </summary>
    private void DetermineBattleOrder() {
        
    }

    private IEnumerator BattleLoop() {
        while (currentRound < 4 && !battleEnded) {
            Debug.Log($"第 {currentRound + 1} 回合开始");
            yield return StartCoroutine(PlayRound());
            currentRound++;
            
            if (CheckBattleEnd()) {
                break;
            }
        }
        EndBattle();
    }

    private IEnumerator PlayRound() {
        // 弃牌阶段
        //yield return StartCoroutine(DiscardPhase());
        
        // 出牌阶段
        List<Card> playerHand = new();
        List<Card> enemyHand = new();
        
        // 组合公共牌
        //if (PublicCards.Count > 0) {
        //    playerHand.AddRange(PublicCards);
        //    enemyHand.AddRange(PublicCards);
        //}
        
        // 触发牌型技能
        //if (playerFirst) {
        //    yield return StartCoroutine(ProcessPlayerTurn(playerHand));
        //    yield return StartCoroutine(ProcessEnemyTurn(enemyHand));
        //} else {
            yield return StartCoroutine(ProcessEnemyTurn(enemyHand));
            yield return StartCoroutine(ProcessPlayerTurn(playerHand));
        //}
        
        // 回合结束处理
        //yield return StartCoroutine(EndRoundCleanup());
    }

    private IEnumerator ProcessPlayerTurn(List<Card> hand) {
        //PokerHand handType = HandEvaluator.EvaluateHand(hand);
        //SkillManager.Instance.TriggerHandSkill(handType, Player.Instance, Enemy.Instance);
        yield return StartCoroutine(TriggerCardSkills(hand, isPlayer: true));
        //Player.Instance.FinalSkill();
    }

    private IEnumerator ProcessEnemyTurn(List<Card> hand) {
        //PokerHand handType = HandEvaluator.EvaluateHand(hand);
        //SkillManager.Instance.TriggerHandSkill(handType, Enemy.Instance, Player.Instance);
        yield return StartCoroutine(TriggerCardSkills(hand, isPlayer: false));
        //Enemy.Instance.FinalSkill();
    }

    private IEnumerator TriggerCardSkills(List<Card> hand, bool isPlayer) {
        foreach (Card card in hand) {
            yield return new WaitForSeconds(0.5f);
        }
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