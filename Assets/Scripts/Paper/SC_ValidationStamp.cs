using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_ValidationStamp : MonoBehaviour
{
    public static SC_ValidationStamp instance;

    public bool ValidationState;
    public bool canDoValidationState;
    [Space]
    public Collider validationStampCollider;
    public Animator anim;
    public GameObject animObj;
    [Space]
    public List<Button> paragraphSorterBtn;
    public List<Button> dragDropWindowsbtn;
    public List<Rigidbody> dragDropWindowsrb;
    public List<Rigidbody> dragDropWords;

    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }


    // Active le tampon de validation
    private void OnMouseDown()
    {
        if (canDoValidationState)
        {
            Debug.Log("validation activated");
            // Active le collider
            validationStampCollider.gameObject.SetActive(true);
            // change le curseur
            SC_GM_Cursor.gm.changeToValidationCursor();

            // Désactive les boutons du paragraph sorter
            for (int i = 0; i < paragraphSorterBtn.Count; i++)
            {
                paragraphSorterBtn[i].enabled = false;
            }

            // Désactive les drag&drop et les boutons remove
            for (int i = 0; i < dragDropWindowsbtn.Count; i++)
            {
                dragDropWindowsbtn[i].enabled = false;
            }
            for (int k = 0; k < dragDropWindowsrb.Count; k++)
            {
                dragDropWindowsrb[k].constraints = RigidbodyConstraints.FreezeAll;
            }
            for (int j = 0; j < dragDropWords.Count; j++)
            {
                dragDropWords[j].constraints = RigidbodyConstraints.FreezeAll;
            }

            //Finish the anim
            animObj.SetActive(false);

            // Active validation state
            ValidationState = true;
        }
    }
}
