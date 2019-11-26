using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRButtonHandler : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("VRButtonHandler");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter " + other.gameObject.name);
    }
}
