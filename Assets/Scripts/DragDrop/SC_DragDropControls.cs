﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script make the object attached to drag & drop-able

public class SC_DragDropControls : MonoBehaviour
{
    [Header("System values")]

    public float HoveringHeight = -5.25f; // Height at which the object will hover while being dragged
    public float SpeedDivider = 2.5f; // Divide the drag speed
    public float SnapSpeed; // Speed of the snapping movement
    public int ParagraphSize; // The number of lines the paragraph contains
    public float TimerDisableHover; // Timer after which the hover is disabled if it fails to get to snap position
    public float FirstLastSnapPositionOffset; // Offset at which the paragraph will snap if it snap only to first or last positions

    private bool IsSelected; // Is the object being selected ?
    private Vector3 OriginalPosition; // Position where the object is at start. Also where the object will return if it isn't snapped

    //[Header("Debug")]

    private Vector3 SnapPosition; // Position at which the object snaps

    //[Space]
    private GameObject SnapPositionObjectOverTop;
    private GameObject SnapPositionObjectTop; // Which object Top is it snapped to ?
    private GameObject SnapPositionObjectDown; // Which object Down is it snapped to ?
    private GameObject SnapPositionObjectUnderDown;

    //[Space]
    private bool overTopSnapped;
    private bool topSnapped;
    private bool downSnapped;
    private bool underDownSnapped;

    //[Space]
    public bool IsSnapped; // Is the object snapped ?
    private bool snapMovementActive; // Is the object moving to it's snap ?

    private Vector3 mouseOffset;
    private float mouseZCoord;
    private Rigidbody rig; // Object rigidbidy
    public GameObject removeButton;

    //[Space]
    private float timer;

    private void Start()
    {
        OriginalPosition = transform.position;
        rig = GetComponent<Rigidbody>();

        removeButton.SetActive(false);
    }

    private void Update()
    {
        // Snap the object to it's target
        if (snapMovementActive == true && Vector3.Distance(transform.position, SnapPosition) > 0.03)
        {
            //Debug.Log("aligning");
            rig.position = Vector3.Lerp(rig.position, SnapPosition, SnapSpeed*Time.deltaTime);

            // Disable the snap movement if the object is blocked too long
            timer += Time.deltaTime;
            if (timer > TimerDisableHover)
            {
                timer = 0;
                snapMovementActive = false;
                rig.useGravity = true;
                if(IsSnapped)
                    removeButton.SetActive(true);
            }
        }
        // Unsnap the object from it's target
        else if(snapMovementActive == true && Vector3.Distance(transform.position, SnapPosition) <= 0.03)
        {
            timer = 0;
            snapMovementActive = false;
            rig.useGravity = true;
            if(IsSnapped)
                removeButton.SetActive(true);
        }
    }

    // When the mouse is being pressed
    private void OnMouseDown()
    {
        SC_StagesAnim.instance.anim.SetTrigger("StartWriting");

        if (enabled && IsSnapped == false)
        {
                IsSelected = true;
                //Debug.Log("MOUSE DOWN");
                mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                mouseZCoord /= SpeedDivider;

                mouseOffset = gameObject.transform.position - GetMouseWorldPos();
                if (rig != null)
                    rig.useGravity = false;
        }
    }

