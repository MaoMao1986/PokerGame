using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_UILayer : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayerInfo;
    void Start()
    {
        UI_PlayerInfo t_PlayerInfo = m_PlayerInfo.GetComponent<UI_PlayerInfo>();
        t_PlayerInfo.UpdateData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
