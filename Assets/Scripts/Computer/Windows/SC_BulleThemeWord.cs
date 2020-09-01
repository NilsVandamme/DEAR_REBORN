using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulleThemeWord : MonoBehaviour
{
    public Vector3 LeftPosition, MiddlePosition, RightPosition;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

public void GoToLeft()
    {
        anim.Play("BulleLeft");
    }

    public void GotoMiddle()
    {
        anim.Play("BulleMiddle");
    }

    public void GoToRight()
    {
        anim.Play("BulleRight");
    }

    public void MakeWordAppear()
    {
        //pop le mot à sa position
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
