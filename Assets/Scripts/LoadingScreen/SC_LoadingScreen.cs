using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This script manage the loading of scenes and display of loading screen

public class SC_LoadingScreen : MonoBehaviour
{
    public static SC_LoadingScreen Instance; // Instance of this script

    private string LoadedScene = null; // Name of the scene which will be loaded
    public Animator anim; // Animator of the load screen
    public Image loadImage; // Image of the load screen

    private void Awake()
    {
        // Singleton logic:
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        loadImage = GetComponentInChildren<Image>();
        loadImage.enabled = false;
    }

    // Load the scene indicated by LoadedScene and trigger the loadscreen animation
    public void LoadThisScene(string sceneToLoad)
    {
        LoadedScene = sceneToLoad;
        loadImage.enabled = true;
        anim.SetTrigger("Show");
        StartCoroutine("MinimumLoadTime");
    }

    // Display the load screen
    public void Show()
    {
        loadImage.enabled = true;
        anim.SetTrigger("Show");
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Hide the load screen
    public void Hide()
    {
        loadImage.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    // Make sure there's a minimum loading time
    private IEnumerator MinimumLoadTime()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(LoadedScene);
        anim.SetTrigger("Hide");
    }
}
