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

    // Start is called before the first frame update
    void Start()
    {

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
                    Vector2 localpoint;
                    RectTransform rt = result.gameObject.GetComponent<RectTransform>();
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, canvasCamera, out localpoint);

                    Vector2 normalizedPoint = Rect.PointToNormalized(rt.rect, localpoint);

                    RaycastHit hit;
                    Ray ray = mapCamera.ViewportPointToRay(normalizedPoint);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.name == "OfficerIcon")
                        {
                            Debug.Log("Officer selected");
                        }
                    }
                }
            }
        }
    }
}
