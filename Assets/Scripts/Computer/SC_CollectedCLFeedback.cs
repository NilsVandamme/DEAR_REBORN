using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CollectedCLFeedback : MonoBehaviour
{
    public Transform CollectPosition;
    public float StartAnimDistance;
    private Animator anim;
    private bool Moving;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartFeedback(Vector2 vect)
    {
        transform.position = vect;
        Moving = true;
        
    }

    private void Update()
    {
        if (Moving)
        {
            if (Vector2.Distance(transform.position, CollectPosition.position) > StartAnimDistance)
            {
                transform.position = Vector2.Lerp(transform.position, CollectPosition.position, Time.deltaTime);
            }
            else
            {
                //Start anim
                anim.SetTrigger("");
                transform.position = Vector2.Lerp(transform.position, CollectPosition.position, Time.deltaTime);
            }

            if (Vector2.Distance(transform.position, CollectPosition.position) > 0.1f)
            {
                Moving = false;
            }
        }
    }
}
