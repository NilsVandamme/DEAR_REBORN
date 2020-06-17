using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SC_Messaging_Service : MonoBehaviour
{
    #region Public attributes
    [Header("Effects")]
    public GameObject particlEffectBossMessage;

    [Header("ConfigTech")]
    public GameObject chatPanelObject;
    public GameObject listBossDialogObject;

    [Header("List distrubition Talking time")]
    [Tooltip("Check a case to let the player talk after a checked case")]
    public List<bool> listOrderDialog = new List<bool>();

    [Header("Translate Parameters")]
    public Scrollbar scrollbar;
    public float newPositionLerp = 0f;
    public bool CanStartTuto;

    #endregion

    #region privates attributes
    Animator animatorChat;

    AudioSource messageAudioSource;

    List<GameObject> listBossMessages = new List<GameObject>();

    int countPassedDialog;
    int TotalTextInDialog;

    float LerpVelocity = 0.0f;
    float LerpTime = 0.1f;

    bool playerTurn = false;
    bool bossWrittingAnimationStarted = false;
    bool ChatStarted = false;
    bool isChatFinished = false;
    bool isCallMusicStarted = false;
    bool isCallMusicStopped= false;
    bool chatRefreshed = true;
    public bool lerpUpdate = false;
    #endregion

    private void Awake()
    {
        gameObject.SetActive(true);

        countPassedDialog = 0;

        animatorChat = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        // Get the number of dialog
        TotalTextInDialog = listBossDialogObject.GetComponentsInChildren<TextMeshProUGUI>(true).Length;

        // Delete the list only if modification
        if (listOrderDialog.Count != TotalTextInDialog)
        {
            // Empty the list
            listOrderDialog.Clear();

            // Set the correct number of bool to the list
            for (int i = 0; i < TotalTextInDialog; i++) listOrderDialog.Add(false);
        }

        GetAllDialog();

        messageAudioSource = GetComponent<AudioSource>();

        isChatFinished = animatorChat.GetBool("IsChatFinished");
    }

    /**
     * Function called when there is a modification in the scene
     */
    private void Update()
    {
        // Make the chat go down when someone wrote
        if (!chatRefreshed)
        {
            chatRefreshed = true;

            StartCoroutine(RefreshChat());
        }

        // When in editor
        if (listBossDialogObject != null)
        {
            // Get the number of dialog
            TotalTextInDialog = listBossDialogObject.GetComponentsInChildren<TextMeshProUGUI>(true).Length;

            // Delete the list only if modification
            if (listOrderDialog.Count != TotalTextInDialog)
            {
                // Empty the list
                listOrderDialog.Clear();

                // Set the correct number of bool to the list
                for (int i = 0; i < TotalTextInDialog; i++) listOrderDialog.Add(false);
            }
        }

        // Boucle dialog
        if (ChatStarted && countPassedDialog < TotalTextInDialog)
        { 

            if (!playerTurn)
            {

                if (!bossWrittingAnimationStarted)
                {
                    bossWrittingAnimationStarted = true;

                    StartCoroutine(BossIsTalking());
                }
            }
        }


        if (countPassedDialog == TotalTextInDialog && !playerTurn)
        {
            animatorChat.SetBool("IsChatFinished", true);
        }


        if (lerpUpdate)
        {
            chatPanelObject.GetComponent<RectTransform>().localPosition = new Vector3(
                chatPanelObject.GetComponent<RectTransform>().localPosition.x,
                Mathf.SmoothDamp(chatPanelObject.GetComponent<RectTransform>().localPosition.y, newPositionLerp, ref LerpVelocity, LerpTime, 500f, Time.deltaTime),
                chatPanelObject.GetComponent<RectTransform>().localPosition.z);

            if(Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                lerpUpdate = false;
            }
        }

        // Play the ringing sound of the call
        if (!ChatStarted && !isChatFinished && !isCallMusicStarted)
        {
            //SC_GM_SoundManager.instance.PlayBossCallIncomingSound();
            isCallMusicStarted = true;

            //SC_GM_SoundManager.instance.ChangeSoundVolume(0.3f);
        }
        if (ChatStarted && !isCallMusicStopped)
        {
            //SC_GM_SoundManager.instance.StopBossCallIncomingSound();
            isCallMusicStopped = true;
        }
    }

    /*
     * Open the window boss call
     */
    public void PickUpCall()
    {
        animatorChat.SetBool("IsChatOpen", true);
        StartCoroutine(ChatIsOpenning());
    }

    /*
     * Close the window boss call
     */
    public void CloseChat()
    {
        animatorChat.SetBool("IsChatOpen", false);

        StartCoroutine(CloseChatDefinitively());
    }

    /**
     * Player responde with an emojy while clicking on it
     */
    public void playerSendResponce()
    {
        Debug.Log("Début du choix du joueur");

        if (playerTurn)
        {
            playerTurn = false;

            // Creation of the new message
            // Il faut faire que les deux autres icones fades
        }
    }

    /**
     * Start the animation of writting in the chat
     * Then send the boss message to chat
     */
    IEnumerator BossIsTalking()
    {

        // Creation of the new message
        GameObject message = Instantiate(listBossMessages[countPassedDialog],
            chatPanelObject.transform);
        message.transform.localScale = new Vector3(1, 1, 1);
        message.transform.position.Set(message.transform.position.x, 
            message.transform.position.y,
            0);

        Canvas.ForceUpdateCanvases();
        scrollbar.value = 1;
        scrollbar.value = 0;
        Canvas.ForceUpdateCanvases();

        /*
        // Creation of the particles
        var particles = Instantiate(particlEffectBossMessage, message.GetComponentInChildren<Image>().transform);
        particles.transform.SetParent(message.GetComponentInChildren<Image>().transform);
        StartCoroutine(PlayEffects(particles.GetComponent<ParticleSystem>(), 0));
        */

        yield return new WaitForSeconds(2f);
        bossWrittingAnimationStarted = false;

        chatRefreshed = false;

        // Check the turn of the person who can talk
        // If it's the player turn
        // The boss wait it's responce before talking
        if (listOrderDialog[countPassedDialog]) playerTurn = true;
        else playerTurn = false;

        // Then pass to the next dialog
        countPassedDialog++;
    }

    IEnumerator PlayEffects(ParticleSystem particles, int type)
    {
        yield return new WaitForSeconds(0.2f);
        particles.Play();

        if (type == 0)
        {

            SC_GM_SoundManager.instance.PlayMessageBossSound();

        } else
        {

            SC_GM_SoundManager.instance.PlayMessageEmployeeSound();
        }
    }

    // Wait for the window to have finished to open to set the variable
    IEnumerator ChatIsOpenning()
    {
        yield return new WaitForSeconds(1.2f);
        ChatStarted = true;
    }

    // Make the chat go down when the text appear
    IEnumerator RefreshChat()
    {
        newPositionLerp = chatPanelObject.GetComponent<RectTransform>().localPosition.y + 1000f;
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator CloseChatDefinitively()
    {
        yield return new  WaitForSeconds(1.3f);
        CanStartTuto = true;
        gameObject.SetActive(false);
    }

    /**
     * Get All dialog from the scene 
     */
    void GetAllDialog()
    {
        for (int i = 0; i < TotalTextInDialog; i++)
        {
            listBossMessages.Add(listBossDialogObject.transform.GetChild(i).gameObject);
        }
    }

    public void StartTuto()
    {
        CanStartTuto = true;
    }
}