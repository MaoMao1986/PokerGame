using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UI_PropertyRow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ProName;
    [SerializeField] private TextMeshProUGUI m_ProValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateData(Property p_Property)
    {
        m_ProName.text = ConfigManager.GetRow<DRProperty>(p_Property.Id).Displayname;
        m_ProValue.text = p_Property.GetDisplay();
    }
}
