using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject VirtualPhone;

    private PhoneControls PhoneControls;

    // Start is called before the first frame update
    void Start()
    {
        PhoneControls = VirtualPhone.GetComponent<PhoneControls>();
    }

    //VR touch controls
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "VRUIButton")
        {
            switch (collider.gameObject.name)
            {
                case "ButtonChat":
                    PhoneControls.OpenChat();
                    break;
                case "ButtonCamera":
                    PhoneControls.OpenCamera();
                    break;
                case "ButtonMap":
                    PhoneControls.OpenMap();
                    break;
            }
        }
        Debug.Log(collider.gameObject.name);
    }
}
