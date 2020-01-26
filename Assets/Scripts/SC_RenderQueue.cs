using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_RenderQueue : MonoBehaviour
{
    public int renderOrder;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.renderQueue = renderOrder;
    }
}
