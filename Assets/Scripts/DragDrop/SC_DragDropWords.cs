using System.Collections;
using TMPro;
using UnityEngine;

public class SC_DragDropWords : MonoBehaviour
{
    public bool IsSelected;
    public bool Snapped;
    public bool SnapMovement;
    public float SpeedDivider;
    public float HoveringHeight;
    public float SnapSpeed;
    public TextMeshPro text;
    public GameObject ParticleWin;


    private Vector3 OriginalPosition;
    private Vector3 mouseOffset;
    private float mouseZCoord;
    private Rigidbody rig; // Object rigidbidy
    private SC_AutoComplete autoc;
    private SC_DragDropControls ddcontrols;
    private Vector3 SnapPosition;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        OriginalPosition = transform.position;
        rig = GetComponent<Rigidbody>();
        animator = this.gameObject.GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SnapMovement)
        {
            if (Vector3.Distance(transform.position, SnapPosition) > 0.03)
            {
                //Debug.Log("aligning");
                rig.position = Vector3.Lerp(rig.position, SnapPosition, SnapSpeed * Time.deltaTime);
            }
            else if (Vector3.Distance(transform.position, SnapPosition) <= 0.03)
            {
                SnapMovement = false;
                rig.useGravity = true;

            }
        }
    }

    private void OnMouseDown()
    {
        IsSelected = true;

        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseZCoord /= SpeedDivider;

        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
        if (rig != null)
            rig.useGravity = false;

        SC_GM_WheelToLetter.instance.OnClickButtonAutoComplete(text);
    }

    private void OnMouseUp()
    {
        if (Snapped && ddcontrols.IsSnapped)
        {
            Instantiate(ParticleWin, transform.position, Quaternion.identity);
            autoc.OnClick(animator);

            StartCoroutine("GoToPlace");
        }
    }

    private IEnumerator GoToPlace()
    {
        yield return new WaitForSeconds(1);

        // Send the element back to it's original position
        SnapPosition = OriginalPosition;
        SnapMovement = true;
    }

    private void OnMouseDrag()
    {
        if (IsSelected)
        {
            if (rig != null)
                rig.position = new Vector3(GetMouseWorldPos().x + mouseOffset.x, Mathf.Lerp(transform.position.y, HoveringHeight, Time.deltaTime * 2), GetMouseWorldPos().z + mouseOffset.z);

            Physics.Raycast(transform.GetChild(1).transform.position, Vector3.down, out RaycastHit topHit, 1000f);
            Physics.Raycast(transform.GetChild(2).transform.position, Vector3.down, out RaycastHit downHit, 1000f);

            Debug.DrawRay(transform.GetChild(1).transform.position, Vector3.down);
            Debug.DrawRay(transform.GetChild(2).transform.position, Vector3.down);

            if(Physics.Raycast(transform.GetChild(1).transform.position, Vector3.down, 1000f) || Physics.Raycast(transform.GetChild(2).transform.position, Vector3.down, 1000f))
            {
                if(topHit.transform.tag == "Paragraph" && downHit.transform.tag == "Paragraph")
                {
                    autoc = topHit.transform.GetComponent<SC_AutoComplete>();
                    ddcontrols = topHit.transform.GetComponent<SC_DragDropControls>();
                    Snapped = true;

                    
                }
                else
                {
                    Snapped = false;
                }
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
