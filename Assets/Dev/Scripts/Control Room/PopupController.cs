﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public GameObject popup;
    public GameObject popupMap;
    public GameObject popupIncident;
    public GameObject popupKernwoorden;
    public GameObject popupEnd;
    public GameObject popupSendPoliceUitleg;

    public GameObject policeSend; // popup to send police for explanation 

    public Button closePopup;
    public Button closePopupMap;
    public Button closePopupIncident;
    public Button closePopupKernwoorden;
    public Button closePopupEnd;
    public Button closePopupPoliceUitleg;

    public IncidentController incidentController;

    // Start is called before the first frame update
    void Start()
    {
        closePopupMap.onClick.AddListener(OnClickHandlerMap);
        closePopupPoliceUitleg.onClick.AddListener(OnClickHandlerPoliceUitleg);
        closePopup.onClick.AddListener(OnClickHandlerPopup);
        closePopupIncident.onClick.AddListener(OnClickHandlerIncident);
        closePopupKernwoorden.onClick.AddListener(OnClickHandlerPopupKernwoorden);
        closePopupEnd.onClick.AddListener(OnClickHandlerPopupEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickHandlerMap()
    {
        if (popupMap.activeInHierarchy)
        {
            popupMap.SetActive(false);
            popupSendPoliceUitleg.SetActive(true);
            policeSend.SetActive(true);
            
        }
    }
    
    void OnClickHandlerPoliceUitleg()
    {
        if (popupSendPoliceUitleg.activeInHierarchy)
        {
            popupSendPoliceUitleg.SetActive(false);
            policeSend.SetActive(false);
            popupIncident.SetActive(true);
            incidentController.TutScenario();

        }
    }

    void OnClickHandlerIncident()
    {
        if (popupIncident.activeInHierarchy)
        {            
            popupIncident.SetActive(false);
        }
    }


    void OnClickHandlerPopup()
    {
        if (popup.activeInHierarchy)
        {
            popup.SetActive(false);
            popupKernwoorden.SetActive(true);
        }
    }

    void OnClickHandlerPopupKernwoorden()
    {
        if (popupKernwoorden.activeInHierarchy)
        {
            popupKernwoorden.SetActive(false);
            popupEnd.SetActive(true);
        }
    }

    void OnClickHandlerPopupEnd()
    {
        if (popupEnd.activeInHierarchy)
        {
            popupEnd.SetActive(false);
            
        }
    }

}
