using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationsController : MonoBehaviour
{
    //public
    public List<GameObject> NotificationType = new List<GameObject>();

    //private
    private GameObject ActiveNotification = null;
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device controller;

    private void Update()
    {
        try
        {
            controller = SteamVR_Controller.Input((int)trackedObject.index);
        } catch
        {

        }
    }

    void ShowNotification()
    {
        if (ActiveNotification != null)
            ActiveNotification.SetActive(true);
    }

    public void HideActiveNotification()
    {
        if (ActiveNotification != null)
            ActiveNotification.SetActive(false);
    }

    public void HideNotification()
    {
        if (NotificatioToHide != null)
            NotificatioToHide.SetActive(false);
    }

    public void SetActiveNotification(GameObject notification, string message)
    {
        HideActiveNotification();//Hide old
        ActiveNotification = notification;//Save new
        ActiveNotification.GetComponentInChildren<TMP_Text>().text = message;
        ShowNotification();//Show new
        HideAfterSeconds(2.5f);
        StartCoroutine(LongVibration(1, 3999));
    }

    GameObject NotificatioToHide;
    public void HideAfterSeconds(float seconds)
    {
        NotificatioToHide = ActiveNotification;
        Invoke("HideNotification", 3);
    }

    IEnumerator LongVibration(float seconds, ushort strength)
    {
        for (float i = 0; i < seconds; i += Time.deltaTime)
        {
            try
            {
                controller.TriggerHapticPulse(strength);
            } catch
            {

            }
            yield return null; //every single frame for the duration of "length" you will vibrate at "strength" amount
        }
    }
}
