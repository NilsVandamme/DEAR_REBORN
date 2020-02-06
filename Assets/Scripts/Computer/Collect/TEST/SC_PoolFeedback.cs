using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PoolFeedback : MonoBehaviour
{
    public static SC_PoolFeedback instance;

    public GameObject[] poolCL;
    public GameObject[] poolStamps;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void LaunchFeedbackForCL()
    {
        for(int i = 0; i < poolCL.Length; i++)
        {
            if(poolCL[i].GetComponent<SC_CollectedCLFeedbackUI>().moving == false)
            {
                poolCL[i].GetComponent<SC_CollectedCLFeedbackUI>().TargetPosition = SC_Collect.instance.imagesDiodes[SC_GM_Local.gm.numberOfCLRecover-1].transform;
                poolCL[i].GetComponent<SC_CollectedCLFeedbackUI>().StartFeedback();
                return;
            }
        }
    }
}