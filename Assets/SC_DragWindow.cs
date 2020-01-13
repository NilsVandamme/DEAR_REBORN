using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SC_DragWindow : MonoBehaviour
{

    public bool isDragging;
    public Camera UI_Camera;
    private Vector3 dist;
    private float posX, posY;
    // Start is called before the first frame update
    void Start()
    {
        isDragging = false;
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        isDragging = false;
        dist = UI_Camera.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
    }

    public void OnMouseDrag()
    {
        isDragging = true;
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
        Vector3 worldPos = UI_Camera.ScreenToWorldPoint(curPos);
    }
}
