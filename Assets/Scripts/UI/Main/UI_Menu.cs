using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Menu : MonoBehaviour
{
    [SerializeField] private Button m_Map;
    [SerializeField] private Button m_Test;
    // Start is called before the first frame update
    void Start()
    {
        m_Test.onClick.AddListener(AddExp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddExp()
    {
        RuntimeData.Player.PlayerLevel.PlayerPros.CurrentExp.Add(666, out int t_Add);
    }
}
