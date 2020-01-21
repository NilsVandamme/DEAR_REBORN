using UnityEngine;
using UnityEngine.EventSystems;

// This script manage the function of the top bar of the cumputer windows

public class SC_WindowTopBar_old : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private Vector2 offset; // Offset of the mouse comapred to the window center
    public float SnapInterval; // Make the window snap every x units
    private RectTransform RT; // RectTransform of the computer

    private void Start()
    {

    }

    // Put the window in front of all others
    public void SetWindowFirst()
    {
        transform.parent.SetAsLastSibling();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RT = transform.parent.parent.GetComponent<RectTransform>();
        // position of the mouse
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out Vector2 localpoint);

        offset = new Vector2(transform.parent.localPosition.x, transform.parent.localPosition.y) - localpoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // position of the mouse
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out Vector2 localpoint);

        // move the window on the x axis
        if(localpoint.x < RT.sizeDelta.x / 2 && localpoint.x > -(RT.sizeDelta.x / 2))
        {
            // Move the window
            if(SnapInterval>0)
                transform.parent.localPosition = new Vector3(Mathf.Round((localpoint.x + offset.x) /10) *10, transform.parent.localPosition.y, transform.parent.localPosition.z);
            else
                transform.parent.localPosition = new Vector3(localpoint.x + offset.x, transform.parent.localPosition.y, transform.parent.localPosition.z);
        }

        // move the window on the y axis
        if (localpoint.y < RT.sizeDelta.y / 2 && localpoint.y > -(RT.sizeDelta.y / 2))
        {
            if(SnapInterval>0)
                transform.parent.localPosition = new Vector3(transform.parent.localPosition.x, Mathf.Round((localpoint.y + offset.y) /10) *10 , transform.parent.localPosition.z);
            else
                transform.parent.localPosition = new Vector3(transform.parent.localPosition.x, localpoint.y + offset.y, transform.parent.localPosition.z);
        }

    }
}
