using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTrigger : MonoBehaviour
{
    public GameObject CameraRig, Phone, LeftHand;
    public static bool TutorialBurgerIsDone = false;
    public static bool TutorialControlRoomIsDone = false;
    private bool phoneIsActive = true;

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(CameraRig.transform.position, gameObject.transform.position);
        if (distance < 37 && phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone)
        {
            Phone.SetActive(false);
            LeftHand.SetActive(true);
            phoneIsActive = false;
            Debug.Log("enter");
        } else if (distance >= 37 && !phoneIsActive && TutorialBurgerIsDone && TutorialControlRoomIsDone)
        {
            Phone.SetActive(true);
            LeftHand.SetActive(false);
            phoneIsActive = true;
            Debug.Log("exit");
        } else
        {
            Debug.Log(distance);
        }
    }
}
