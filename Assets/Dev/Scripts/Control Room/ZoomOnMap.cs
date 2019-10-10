// https://forum.unity.com/threads/zoom-in-out-on-scrollrect-content-image.284655/
// Thanks SomeVVhIteGuy!

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomOnMap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Camera canvasCamera;
    [SerializeField] float startSize = 1;
    [SerializeField] float minSize = 0.75f;
    [SerializeField] float maxSize = 1;

    [SerializeField] private float zoomRate = 5;
    private bool isOver = false;

    Vector3 difference;
    Vector3 mousePos;

    private void Update()
    {
        float scrollWheel = -Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0 && isOver)
        {
            ChangeZoom(scrollWheel);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }

    private void ChangeZoom(float scrollWheel)
    {
        PointerEventData cursor = new PointerEventData(EventSystem.current);
        cursor.position = Input.mousePosition;
        List<RaycastResult> objectsHit = new List<RaycastResult>();
        EventSystem.current.RaycastAll(cursor, objectsHit);
        Vector2 localPoint;
        foreach (RaycastResult result in objectsHit)
        {
            if (result.gameObject.name == "2DMap")
            {
                RectTransform rt = result.gameObject.GetComponent<RectTransform>();
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, canvasCamera, out localPoint);
                mousePos = localPoint;
                mousePos.z = 0;
            }
        }
        float rate = 1 + zoomRate * Time.unscaledDeltaTime;
        if (scrollWheel > 0 && transform.localScale.y > minSize)
        {
            SetZoom(Mathf.Clamp(transform.localScale.y / rate, minSize, maxSize));
            difference = transform.localPosition - mousePos;
            difference.z = 0;
            transform.localPosition = mousePos + (difference * 0.9F);
        }
        else if (scrollWheel < 0 && transform.localScale.y < maxSize)
        {
            SetZoom(Mathf.Clamp(transform.localScale.y * rate, minSize, maxSize));
            difference = transform.localPosition - mousePos;
            difference.z = 0;
            transform.localPosition = mousePos + (difference * 1.11F);
        }
    }

    private void SetZoom(float targetSize)
    {
        transform.localScale = new Vector3(targetSize, targetSize, 1);
    }
}