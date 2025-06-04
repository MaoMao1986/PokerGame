using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_MapLayer : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab_Map;
    // Start is called before the first frame update
    void Start()
    {
        GameObject t_Point = Instantiate(m_Prefab_Map, transform);
        UI_MapPoint t_UIMap = t_Point.GetComponent<UI_MapPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
