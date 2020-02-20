using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_NamePlayer : MonoBehaviour
{
    public GameObject storyArbo;

    public TMP_InputField namePlayer;
    public Button next;
    public string sceneToLoad;
    public Animator donnaA1;

    void Start()
    {
        next.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (namePlayer.text != "")
        {
            SC_GM_Master.gm.namePlayer = namePlayer.text;

            SC_SaveGame save = new SC_SaveGame();
            save.Save(sceneToLoad, true);

            storyArbo.SetActive(true);
            donnaA1.Play("Unlocked");
        }
    }
}
