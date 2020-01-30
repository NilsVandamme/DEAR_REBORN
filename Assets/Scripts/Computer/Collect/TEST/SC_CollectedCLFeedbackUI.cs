﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_CollectedCLFeedbackUI : MonoBehaviour
{
    public static SC_CollectedCLFeedbackUI instance;

    public Transform TargetPosition;
    public float startAnimDistance;
    public float MousePosMult;
    private float timer;
    public float WaitTime;

    private bool moving;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StartFeedback()
    {
        transform.position = new Vector3(GetMouseWorldPos().x, GetMouseWorldPos().y, -0.5f);
        transform.position = new Vector3(GetMouseWorldPos().x, GetMouseWorldPos().y, -0.5f);

        moving = true;
        timer = 0;

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void StopFeedback()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        timer = 0;
        moving = false;
        SC_Collect.instance.Recolt();
    }

    // Update is called once per frame
    void Update()
    {

        if (moving)
        {
            timer += Time.deltaTime;
            if(timer > WaitTime)
            {
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -0.5f), new Vector3(TargetPosition.position.x, TargetPosition.position.y, -0.5f) , 0.03f);

                if(Vector2.Distance(transform.position, TargetPosition.position) < startAnimDistance)
                {
                    StopFeedback();
                }
            }
        }
    }

    public Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = transform.position.z;

        return Camera.main.ScreenToWorldPoint(mousePoint) * MousePosMult;
    }
}
