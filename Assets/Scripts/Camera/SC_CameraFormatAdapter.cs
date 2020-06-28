using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SC_CameraFormatAdapter : MonoBehaviour
{

    public CinemachineVirtualCamera ComputerCamera;
    public CinemachineVirtualCamera DeskCamera;

    private float screenRatio;

    private float width;
    private float height;

    private void Awake()
    {
        width = Screen.width;
        height = Screen.height;
        Debug.Log("res = " + Screen.width + "/" + Screen.height);

        screenRatio = width / height;
        Debug.Log("aspect ratio = " + screenRatio);


        if (screenRatio <= 1.25f) // 5:4 format
        {
            Debug.Log("aspect ratio is 5:4");
            ComputerCamera.m_Lens.FieldOfView = 53;
            DeskCamera.m_Lens.FieldOfView = 59;
        }
        else if (screenRatio <= 1.4f) // 4:3 format
        {
            Debug.Log("aspect ratio is 4:3");
            ComputerCamera.m_Lens.FieldOfView = 49;
            DeskCamera.m_Lens.FieldOfView = 56;
        }
        else if (screenRatio <= 1.55f) // 3:2 format
        {
            Debug.Log("aspect ratio is 3:2");
            ComputerCamera.m_Lens.FieldOfView = 44;
            DeskCamera.m_Lens.FieldOfView = 51;
        }
        else if (screenRatio <= 1.65f) // 16:10 format
        {
            Debug.Log("aspect ratio is 16:10");
            ComputerCamera.m_Lens.FieldOfView = 42;
            DeskCamera.m_Lens.FieldOfView = 48;
        }
        else if (screenRatio <= 1.8f) // 16:9 format
        {
            Debug.Log("aspect ratio is 16:9");
            ComputerCamera.m_Lens.FieldOfView = 38;
            DeskCamera.m_Lens.FieldOfView = 46;
        }
        else
        {
            Debug.LogWarning("Screen format higher than 16:9, standart 16:9 format will be applied");
        }
    }

}
