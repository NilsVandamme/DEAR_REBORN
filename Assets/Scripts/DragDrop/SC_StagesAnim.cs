using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_StagesAnim : MonoBehaviour
{
    public static SC_StagesAnim instance;

    public Animator anim;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
}
