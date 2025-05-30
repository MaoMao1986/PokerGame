using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BattleTeam : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab_BattleUnit;

    public UI_BattleUnit Create(FightingUnit p_Unit)
    {
        GameObject t_Cell = Instantiate(m_Prefab_BattleUnit, transform);
        UI_BattleUnit t_Script = t_Cell.GetComponent<UI_BattleUnit>();
        t_Script.InitData(p_Unit);
        return t_Script;
    } 
}
