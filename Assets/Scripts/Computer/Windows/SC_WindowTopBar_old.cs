﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

// This script manage the function of the top bar of the cumputer windows

public class SC_WindowTopBar_old : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private Vector2 offset;
    //public bool IsOpen;
    public float SnapInterval;
    //public Image btnImg;
    //public Text btnText;
    private Animator windowAnim;
    private RectTransform RT;

    private void Start()
    {
        windowAnim = GetComponentInParent<Animator>();
    }

    public void SetWindowFirst()
    {
        transform.parent.SetAsLastSibling();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("being moving " + gameObject.name);
        RT = transform.parent.parent.GetComponent<RectTransform>();
        // position of the mouse
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.parent.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out Vector2 localpoint);

        offset = new Vector2(transform.parent.localPosition.x, transform.parent.localPosition.y) - localpoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("moving " + gameObject.name + " window");

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

    // Top bar buttons functions

    // Close button
    public void CloseWindow()
    {
        transform.parent.gameObject.SetActive(false);
    }

    // Maximize or minimize button
    public void MaximizeWindow()
    {
        /*
        if(IsOpen == true) // Minimize
        {
            //windowContent.SetActive(false);

            //btnImg.color = new Color(0.63f, 1f, 0.46f); // green
            //btnText.text = "+";

            windowAnim.SetTrigger("Min");

            IsOpen = false;
        }
        else // Maximize
        {
            //windowContent.SetActive(true);

            //btnImg.color = new Color(1f, 0.85f, 0.46f); // yellow
            //btnText.text = "-";

            windowAnim.SetTrigger("Max");

            IsOpen = true;
        }
        */
    }
}
