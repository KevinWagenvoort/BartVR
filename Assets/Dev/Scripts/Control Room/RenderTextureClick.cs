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
    public GameObject pizzaLocation;

    public GameObject selectedPolice;
    public bool? send;

    public Button yes;
    public Button no;

    Vector2 localpoint;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        yes.onClick.AddListener(OnClickHandlerYes);
        no.onClick.AddListener(OnClickHandlerNo);
    }

    //button yes to send police
    void OnClickHandlerYes()
    {
        this.send = true;
    }

    //button no to not send police
    void OnClickHandlerNo()
    {
        this.send = false;
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
                            Debug.Log("Officer selected");

                            selectedPolice.SetActive(true);                           
                        }
                    }
                }
            }

            //buttons on 'popup' to send police to location or not
            if (send == true)
            {
                hit.transform.gameObject.GetComponent<NPCBehaviour>().MoveToTarget(pizzaLocation);
                selectedPolice.SetActive(false);
                send = null;
            }
            else if (send == false)
            {
                Debug.Log("Officer unselected");
                selectedPolice.SetActive(false);
                send = null;
            }

        }
    }
}
