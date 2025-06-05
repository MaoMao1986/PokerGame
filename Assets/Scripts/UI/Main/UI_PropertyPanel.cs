using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PropertyPanel : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab_PropertyRow;
    [SerializeField] private GameObject m_PropertyListPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitData(Propertys p_List)
    {
        foreach (Property t_Property in p_List.PropertyList.Values)
        {
            GameObject t_PropertyRow = Instantiate(m_Prefab_PropertyRow, m_PropertyListPanel.transform);
            UI_PropertyRow t_Script = t_PropertyRow.GetComponent<UI_PropertyRow>();
            if (t_Script != null)
            {
                t_Script.UpdateData(t_Property);
            }
            else
            {
                Debug.LogError("UI_PropertyRow script not found on prefab!");
            }
        }
    }
}
