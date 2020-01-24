using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CollectedCLFeedback : MonoBehaviour
{
    public Transform CollectPosition;
    public float StartAnimDistance;
    private Animator anim;
    private bool Moving;
    private bool animPlaying;
    private float timer;
    public float WaitTime;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    public void StartFeedback(Vector2 vect)
    {
        transform.position = new Vector3(vect.x, vect.y, transform.position.z) ;
        Moving = true;

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log(GetMouseWorldPos());
            StartFeedback(GetMouseWorldPos());
        }



        if (Moving)
        {
            timer += Time.deltaTime;
            if (timer > WaitTime)
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -0.5f), CollectPosition.position, 0.03f);


                if (Vector2.Distance(transform.position, CollectPosition.position) < StartAnimDistance && Vector2.Distance(transform.position, CollectPosition.position) >= StartAnimDistance - 1)
                {
                    if (!animPlaying)
                    {
                        animPlaying = true;
                    }
                }

                if (Vector2.Distance(transform.position, CollectPosition.position) < 0.5f)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(false);
                    timer = 0;
                    Moving = false;
                }
            }
        }
           
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePoint)*-16;
    }
}
