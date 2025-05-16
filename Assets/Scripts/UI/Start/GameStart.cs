using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject Prefab_UIBattle;
    public GameObject Bg;

    // Start is called before the first frame update
    void Start()
    {
        CfgTableMgr.LoadConfig();
        CreateBattleUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateBattleUI()
    {
        GameObject t_Cell = Instantiate(Prefab_UIBattle, Bg.transform);
    }
}
