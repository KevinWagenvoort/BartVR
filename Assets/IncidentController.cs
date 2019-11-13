using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncidentController : MonoBehaviour
{
    //public
    public GameObject Content;
    public GameObject ChatScreen;
    public GameObject OpenIncidentScreen;
    public List<GameObject> ListOfIncidentsTypes = new List<GameObject>();

    //private
    private List<GameObject> ListOfIncidents = new List<GameObject>();
    private List<GameObject> ListOfObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        AddIncident(ListOfIncidentsTypes[0]);
        AddIncident(ListOfIncidentsTypes[1]);
        AddIncident(ListOfIncidentsTypes[2]);
        Invoke("CreateScenarioIncident", 5);
        LocationHandler();
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddIncident (GameObject Incident)
    {
        ListOfObjects.Add(Instantiate(Incident, Content.transform));
        SetHandlers(Incident);
    }

    void CreateScenarioIncident()
    {
        AddIncident(ListOfIncidentsTypes[3]);
    }

    void LocationHandler ()
    {
        Debug.Log("Location");
    }

    void SendHandler()
    {
        Debug.Log("Send");
    }

    void OpenHandler ()
    {
        OpenIncidentScreen.SetActive(false);
        ChatScreen.SetActive(true);
        Debug.Log("Open");
    }
}
