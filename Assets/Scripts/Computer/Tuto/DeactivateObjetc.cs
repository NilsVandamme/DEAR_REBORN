using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeactivateObjetc : MonoBehaviour
{
    public GameObject TheObject;

    public void Deactivate()
    {
        TheObject.GetComponent<Button>().interactable = false;
    }
}
