// https://forum.unity.com/threads/zoom-in-out-on-scrollrect-content-image.284655/
// Thanks SomeVVhIteGuy!

using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomOnMap : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
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

        float rate = 1 + zoomRate * Time.unscaledDeltaTime;
        if (scrollWheel > 0 && transform.localScale.y > minSize)
        {
            mousePos = Input.mousePosition;
            SetZoom(Mathf.Clamp(transform.localScale.y / rate, minSize, maxSize));
            difference = transform.position - mousePos;
            transform.position = mousePos + (difference * 0.9F);
        }
        else if (scrollWheel < 0 && transform.localScale.y < maxSize)
        {
            mousePos = Input.mousePosition;
            SetZoom(Mathf.Clamp(transform.localScale.y * rate, minSize, maxSize));
            difference = transform.position - mousePos;
            transform.position = mousePos + (difference * 1.11F);
        }
    }

    private void SetZoom(float targetSize)
    {
        transform.localScale = new Vector3(targetSize, targetSize, 1);
    }
}