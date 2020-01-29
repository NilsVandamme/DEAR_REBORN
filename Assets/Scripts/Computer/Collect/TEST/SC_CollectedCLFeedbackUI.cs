using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_CollectedCLFeedbackUI : MonoBehaviour
{
    public static SC_CollectedCLFeedbackUI instance;

    public Transform TargetPosition;
    public float startAnimDistance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void StartFeedback(Vector2 vect)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
