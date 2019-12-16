using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This script generate the content of the window for a prefab and it's icons/name

public class SC_WindowContent : MonoBehaviour
{
    public string Name;
    public Sprite Icon;
    public GameObject contentPrefab;

    [Space]

    public TMP_Text nameText;
    public Image IconImage;
    public GameObject contentEmplacement;

    private void Awake()
    {
        nameText.text = Name;
        IconImage.sprite = Icon;

        GameObject content = Instantiate(contentPrefab);
        content.transform.SetParent(contentEmplacement.transform, false);
    }
}
