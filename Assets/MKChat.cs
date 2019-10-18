using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MKChat : MonoBehaviour
{
    public Button MKSendButton;
    public Dropdown MKDropDown;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = MKSendButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClickHandler);
    }


    // Update is called once per frame
    void Update()
    {

    }

    void OnClickHandler()
    {
        ChatApp.StartingConversation();
    }
}
