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
                case "LocationButton":
                    btn.onClick.AddListener(() => LocationHandler(btn));
                    break;
                case "SendButton":
                    btn.onClick.AddListener(() => SendHandler(btn));
                    break;
                case "OpenButton":
                    btn.onClick.AddListener(() => OpenHandler(btn));
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
            case 0://Facebook
                AddIncident(ListOfIncidentsTypes[0], "Ben vandaag ergens gezakkenrold. Lekker dit!", "Klik <color=#0000FF>hier</color> om aangifte te doen via onze website ");
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[0], "Er zit een kat vast de in boom!");
                Invoke("TutScenario", 2);
                break;//Instagram
            case 1:
                AddIncident(ListOfIncidentsTypes[1], "Mijn fiets is gestolen, maar de band ligt er nog.", "Klik <color=#0000FF>hier</color> om aangifte te doen via onze website.");
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[1], "Mijn fiets is gestolen, maar de band ligt er nog.");
                Invoke("TutScenario", 2);
                break;
            case 2://Twitter
                AddIncident(ListOfIncidentsTypes[2], "Juwelier overvallen #shocking", "De politie is hiervan op de hoogte #Zwolle ^Nick");
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[2], "Juwelier overvallen #shocking");
                Invoke("TutScenario", 2);
                break;
            case 3://Whatsapp
                AddIncident(ListOfIncidentsTypes[3], "Inbreker gespot in woonwijk, bewoners zijn op vakantie.", "");
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
                NotificationsController.SetActiveNotification(NotificationsController.NotificationType[3], message);
                Invoke("BeginScenario", TimeBetweenIncidents);
                break;
            case 4://Instagram
                message = "Foto�s van vernielde bankjes in binnenstad Zwolle";
                answer = "Wat is de locatie van deze bankjes? Dan kan de gemeente het oplossen.";
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

    void LocationHandler(Button btn)
    {
        Debug.Log("Location");
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
        }
        btn.transform.parent.Find("Background").GetComponent<Image>().color = ActiveColor;
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