    // When the mouse is being released
    private void OnMouseUp()
    {
        timer = 0;

        if (enabled && IsSnapped == false)
        {
            if (IsSelected == true)
            {
                //Debug.Log("MOUSE UP");

                // If the object has a snap point
                if ((topSnapped == true && downSnapped == true) || (topSnapped == true && SnapPositionObjectDown == null) || (downSnapped == true && SnapPositionObjectTop == null))
                {
                    snapMovementActive = true;
                    IsSelected = false;

                    // Snap the paragraph to the right position if it's on the top or bottom position depending on it's size
                    if (ParagraphSize == 1)
                    {
                        // Give the position to snap to
                        if (SnapPositionObjectTop != null)
                        {
                            SnapPosition = SnapPositionObjectTop.transform.position;
                            IsSnapped = true;
                        }
                        else if (SnapPositionObjectDown != null)
                        {
                            SnapPosition = SnapPositionObjectDown.transform.position;
                            IsSnapped = true;
                        }
                        else
                        {
                            SnapPosition = OriginalPosition;
                        }
                    }
                    else if (ParagraphSize == 2)
                    {
                        // Give the position to snap to
                        if (SnapPositionObjectTop != null && SnapPositionObjectDown != null) // Snap between the two spots
                        {
                            SnapPosition = Vector3.Lerp(SnapPositionObjectTop.transform.position, SnapPositionObjectDown.transform.position, 0.5f);
                            IsSnapped = true;
                        }
                        else if (topSnapped == true && SnapPositionObjectDown == null && overTopSnapped == true && SnapPositionObjectTop.tag != "FirstSnapPosition")
                        { // Snap on the upper part of the paper
                            SnapPosition = SnapPositionObjectTop.transform.position + Vector3.forward * FirstLastSnapPositionOffset;
                            IsSnapped = true;
                        }
                        else if (downSnapped == true && SnapPositionObjectTop == null && underDownSnapped == true && SnapPositionObjectDown.tag != "LastSnapPosition")
                        { // Snap on the lower part of the paper
                            SnapPosition = SnapPositionObjectDown.transform.position + Vector3.back * FirstLastSnapPositionOffset;
                            IsSnapped = true;
                        }
                        else if (topSnapped == true && SnapPositionObjectDown == null && underDownSnapped == false && SnapPositionObjectTop.tag == "FirstSnapPosition")
                        { // Return to original position because there's not enough space
                            SnapPosition = OriginalPosition;
                        }
                        else if (downSnapped == true && SnapPositionObjectTop == null && overTopSnapped == false && SnapPositionObjectDown.tag == "LastSnapPosition")
                        { // Return to original position because there's not enough space
                            SnapPosition = OriginalPosition;
                        }
                        else
                        {
                            SnapPosition = OriginalPosition;
                        }
                    }

                    SC_ParagraphSorter.instance.Affiche(SC_ParagraphSorter.instance.lastParagrapheMove);

                    SC_AutoComplete elem = gameObject.GetComponent<SC_AutoComplete>();
                    elem.myTextContenue.gameObject.SetActive(true);
                    elem.myTextPresentation.gameObject.SetActive(false);

                    Animator anim = gameObject.GetComponent<Animator>();
                    anim.Play("ScaleUp");

                }
                // If the object has no snap point
                else if(SnapPositionObjectTop == null && SnapPositionObjectDown == null) 
                {
                    snapMovementActive = true;
                    IsSelected = false;

                    if (ParagraphSize == 1)
                    {
                        SnapPosition = OriginalPosition;
                    }
                    else if(ParagraphSize == 2)
                    {
                        SnapPosition = OriginalPosition;
                    }
                }
                else
                {
                    if (rig != null)
                        rig.useGravity = true;
                    snapMovementActive = false;
                    IsSelected = false;
                }
            }
        }
    }


