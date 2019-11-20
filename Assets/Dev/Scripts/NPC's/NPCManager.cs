using UnityEngine;

public class NPCManager : MonoBehaviour {

    public GameObject OfficerDefaultLocation;

    [SerializeField]
    private GameObject CheckpointContainer;
    [SerializeField]
    private string NpcPrefabsPath;

    private GameObject Officer;
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

        Officer = GameObject.FindGameObjectsWithTag("Officer")[0];

        // Create all civilians
        for (int i = 0; i < GameManager.amountOfNpcsToSpawn; i++)
            npcMaker.CreateCivilian();
    }

    private void Update()
    {
        if (!OfficerIsDirected && Officer.GetComponent<NPCBehaviour>().agent != null)
        {
            Officer.GetComponent<NPCBehaviour>().MoveToTarget(OfficerDefaultLocation);
            OfficerIsDirected = true;
        }
    }
}