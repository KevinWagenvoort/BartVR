using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MKChat : MonoBehaviour
{
    public Button MKSendButton;
    public TMP_Dropdown MKDropDown;

    public GameObject NeighbourhoodApp;
    private NeighbourhoodAppScript NeighbourhoodAppScript;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = MKSendButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClickHandler);

        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
    }

    void OnClickHandler()
    {
        Debug.Log(MKDropDown.options[MKDropDown.value].text);
        NeighbourhoodAppScript.TemplateConversation();
    }
}
