using System.Collections;
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
    public Button closePopup;
    public Button closePopupMap;
    public Button closePopupIncident;
    public Button closePopupKernwoorden;
    public Button closePopupEnd;

    public IncidentController incidentController;

    // Start is called before the first frame update
    void Start()
    {
        closePopupMap.onClick.AddListener(OnClickHandlerMap);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickHandlerMap()
    {
        if (popupMap.active)
        {
            popupMap.SetActive(false);
            closePopupIncident.onClick.AddListener(OnClickHandlerIncident);
            popupIncident.SetActive(true);
        }
    }

    void OnClickHandlerIncident()
    {
        if (popupIncident.active)
        {
            popupIncident.SetActive(false);
            closePopup.onClick.AddListener(OnClickHandlerPopup);
            popup.SetActive(true);

        }
    }

    void OnClickHandlerPopup()
    {
        if (popup.active)
        {
            popup.SetActive(false);
            closePopupKernwoorden.onClick.AddListener(OnClickHandlerPopupKernwoorden);
            popupKernwoorden.SetActive(true);
        }
    }

    void OnClickHandlerPopupKernwoorden()
    {
        if (popupKernwoorden.active)
        {
            popupKernwoorden.SetActive(false);
            closePopupEnd.onClick.AddListener(OnClickHandlerPopupEnd);
            popupEnd.SetActive(true);
        }
    }

    void OnClickHandlerPopupEnd()
    {
        if (popupEnd.active)
        {
            popupEnd.SetActive(false);
            incidentController.TutScenario();
        }
    }

}
