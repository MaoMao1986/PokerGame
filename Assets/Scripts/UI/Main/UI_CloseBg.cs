using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CloseBg : MonoBehaviour
{
    public enum CloseType
    {
        Ïú»Ù,
        Òþ²Ø
    }

    [SerializeField] private CloseType m_CloseType = CloseType.Ïú»Ù;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(m_OnClickClose);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void m_OnClickClose()
    {
        switch (m_CloseType)
        {
            case CloseType.Ïú»Ù:
                Destroy(transform.parent.gameObject);
                break;
            case CloseType.Òþ²Ø:
                transform.parent.gameObject.SetActive(false);
                break;
        }
    }
}
