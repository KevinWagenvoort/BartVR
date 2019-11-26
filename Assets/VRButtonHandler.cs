using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRButtonHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "VRUIButton")
        {
            Debug.Log("VRUIButtonPressed " + collider.gameObject.name);
        }
    }
}
