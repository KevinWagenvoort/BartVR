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
    public GameObject pizzaLocation, tutLocation;

    //Send officer
    public GameObject SendPopup;
    public bool? send;
    public Button yes;
    public Button no;

    //Private
    private Vector2 localpoint;
    private RaycastHit hit;
    private GameObject SelectedOfficer;
    private Vector3 DefaultScale = new Vector3(1, 1, 1);
    private Vector3 SelectedScale = new Vector3(2, 2, 2);

    // Start is called before the first frame update
    void Start()
    {
        yes.onClick.AddListener(OnClickHandlerYes);
        no.onClick.AddListener(OnClickHandlerNo);
    }

    //button yes to send police
    void OnClickHandlerYes()
    {
        if (DistanceTrigger.VandalismHasHappend)
        {
            SelectedOfficer.GetComponentInParent<NPCBehaviour>().MoveToTarget(pizzaLocation);
        } else
        {
            SelectedOfficer.GetComponentInParent<NPCBehaviour>().MoveToTarget(tutLocation);
        }

        SelectedOfficer.transform.localScale = DefaultScale;
        SendPopup.SetActive(false);
    }

    //button no to not send police
    void OnClickHandlerNo()
    {
        SelectedOfficer.transform.localScale = DefaultScale;
        SendPopup.SetActive(false);
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
                            if (SelectedOfficer != null)//Check if there was an old officer
                            {
                                SelectedOfficer.transform.localScale = DefaultScale;
                            }

                            SelectedOfficer = hit.collider.gameObject;
                            SelectedOfficer.transform.localScale = SelectedScale;
                            SendPopup.SetActive(true);                           
                        }
                    }
                }
            }
        }
    }
}
