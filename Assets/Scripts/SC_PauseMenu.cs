using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC_PauseMenu : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(transform.GetChild(0).gameObject.activeInHierarchy == true)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }

        }
    }

    public void ExitToMainMenu()
    {
        SC_LoadingScreen.Instance.LoadThisScene("L_00Menu");
    }

    public void Restart()
    {
        SC_LoadingScreen.Instance.LoadThisScene(SceneManager.GetActiveScene().name);
    }


}
