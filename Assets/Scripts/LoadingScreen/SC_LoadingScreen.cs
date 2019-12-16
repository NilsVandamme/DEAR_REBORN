﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This script manage the loading of scenes and display of loading screen

public class SC_LoadingScreen : MonoBehaviour
{
    public static SC_LoadingScreen Instance; // Instance of this script

    private string LoadedScene; // Name of the scene which will be loaded
    private Animator anim; // Animator of the load screen
    private Image loadImage; // Image of the load screen

    private void Awake()
    {
        // Singleton logic:
        if (Instance == null)
        {
            Instance = this;
            // Don't destroy the loading screen while switching scenes:
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        loadImage = GetComponentInChildren<Image>();
        loadImage.enabled = false;

        //anim.SetTrigger("Hide");
    }


    // Debug only
    /*
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadThisScene();
        }
    }
    */

    // Load the scene indicated by LoadedScene and trigger the loadscreen animation
    public void LoadThisScene( string sceneToLoad)
    {
        loadImage.enabled = true;
        LoadedScene = sceneToLoad;
        anim.SetTrigger("Show");
        StartCoroutine("MinimumLoadTime");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    // Display the load screen
    public void Show()
    {
        loadImage.enabled = true;
        anim.SetTrigger("Show");
    }

    // Hide the load screen
    public void Hide()
    {
        loadImage.enabled = false;
    }

    // Make sure there's a minimum loading time
    private IEnumerator MinimumLoadTime()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(LoadedScene);
        anim.SetTrigger("Hide");
    }
}
