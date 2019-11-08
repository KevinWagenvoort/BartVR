using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrigger : MonoBehaviour
{
    public GameObject CameraRig, Phone, LeftHand;
    private bool phoneIsActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(CameraRig.transform.position, gameObject.transform.position);
        if (distance < 53 && phoneIsActive)
        {
            Phone.SetActive(false);
            LeftHand.SetActive(true);
            phoneIsActive = false;
            Debug.Log("enter");
        } else if (distance >= 53 && !phoneIsActive)
        {
            Phone.SetActive(true);
            LeftHand.SetActive(false);
            phoneIsActive = true;
            Debug.Log("exit");
        }
    }
}
