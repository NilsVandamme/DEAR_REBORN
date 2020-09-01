using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_ArrowLongMail : MonoBehaviour
{
    public GameObject ArrowUp, ArrowDown;
    public float lerpDuration;
    private Scrollbar scrollbar;
    private bool goUp, goDown;
    private float currentValue, aimedValue;
    // Start is called before the first frame update
    void Start()
    {
        scrollbar = gameObject.GetComponent<Scrollbar>();
    }

    public void GoUp()
    {
        scrollbar.value = currentValue;
        StartCoroutine(MoveToPosition(1f, lerpDuration));
        goDown = false;
        goUp = true;  
    }

    public void GoDown()
    {
        scrollbar.value = currentValue;
        StartCoroutine(MoveToPosition(0f, lerpDuration));
        goUp = false;
        goDown = true;
    }

    IEnumerator MoveToPosition(float value, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            scrollbar.value = Mathf.Lerp(currentValue, value, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        scrollbar.value = value;
    }

    public void Update()
    {
     
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       // Scrollbar scrollbar = gameObject.GetComponent<Scrollbar>();
       /*
        if (scrollbar.value >= 1f)
        {
            ArrowUp.SetActive(true);
        }
        */
        if (scrollbar.value <= 0.01f)
        {
            ArrowDown.SetActive(true);
            ArrowUp.SetActive(false);
        }

        else
        {
            ArrowUp.SetActive(true);
            ArrowDown.SetActive(false);
        }

        
    }
}
