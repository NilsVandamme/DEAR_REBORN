using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script make the object attached to drag & drop-able

public class SC_DragDropControls : MonoBehaviour
{
    [Header("System values")]

    public float HoveringHeight = -5.25f; // Height at which the object will hover while being dragged
    public float SpeedDivider = 2.5f; // Divide the drag speed
    public float SnapSpeed; // Speed of the snapping movement
    public float TimerDisableHover; // Timer after which the hover is disabled if it fails to get to snap position

    private bool IsSelected; // Is the object being selected ?
    private Vector3 OriginalPosition; // Position where the object is at start. Also where the object will return if it isn't snapped
    private Vector3 SnapPosition; // Position at which the object snaps

    //[Space]
    private GameObject SnapPositionObjectTop; // Which object Top is it snapped to ?
    private GameObject SnapPositionObjectDown; // Which object Down is it snapped to ?    //[Space]
    private bool topSnapped;
    private bool downSnapped;

    //[Space]
    [HideInInspector] public bool IsSnapped; // Is the object snapped ?
    private bool snapMovementActive; // Is the object moving to it's snap ?

    private Vector3 mouseOffset;
    private float mouseZCoord;
    private Rigidbody rig; // Object rigidbidy

    private float timer;

    [HideInInspector] public bool top;
    [HideInInspector] public bool middle;
    [HideInInspector] public bool down;

    [Space]
    public GameObject removeButton;
    public SpriteRenderer backgroundSR;
    public SpriteRenderer colorSR;
    public TextMeshPro textSR;

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

            backgroundSR.sortingOrder = 0;
            colorSR.sortingOrder = 0;
            textSR.sortingOrder = 1;
        }
    }

    // When the mouse is being pressed
    private void OnMouseDown()
    {
        snapMovementActive = false;
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

        // Change the sorting order of the sprites & text to render over all other elements
        if (!IsSnapped)
        {
            backgroundSR.sortingOrder = 2;
            colorSR.sortingOrder = 2;
            textSR.sortingOrder = 3;
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
                if (topSnapped == true)
                {
                    snapMovementActive = true;
                    IsSelected = false;
                    //Debug.Log("snap movement activated");

                    // Give the position to snap to
                    if (SnapPositionObjectTop != null && SnapPositionObjectDown != null)
                    {
                        // Send the paragraph to it's snap zone position
                        SnapPosition = SnapPositionObjectTop.transform.position;
                        IsSnapped = true;

                        // Determine which zone has a paragraph snapped
                        top = false;
                        middle = false;
                        down = false;

                        if (SnapPositionObjectDown.transform.position.z > -3)
                            top = true;
                        else if (SnapPositionObjectDown.transform.position.z > -5)
                            middle = true;
                        else
                            down = true;

                        OnSnap();
                    }
                    else
                    {
                        // Send the paragraph back to the sorter position
                        SnapPosition = OriginalPosition;
                    }                   
                }
                // If the object has no snap point
                else if(SnapPositionObjectTop == null && SnapPositionObjectDown == null) 
                {
                    // Send the paragraph back to the sorter position
                    snapMovementActive = true;
                    IsSelected = false;
                    SnapPosition = OriginalPosition;
                }
                else
                {
                    // Drop the paragraph on the ground
                    if (rig != null)
                        rig.useGravity = true;
                    snapMovementActive = false;
                    IsSelected = false;
                }
            }
        }
    }

    private void OnSnap()
    {
        SC_ParagraphSorter.instance.Affiche(SC_ParagraphSorter.instance.lastParagrapheMove);

        SC_AutoComplete elem = gameObject.GetComponent<SC_AutoComplete>();
        elem.myTextContenue.gameObject.SetActive(true);
        elem.myTextPresentation.gameObject.SetActive(false);

        Animator anim = gameObject.GetComponent<Animator>();
        anim.Play("ScaleUp");
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

                // Raycasts - !!! DONT FORGET TO CHECK IF THE TRANSFORM.GETCHILD MATCH THE HIERARCHY !!! 
                Physics.Raycast(transform.GetChild(2).transform.position, Vector3.down, out RaycastHit topHit, 1000f);
                Physics.Raycast(transform.GetChild(3).transform.position, Vector3.down,out RaycastHit downHit, 1000f);

                // The top raycast hit a snap area ?

                // Is the raycast hitting something ?
                if (Physics.Raycast(transform.GetChild(2).transform.position, Vector3.down,1000f) || Physics.Raycast(transform.GetChild(3).transform.position, Vector3.down, 1000f))
                {
                    //Debug.Log("raycasts spotted an object");
                    // Is the raycast hitting a zone ?
                    if (topHit.transform.gameObject.layer == 8 && downHit.transform.gameObject.layer == 8)
                    {
                        //Debug.Log("raycasts spotted an zone");
                        // Is the raycats hitting a free zone ?
                        if (topHit.transform.gameObject.GetComponent<SC_PaperSnapGrid>().hasSnappedObject == false && downHit.transform.gameObject.GetComponent<SC_PaperSnapGrid>().hasSnappedObject == false) 
                        {
                            //Debug.Log("Top raycast has found a free snap area");
                            // Snap the paragraph to the free zone
                            SnapPositionObjectTop = topHit.transform.gameObject;
                            SnapPositionObjectDown = downHit.transform.gameObject;
                            topSnapped = true;
                        }
                        else
                        {
                            // Send the paragraph back to the sorter position
                            SnapPositionObjectTop = null;
                            SnapPositionObjectDown = null;
                            topSnapped = false;
                        }
                    }
                    else
                    {
                        // Send the paragraph back to the sorter position
                        SnapPositionObjectTop = null;
                        SnapPositionObjectDown = null;
                        topSnapped = false;
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
