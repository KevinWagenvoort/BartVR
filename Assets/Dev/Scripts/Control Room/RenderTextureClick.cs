using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RenderTextureClick : MonoBehaviour
{
    public Camera mapCamera;
    public Camera canvasCamera;
    public Canvas canvas;
    public GameObject pizzaLocation, tutLocation, NeighbourhoodApp;

    //Send officer
    public GameObject SendPopup;
    public bool? send;
    public Button yes;
    public Button no;

    [System.NonSerialized]
    public bool ShouldInvoke = false;

    //Private
    private Vector2 localpoint;
    private RaycastHit hit;
    private GameObject SelectedOfficer;
    private Vector3 DefaultScale = new Vector3(1, 1, 1);
    private Vector3 SelectedScale = new Vector3(2, 2, 2);
    private NeighbourhoodAppScript NeighbourhoodAppScript;

    // Start is called before the first frame update
    void Start()
    {
        NeighbourhoodAppScript = NeighbourhoodApp.GetComponent<NeighbourhoodAppScript>();

        yes.onClick.AddListener(OnClickHandlerYes);
        no.onClick.AddListener(OnClickHandlerNo);
    }

    //button yes to send police
    void OnClickHandlerYes()
    {
        if (DistanceTrigger.VandalismHasHappend)
        {
            if (ShouldInvoke)
            {
                NeighbourhoodAppScript.Scenario();
            }
            SelectedOfficer.GetComponentInParent<NPCBehaviour>().MoveToTarget(pizzaLocation);
        } else
        {
            if (ShouldInvoke)
            {
                NeighbourhoodAppScript.Tutorial();
            }
            SelectedOfficer.GetComponentInParent<NPCBehaviour>().MoveToTarget(tutLocation);
        }

        SelectedOfficer.transform.localScale = DefaultScale;
        SendPopup.SetActive(false);
        ShouldInvoke = false;
    }

    //button no to not send police
    void OnClickHandlerNo()
    {
        SelectedOfficer.transform.localScale = DefaultScale;
        SendPopup.SetActive(false);
    }
    /// <summary>
    /// </summary>
    /// <param name="newSelectedOfficer">Has to be OfficerIcon</param>
    public void SelectOfficer(GameObject newSelectedOfficer)
    {
        if (SelectedOfficer != null)//Check if there was an old officer
        {
            SelectedOfficer.transform.localScale = DefaultScale;
        }

        SelectedOfficer = newSelectedOfficer;
        SelectedOfficer.transform.localScale = SelectedScale;
        SendPopup.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            PointerEventData cursor = new PointerEventData(EventSystem.current);
            cursor.position = Input.mousePosition;
            List<RaycastResult> objectsHit = new List<RaycastResult>();
            EventSystem.current.RaycastAll(cursor, objectsHit);
            int count = objectsHit.Count;

            foreach(RaycastResult result in objectsHit)
            {
                if (result.gameObject.name == "2DMap")
                {
                    RectTransform rt = result.gameObject.GetComponent<RectTransform>();
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, canvasCamera, out localpoint);

                    Vector2 normalizedPoint = Rect.PointToNormalized(rt.rect, localpoint);

                    Ray ray = mapCamera.ViewportPointToRay(normalizedPoint);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.name == "OfficerIcon")
                        {
                            SelectOfficer(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }
}
