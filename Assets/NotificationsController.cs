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

    int a = 0;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            SetActiveNotification(NotificationType[a], NotificationType[a].name + ": Dit is een hele lange zin, maar is deze zin wel lang genoeg?");
            a++;
        } else if (Input.GetKeyUp(KeyCode.N))
        {
            HideNotification();
        }
        else if (Input.GetKeyUp(KeyCode.M))
        {
            ShowNotification();
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
    }

    GameObject NotificatioToHide;
    public void HideAfterSeconds(float seconds)
    {
        NotificatioToHide = ActiveNotification;
        Invoke("HideNotification", 3);
    }
}
