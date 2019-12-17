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
    public GameObject NeighbourhoodApp;
    public GameObject ChatTut;
    public Color ActiveColor;
    public GameObject PhoneNotifications;
    public GameObject incidentPopup;

    public List<GameObject> ListOfIncidentsTypes = new List<GameObject>();

    //private
    private List<GameObject> ListOfIncidents = new List<GameObject>();
    private List<GameObject> ListOfObjects = new List<GameObject>();
    private NeighbourhoodAppScript NeighbourhoodAppScript;

    private NotificationsController NotificationsController;

    // Start is called before the first frame update
    void Start()
    {
        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();
        NotificationsController = PhoneNotifications.GetComponent<NotificationsController>();
    }

    

    void SetHandlers(GameObject gj)
    {
        Button[] buttons = gj.transform.GetComponentsInChildren<Button>();
        foreach (Button btn in buttons)
        {
            switch (btn.name)
            {
                case "SendButton":
                    btn.onClick.AddListener(() => SendHandler(btn));
                    break;
                case "OpenButton":
                    btn.onClick.AddListener(() => OpenHandler(btn));
                    break;
                case "CallButton":
                    btn.onClick.AddListener(() => SendHandler(btn));
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
        string message, answer;
        switch (passCount)
        {
            case 0://Instagram
                message = "Mijn fiets is gestolen, maar de band ligt er nog.";
                answer = "Klik <color=#0000FF>hier</color> om aangifte te doen via onze website.";
                AddIncident(ListOfIncidentsTypes[1], message, answer);
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[1], message);
                Invoke("TutScenario", 2);
                break;
            case 1://Facebook
                message = "Ben vandaag ergens gezakkenrold. Lekker dit!";
                answer = "Klik <color=#0000FF>hier</color> om aangifte te doen via onze website.";
                AddIncident(ListOfIncidentsTypes[0], message, answer);
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[0], message);
                Invoke("TutScenario", 2);
                break;
            case 2://Call
                message = "Gebeld door:\n088-469-9911";
                answer = "";
                AddIncident(ListOfIncidentsTypes[4], message, answer);
                Invoke("TutScenario", 2);
                break;
            case 3://Twitter
                message = "Juwelier overvallen #shocking";
                answer = "De politie is hiervan op de hoogte #Zwolle ^Nick";
                AddIncident(ListOfIncidentsTypes[2], message, answer);
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[2], message);
                Invoke("TutScenario", 2);
                break;
            case 4://Whatsapp
                message = "Inbreker gespot in woonwijk, bewoners zijn op vakantie.";
                answer = "";
                AddIncident(ListOfIncidentsTypes[3], message, answer);
                Invoke("TutScenario", 2);
                break;
            case 5://Twitter
                message = "Gebroken glas op straat #Amundsenstraat";
                answer = "De gemeente zal het rond 15:30 komen opruimen";
                AddIncident(ListOfIncidentsTypes[2], message, answer);
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[2], message);
                Invoke("TutScenario", 2);
                break;
            case 6://Facebook
                message = "Bushokje Yoghurtstraat is gevandaliseerd. Wat een zooi weer.";
                answer = "Bedankt voor de melding. De gemeente komt vanmiddag kijken.";
                AddIncident(ListOfIncidentsTypes[0], message, answer);
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[0], message);
                Invoke("TutScenario", 2);
                break;
        }

        passCount++;
    }
    int bsCount = 0;
    void BeginScenario()
    {
        string message, answer;
        int TimeBetweenIncidents = 3;
        switch (bsCount)
        {
            case 0://Facebook
                message = "Iemand rijdt met zijn auto door de binnenstad van Zwolle. Hartstikke verboden!!!";
                answer = "We sturen er gelijk iemand op af.";
                AddIncident(ListOfIncidentsTypes[0], message, answer);
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[0], message);
                Invoke("BeginScenario", TimeBetweenIncidents);
                break;
            case 1://Instagram
                message = "Ernstig ongeluk op de hortensiastraat!";
                answer = "We zijn ervan op de hoogte en de politie en ambulance is al onderweg";
                AddIncident(ListOfIncidentsTypes[1], message, answer);
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[1], message);
                Invoke("BeginScenario", TimeBetweenIncidents);
                break;
            case 2://Twitter
                message = "#Overval door grote groep mannen met messen #Rotterdam. Bende plundert sportwinkel";
                answer = "Er is al aangifte gedaan. Klik hier om een verklaring af te leggen #Politie #WijLossenHetOp";
                AddIncident(ListOfIncidentsTypes[2], message, answer);
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[2], message);
                Invoke("BeginScenario", TimeBetweenIncidents);
                break;
            case 3://Whatsapp
                message = "Groep jongeren veroorzaken overlast bij bartonio's pizza.";
                answer = "";
                AddIncident(ListOfIncidentsTypes[3], message, answer);
                Invoke("BeginScenario", TimeBetweenIncidents);
                break;
            case 4://Call
                message = "Gebeld door:\n088-469-9911";
                answer = "";
                AddIncident(ListOfIncidentsTypes[4], message, answer);
                Invoke("BeginScenario", TimeBetweenIncidents);
                break;
            case 5://Instagram
                message = "Berg afval op Bacadistraat. Wat een stank!";
                answer = "Bedankt voor de melding. De gemeente is op de hoogte gesteld.";
                AddIncident(ListOfIncidentsTypes[1], message, answer);
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[1], message);
                Invoke("BeginScenario", TimeBetweenIncidents);
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

    void SendHandler(Button btn)
    {
        btn.transform.parent.gameObject.SetActive(false);
    }

    void OpenHandler (Button btn)
    {
        OpenIncidentScreen.SetActive(false);
        ChatScreen.SetActive(true);
        if (DistanceTrigger.TutorialControlRoomIsDone)
        {
            NeighbourhoodAppScript.Scenario();
        } else
        {
            ChatTut.SetActive(true);
            // GameObject incidentpopup close
            incidentPopup.SetActive(false);
        }
        btn.transform.parent.Find("Background").GetComponent<Image>().color = ActiveColor;
        btn.enabled = false;
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
