using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour 
{
    private int currentRound = 0;
    private bool playerFirst;
    private bool battleEnded = false;
    
    private List<Card> PublicCards = new();
    private BattleUser Player;
    private BattleUser Enemy;

    void Start() {
        InitializeBattle();
        StartCoroutine(BattleLoop());
    }

    private void InitializeBattle() {
        // 初始敌人
        Enemy.Init();
        // 初始化玩家
        Player.Init();
        // 初始化公共牌
        PublicCards = DeckManager.Instance.DrawPublicCards(3);
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
        yield return StartCoroutine(DiscardPhase());
        
        // 出牌阶段
        List<Card> playerHand = Player.Instance.DrawHand();
        List<Card> enemyHand = Enemy.Instance.DrawHand();
        
        // 组合公共牌
        if (PublicCards.Count > 0) {
            playerHand.AddRange(PublicCards);
            enemyHand.AddRange(PublicCards);
        }
        
        // 触发牌型技能
        if (playerFirst) {
            yield return StartCoroutine(ProcessPlayerTurn(playerHand));
            yield return StartCoroutine(ProcessEnemyTurn(enemyHand));
        } else {
            yield return StartCoroutine(ProcessEnemyTurn(enemyHand));
            yield return StartCoroutine(ProcessPlayerTurn(playerHand));
        }
        
        // 回合结束处理
        yield return StartCoroutine(EndRoundCleanup());
    }

    private IEnumerator ProcessPlayerTurn(List<Card> hand) {
        PokerHand handType = HandEvaluator.EvaluateHand(hand);
        SkillManager.Instance.TriggerHandSkill(handType, Player.Instance, Enemy.Instance);
        yield return StartCoroutine(TriggerCardSkills(hand, isPlayer: true));
        Player.Instance.FinalSkill();
    }

    private IEnumerator ProcessEnemyTurn(List<Card> hand) {
        PokerHand handType = HandEvaluator.EvaluateHand(hand);
        SkillManager.Instance.TriggerHandSkill(handType, Enemy.Instance, Player.Instance);
        yield return StartCoroutine(TriggerCardSkills(hand, isPlayer: false));
        Enemy.Instance.FinalSkill();
    }

    private IEnumerator TriggerCardSkills(List<Card> hand, bool isPlayer) {
        foreach (Card card in hand) {
            yield return new WaitForSeconds(0.5f);
        }
    }

    private bool CheckBattleEnd() {
        if (Player.Instance.IsDead || Enemy.Instance.IsDead) {
            battleEnded = true;
            return true;
        }
        return false;
    }

    private void EndBattle() {
        Debug.Log(Player.Instance.IsDead ? "战斗失败" : "战斗胜利");
    }
}