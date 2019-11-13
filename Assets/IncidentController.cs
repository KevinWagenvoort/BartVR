using System.Collections;
using System.Collections.Generic;
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
        GameObject CopyOf = Instantiate(Incident, Content.transform);
        ListOfObjects.Add(CopyOf);
        SetHandlers(CopyOf);
    }

    void CreateScenarioIncident()
    {
        AddIncident(ListOfIncidentsTypes[3]);
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
}
