using UnityEngine;

public class NPCManager : MonoBehaviour {

    public GameObject OfficerDefaultLocationScenario, OfficerDefaultLocationTut;

    [SerializeField]
    private GameObject CheckpointContainer;
    [SerializeField]
    private string NpcPrefabsPath;

    private GameObject Officer0, Officer1;
    private bool OfficerIsDirected = false;
    string officerModelsPath = "Officers";

    // Use this for initialization
    void Awake() {
        NPCMaker npcMaker = GetComponent<NPCMaker>();
        npcMaker.Setup(NpcPrefabsPath, CheckpointContainer);

        // Create suspect
        // npcMaker.CreateSuspect();

        for(int i = 0; i < 5; i++)
        {
            npcMaker.CreateOfficer(officerModelsPath);
        }

        Officer0 = GameObject.FindGameObjectsWithTag("Officer")[0];
        Officer1 = GameObject.FindGameObjectsWithTag("Officer")[1];

        // Create all civilians
        for (int i = 0; i < GameManager.amountOfNpcsToSpawn; i++)
            npcMaker.CreateCivilian();
    }

    private void FixedUpdate()
    {
        if (!OfficerIsDirected && Officer0.GetComponent<NPCBehaviour>().agent != null)
        {
            //Scenario
            Officer0.GetComponent<NPCBehaviour>().agent.Warp(OfficerDefaultLocationScenario.transform.position);
            Officer0.GetComponent<NPCBehaviour>().MoveToTarget(OfficerDefaultLocationScenario);

            //Tut
            Officer1.GetComponent<NPCBehaviour>().agent.Warp(OfficerDefaultLocationTut.transform.position);
            Officer1.GetComponent<NPCBehaviour>().MoveToTarget(OfficerDefaultLocationTut);

            OfficerIsDirected = true;
        }
    }
}