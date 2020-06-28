using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_TimbreDragDrop : MonoBehaviour
{
    public bool IsSelected;
    public bool Snapped;
    public bool SnapMovement;
    public float SpeedDivider = 1.1f;
    public float HoveringHeight = -3;
    public float SnapSpeed = 3;

    public string triggerName;

    [Space]
    private Vector3 OriginalPosition;
    private Vector3 mouseOffset;
    private float mouseZCoord;
    private Rigidbody rig; // Object rigidbidy
    private Vector3 SnapPosition;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        OriginalPosition = transform.position;
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SnapMovement)
        {
            if (Vector3.Distance(transform.position, SnapPosition) > 0.03f)
            {
                //Debug.Log("aligning");
                rig.position = Vector3.Lerp(rig.position, SnapPosition, SnapSpeed * Time.deltaTime);
            }
            else if (Vector3.Distance(transform.position, SnapPosition) <= 0.03f)
            {
                Debug.Log("hehey");
                if (Snapped)
                {  
                    SC_TimbreChooser.instance.selectedStamp = this.gameObject;
                    SC_TimbreChooser.instance.ChooseStamp();
                    SC_TimbreChooser.instance.anim.SetTrigger(triggerName);
                }

                rig.useGravity = true;
                SnapMovement = false;
            }
        }
    }

    private void OnMouseDown()
    {
        IsSelected = true;
        SnapMovement = false;

        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseZCoord /= SpeedDivider;

        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
        if (rig != null)
            rig.useGravity = false;

        //SC_GM_SoundManager.instance.PlaySound("Redaction_PickUpItem");
    }

    private void OnMouseUp()
    {
        if (Snapped)
        {
            SnapMovement = true;
            //SC_GM_SoundManager.instance.PlaySound("Redaction_PlaceWordWriteSound");
        }
        else
        {
            // Send the element back to it's original position
            SnapPosition = OriginalPosition;
            SnapMovement = true;

            //SC_GM_SoundManager.instance.PlaySound("Redaction_Swipe_Papier_Aigu");
        }
    }

    private void OnMouseDrag()
    {
        if (IsSelected)
        {
            // Move the object to mouse position
            if (rig != null)
                rig.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, Mathf.Lerp(transform.position.y, HoveringHeight, Time.deltaTime * 2), GetMouseWorldPos().z + mouseOffset.z);

            // Raycasts - !!! DONT FORGET TO CHECK IF THE TRANSFORM.GETCHILD MATCH THE HIERARCHY !!! 
            Physics.Raycast(transform.position, Vector3.down, out RaycastHit topHit, 1000f);
            //Debug.DrawRay(transform.position, Vector3.down, Color.red);

            // Is the raycast hitting something ?
            if (Physics.Raycast(transform.position, Vector3.down, 1000f))
            {
                // Is the raycast hitting a paragraph
                if (topHit.transform.tag == "StampZone")
                {
                    // Snap the word to the paragraph
                    SnapPosition = topHit.transform.position;
                    Snapped = true;
                }
                else
                {
                    Snapped = false;
                }
            }
            else
            {
                Snapped = false;
            }
        }
    }


    // System
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
