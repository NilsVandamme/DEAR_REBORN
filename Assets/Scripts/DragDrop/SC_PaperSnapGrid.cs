using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Position at which the paragraphs will snap on the letter

public class SC_PaperSnapGrid : MonoBehaviour
{
    public bool hasSnappedObject; // Does this snap has an object snapped to it ?
    public GameObject currentSnappedObject; // The object snapped to this point

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Paragraph")
        {
            currentSnappedObject = other.gameObject;
            hasSnappedObject = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Paragraph" && other.gameObject == currentSnappedObject)
        {
            currentSnappedObject = null;
            hasSnappedObject = false;
        }
    }
}
