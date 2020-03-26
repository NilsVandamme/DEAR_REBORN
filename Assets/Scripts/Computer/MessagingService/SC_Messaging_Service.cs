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
    public GameObject particlEffectPlayerMessage;
    public GameObject particlEffectBossMessage;

    [Header("ConfigTech")]
    public GameObject chatPanelObject;
    public GameObject listBossDialogObject;
    public GameObject listPlayerDialogObject;

    [Header("Buttons")]
    public GameObject buttonsChoices;
    public GameObject buttonEndChat;

    [Header("List distrubition Talking time")]
    [Tooltip("Check a case to let the player talk after a checked case")]
    public List<bool> listOrderDialog = new List<bool>();

    [Header("Translate Parameters")]
    //Je savais pas trop où mettre ça donc j'ai fait un autre header, déso !

    public float newPositionLerp = 0f;

    public bool CanStartTuto;
    #endregion

    #region privates attributes
    Animator animatorChat;

    AudioSource messageAudioSource;

    List<TextMeshProUGUI> chatMessageList = new List<TextMeshProUGUI>();
    TextMeshProUGUI[] listBossMessages;
    TextMeshProUGUI[] listPlayerMessages;

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
    public void OpenChat()
    {
        //SC_GM_SoundManager.instance.PlayPickupBossCallSound();

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
     * Player send message to chat
     */
    public void playerSendResponce(int numeroSmilley)
    {

        if (playerTurn)
        {
            playerTurn = false;

            // PlaceHolder
            chatMessageList.Add(
                Instantiate(listPlayerMessages[4],
                chatPanelObject.transform));

            // Creation of the new message
            var message = Instantiate(listPlayerMessages[numeroSmilley],
                chatPanelObject.transform);

            // Add the particleSystem
            var particles = Instantiate(particlEffectPlayerMessage, message.GetComponentInChildren<Image>().transform);
            particles.transform.SetParent(message.GetComponentInChildren<Image>().transform);
            
            StartCoroutine(PlayEffects(particles.GetComponent<ParticleSystem>(), 1));

            // Add the message
            chatMessageList.Add(message);

            

            // PlaceHolder
            chatMessageList.Add(
                Instantiate(listPlayerMessages[4],
                chatPanelObject.transform));

            chatRefreshed = false;
            lerpUpdate = true;
        }
    }

    /**
     * Start the animation of writting in the chat
     * Then send the boss message to chat
     */
    IEnumerator BossIsTalking()
    {
        animatorChat.SetBool("IsBossWritting", true);

        // PlaceHolder
        var placeholder = Instantiate(listPlayerMessages[4],
            chatPanelObject.transform);

        var placeholder2 = Instantiate(listPlayerMessages[4],
            chatPanelObject.transform);

        var placeholder3 = Instantiate(listPlayerMessages[4],
            chatPanelObject.transform);

        var placeholder4 = Instantiate(listPlayerMessages[4],
            chatPanelObject.transform);

        chatMessageList.Add(placeholder);
        chatMessageList.Add(placeholder2);
        chatMessageList.Add(placeholder3);
        chatMessageList.Add(placeholder4);

        yield return new WaitForSeconds(2f);

        chatMessageList.RemoveAt(chatMessageList.Count - 1);
        chatMessageList.RemoveAt(chatMessageList.Count - 1);
        chatMessageList.RemoveAt(chatMessageList.Count - 1);
        chatMessageList.RemoveAt(chatMessageList.Count - 1);

        Destroy(placeholder);
        Destroy(placeholder2);
        Destroy(placeholder3);
        Destroy(placeholder4);

        animatorChat.SetBool("IsBossWritting", false);
        bossWrittingAnimationStarted = false;

        // Creation of the new message
        var message = Instantiate(listBossMessages[countPassedDialog],
            chatPanelObject.transform);

        // Creation of the particles
        var particles = Instantiate(particlEffectBossMessage, message.GetComponentInChildren<Image>().transform);
        particles.transform.SetParent(message.GetComponentInChildren<Image>().transform);
        StartCoroutine(PlayEffects(particles.GetComponent<ParticleSystem>(), 0));
        
        chatMessageList.Add(message);

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
        yield return new WaitForSeconds(1.3f);
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
        // Get the all the text of the boss
        listBossMessages = listBossDialogObject.GetComponentsInChildren<TextMeshProUGUI>(true);

        // Get the all the smilley of the player
        listPlayerMessages = listPlayerDialogObject.GetComponentsInChildren<TextMeshProUGUI>(true);
    }

    public void StartTuto()
    {
        CanStartTuto = true;
    }
}