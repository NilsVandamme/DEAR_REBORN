using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class SC_Messaging_Service : MonoBehaviour
{
    #region Public attributes
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
    #endregion

    #region privates attributes
    Animator animatorChat;

    List<TextMeshProUGUI> chatMessageList = new List<TextMeshProUGUI>();
    TextMeshProUGUI[] listBossMessages;
    TextMeshProUGUI[] listPlayerMessages;

    int countPassedDialog;
    int TotalTextInDialog;

    bool playerTurn = false;
    bool bossWrittingAnimationStarted = false;
    bool ChatStarted = false;
    bool chatRefreshed = true;
    #endregion

    private void Awake()
    {
        countPassedDialog = 0;

        animatorChat = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        GetAllDialog();
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
        if (listBossDialogObject != null && runInEditMode)
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
    }

    /*
     * Open the window boss call
     */
    public void OpenChat()
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
            chatMessageList.Add(
                Instantiate(listPlayerMessages[numeroSmilley],
                chatPanelObject.transform));

            // PlaceHolder
            chatMessageList.Add(
                Instantiate(listPlayerMessages[4],
                chatPanelObject.transform));

            chatRefreshed = false;

            Debug.Log("Chat refreshed");
        }
    }

    /**
     * Start the animation of writting in the chat
     * Then send the boss message to chat
     */
    IEnumerator BossIsTalking()
    {
        animatorChat.SetBool("IsBossWritting", true);

        yield return new WaitForSeconds(2);

        animatorChat.SetBool("IsBossWritting", false);
        bossWrittingAnimationStarted = false;

        // Creation of the new message
        chatMessageList.Add(
            Instantiate(listBossMessages[countPassedDialog],
            chatPanelObject.transform));

        chatRefreshed = false;
        Debug.Log("Chat refreshed");


        // Check the turn of the person who can talk
        // If it's the player turn
        // The boss wait it's responce before talking
        if (listOrderDialog[countPassedDialog]) playerTurn = true;
        else playerTurn = false;

        // Then pass to the next dialog
        countPassedDialog++;
    }

    // Wait for the window to have finished to open to set the variable
    IEnumerator ChatIsOpenning()
    {
        yield return new WaitForSeconds(1);
        ChatStarted = true;
    }

    IEnumerator RefreshChat()
    {
        yield return new WaitForSeconds(0.2f);

        chatPanelObject.GetComponent<RectTransform>().localPosition = new Vector3(
        chatPanelObject.GetComponent<RectTransform>().localPosition.x,
        chatPanelObject.GetComponent<RectTransform>().localPosition.y + 100,
        chatPanelObject.GetComponent<RectTransform>().localPosition.z);
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
}