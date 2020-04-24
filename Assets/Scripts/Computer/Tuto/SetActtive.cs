using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActtive : MonoBehaviour
{
    public GameObject Object;

    public void UseSetActive()
    {
        Object.SetActive(false);
    }
}