    // When the mouse is kept being pressed
    private void OnMouseDrag()
    {
        if (enabled && IsSnapped == false)
        {
            if (IsSelected == true)
            {
                //Debug.Log("MOUSE DRAG");
                // Move the object to mouse position
                if (rig != null)
                    rig.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, Mathf.Lerp(transform.position.y, HoveringHeight, Time.deltaTime * 2), GetMouseWorldPos().z + mouseOffset.z);


                Physics.Raycast(transform.GetChild(1).transform.position, Vector3.down, out RaycastHit topHit, 1000f);
                Physics.Raycast(transform.GetChild(2).transform.position, Vector3.down,out RaycastHit downHit, 1000f);

                Debug.DrawRay(transform.GetChild(1).transform.position, Vector3.down);
                Debug.DrawRay(transform.GetChild(2).transform.position, Vector3.down);

                // The top raycast hit a snap area ?
                if (Physics.Raycast(transform.GetChild(1).transform.position, Vector3.down,1000f))
                {
                    if (topHit.transform.gameObject.layer == 8)
                    {
                        if (topHit.transform.gameObject.GetComponent<SC_PaperSnapGrid>().hasSnappedObject == false)
                        {
                            if (topHit.transform.gameObject != SnapPositionObjectTop && ParagraphSize > 1)
                            {
                                //Debug.Log("Top raycast has found a free snap area");
                                SnapPositionObjectTop = topHit.transform.gameObject;
                                topSnapped = true;
                            }
                            else if (topHit.transform.gameObject != SnapPositionObjectTop && ParagraphSize == 1)
                            {
                                //Debug.Log("Top raycast has found a free snap area");
                                SnapPositionObjectTop = topHit.transform.gameObject;
                                topSnapped = true;
                            }
                        }
                    }
                    else
                    {
                        topSnapped = false;
                        SnapPositionObjectTop = null;
                    }
                }

                if (Physics.Raycast(transform.GetChild(2).transform.position, Vector3.down, 1000f))
                {
                    // The down raycast hit a snap area ?
                    if (downHit.transform.gameObject.layer == 8)
                    {
                        if (downHit.transform.gameObject.GetComponent<SC_PaperSnapGrid>().hasSnappedObject == false)
                        {
                            if (downHit.transform.gameObject != SnapPositionObjectTop && ParagraphSize > 1)
                            {
                                //Debug.Log("Down raycast has found a free snap area");
                                SnapPositionObjectDown = downHit.transform.gameObject;
                                downSnapped = true;
                            }
                            else if (downHit.transform.gameObject != SnapPositionObjectTop && ParagraphSize == 1)
                            {
                                //Debug.Log("Down raycast has found a free snap area");
                                SnapPositionObjectDown = downHit.transform.gameObject;
                                downSnapped = true;
                            }
                        }
                    }
                    else
                    {
                        downSnapped = false;
                        SnapPositionObjectDown = null;
                    }
                }


                // Add more raycasts if the paragraph is larger than 1
                if (ParagraphSize > 1)
                {
                    Physics.Raycast(transform.GetChild(3).transform.position, Vector3.down, out RaycastHit overTopHit, 1000f);
                    Physics.Raycast(transform.GetChild(4).transform.position, Vector3.down, out RaycastHit underDownHit, 1000f);

                    Debug.DrawRay(transform.GetChild(3).transform.position, Vector3.down);
                    Debug.DrawRay(transform.GetChild(4).transform.position, Vector3.down);

                    if (Physics.Raycast(transform.GetChild(3).transform.position, Vector3.down, 1000f))
                    {
                        // The over top raycast hit a snap area ?
                        if (overTopHit.transform.gameObject.layer == 8)
                        {
                            if (overTopHit.transform.gameObject.GetComponent<SC_PaperSnapGrid>().hasSnappedObject == false)
                            {
                                //Debug.Log("overTop raycast has found a free snap area");
                                SnapPositionObjectOverTop = overTopHit.transform.gameObject;
                                overTopSnapped = true;
                            }
                        }
                        else
                        {
                            overTopSnapped = false;
                            SnapPositionObjectOverTop = null;
                        }
                    }


                    if (Physics.Raycast(transform.GetChild(4).transform.position, Vector3.down, 1000f))
                    {
                        // The under down raycast hit a snap area ?
                        if (underDownHit.transform.gameObject.layer == 8)
                        {
                            if (underDownHit.transform.gameObject.GetComponent<SC_PaperSnapGrid>().hasSnappedObject == false)
                            {
                                //Debug.Log("underDown raycast has found a free snap area");
                                SnapPositionObjectUnderDown = underDownHit.transform.gameObject;
                                underDownSnapped = true;
                            }
                        }
                        else
                        {
                            underDownSnapped = false;
                            SnapPositionObjectUnderDown = null;
                        }
                    }
                }
            }
        }
    }

    public void RemoveParagraph()
    {
        gameObject.SetActive(false);
        SC_ParagraphSorter.instance.SnappedParagraphs.Remove(gameObject);

        int temp = (int)gameObject.GetComponent<SC_ParagraphType>().Type;
        SC_ParagraphSorter.instance.Paragraphs[temp].Add(gameObject);
        SC_ParagraphSorter.instance.Affiche(temp);

        SC_AutoComplete elem = gameObject.GetComponent<SC_AutoComplete>();
        elem.myTextContenue.gameObject.SetActive(false);
        elem.myTextPresentation.gameObject.SetActive(true);

        transform.position = OriginalPosition;
        
        removeButton.SetActive(false);
        IsSnapped = false;

    }

    public void GetOriginalSnapPosition() // Set the current position as the default snap position
    {
        OriginalPosition = transform.position;
    }

    // System
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
