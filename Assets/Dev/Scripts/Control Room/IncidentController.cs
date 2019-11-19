using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IncidentController : MonoBehaviour
{
    //public
    public GameObject Content;
    public GameObject ChatScreen;
    public GameObject OpenIncidentScreen;
    public Color ActiveColor;

    public PopupController popupController;

    public List<GameObject> ListOfIncidentsTypes = new List<GameObject>();

    //private
    private List<GameObject> ListOfIncidents = new List<GameObject>();
    private List<GameObject> ListOfObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //popupController.closePopupMap.onClick.AddListener(popupController.OnClickHandlerMap);
        //TutScenario();
    }

    

    void SetHandlers(GameObject gj)
    {
        Button[] buttons = gj.transform.GetComponentsInChildren<Button>();
        foreach (Button btn in buttons)
        {
            switch (btn.name)
            {
                case "LocationButton":
                    btn.onClick.AddListener(LocationHandler);
                    break;
                case "SendButton":
                    btn.onClick.AddListener(SendHandler);
                    break;
                case "OpenButton":
                    btn.onClick.AddListener(OpenHandler);
                    break;
            }
        }
    }

    bool OneTimeStartedSendingMessages = true;
    // Update is called once per frame
    void Update()
    {
        if (DistanceTrigger.StartedSendingMessages && OneTimeStartedSendingMessages)
        {
            OneTimeStartedSendingMessages = false;
            BeginScenario();
        }
    }

    int passCount = 0;
    public void TutScenario()
    {
        switch(passCount)
        {
            case 0:
                AddIncident(ListOfIncidentsTypes[0], "Er zit een kat vast de in boom!", "Kun je de kat uit de boom proberen te lokken met voer?");
                Invoke("TutScenario", 2);
                break;
            case 1:
                AddIncident(ListOfIncidentsTypes[1], "Mijn fiets is gestolen, maar de band ligt er nog.", "Klik <color=#0000FF>hier</color> om aangifte te doen via onze website.");
                Invoke("TutScenario", 2);
                break;
            case 2:
                AddIncident(ListOfIncidentsTypes[2], "Juwelier overvallen #shocking", "De politie is hiervan op de hoogte #Zwolle ^Nick");
                Invoke("TutScenario", 2);
                break;
            case 3:
                AddIncident(ListOfIncidentsTypes[3], "Inbreker gespot in woonwijk, bewoners zijn op vakantie.", "");
                Invoke("TutScenario", 2);
                break;
        }

        passCount++;
    }
    int bsCount = 0;
    void BeginScenario()
    {
        switch (bsCount)
        {
            case 0:
                AddIncident(ListOfIncidentsTypes[0], "Er zit een kat vast de in boom!", "Kun je de kat uit de boom proberen te lokken met voer?");
                Invoke("BeginScenario", 2);
                break;
            case 1:
                AddIncident(ListOfIncidentsTypes[1], "Mijn fiets is gestolen, maar de band ligt er nog.", "Klik <color=#0000FF>hier</color> om aangifte te doen via onze website.");
                Invoke("BeginScenario", 2);
                break;
            case 2:
                AddIncident(ListOfIncidentsTypes[2], "Juwelier overvallen #shocking", "De politie is hiervan op de hoogte #Zwolle ^Nick");
                Invoke("BeginScenario", 2);
                break;
            case 3:
                AddIncident(ListOfIncidentsTypes[3], "Groep jongeren veroorzaken overlast bij bartonio's pizza.", "");
                Invoke("BeginScenario", 2);
                break;
        }

        bsCount++;
    }

    void AddIncident (GameObject Incident, string message, string answer)
    {
        GameObject CopyOf = Instantiate(Incident, Content.transform);
        ListOfObjects.Add(CopyOf);
        SetHandlers(CopyOf);
        TMP_Text[] text = CopyOf.GetComponentsInChildren<TMP_Text>();
        text[0].text = message;
        text[1].text = answer;
    }

    void LocationHandler()
    {
        Debug.Log("Location");
    }

    void SendHandler()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.gameObject.SetActive(false);
        Debug.Log("Send");
    }

    void OpenHandler ()
    {
        OpenIncidentScreen.SetActive(false);
        ChatScreen.SetActive(true);
        EventSystem.current.currentSelectedGameObject.transform.parent.Find("Background").gameObject.GetComponent<Image>().color = ActiveColor;
        Debug.Log("Open");
    }

    public void ResetMK()
    {
        //Remove all incidents
        foreach (GameObject gj in ListOfObjects)
        {
            Destroy(gj);
        }
        ListOfObjects = new List<GameObject>();
        //Disable chat
        ChatScreen.SetActive(false);
        //Enable openincidentview
        OpenIncidentScreen.SetActive(true);
    }
}
