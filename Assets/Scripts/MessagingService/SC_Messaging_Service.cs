using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SC_Messaging_Service : MonoBehaviour
{
    #region Public attributes
    [Header("ConfigTech")]
    public GameObject chatPanel;
    public GameObject listDialog;

    [Header("Icons chat")]
    public Sprite bossIcon;
    public Sprite playerIcon;

    public List<bool> listOrderDialog = new List<bool>();
    #endregion

    #region privates attributes
    Animator animatorChat;

    List<TextMeshProUGUI> chatMessageList = new List<TextMeshProUGUI>();
    TextMeshProUGUI[] listMessages;

    int countPassedDialog = 0;
    int TotalTextInDialog;

    bool bossTurn;
    bool bossWrittingAnimationStarted = false;
    #endregion

    private void Start()
    {
        animatorChat = gameObject.GetComponent<Animator>();

        // Get the number of dialog
        TotalTextInDialog = listDialog.GetComponentsInChildren<TextMeshProUGUI>().Length;

        // Set the correct number of bool to the list
        for (int i = 0; i < TotalTextInDialog; i++)
        {
            listMessages = listDialog.GetComponentsInChildren<TextMeshProUGUI>();
        }
    }

    /**
     * Function called when there is a modification in the scene
     */
    private void Update()
    {
        // When in editor
        if (listDialog != null && runInEditMode)
        {
            // Get the number of dialog
            TotalTextInDialog = listDialog.GetComponentsInChildren<TextMeshProUGUI>().Length;

            // Delete the list only if modification
            if (listOrderDialog.Count != TotalTextInDialog)
            {
                // Empty the list
                listOrderDialog.Clear();

                // Set the correct number of bool to the list
                for (int i = 0; i < TotalTextInDialog; i++) listOrderDialog.Add(false);
            }
        }

        // When not in editor
        // Check the turn of the person who can talk
        if (listOrderDialog[countPassedDialog]) bossTurn = true;
        else bossTurn = false;

        if (bossTurn && !bossWrittingAnimationStarted)
        {
            StartCoroutine(BossIsTalking());

            Debug.Log("Boss is talking");
        }
    }

    /* 
     * Write the next message in the chat
     * */
    void SendMessageToChat()
    {
        // Creation of the new message
        chatMessageList.Add(
            Instantiate(listMessages[countPassedDialog],
            chatPanel.transform));

        countPassedDialog++;
    }

    /*
     * Open the window boss call
     */
    public void OpenChat()
    {
        animatorChat.SetBool("IsChatOpen", true);
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
    public void playerSendResponce()
    {
        if (!bossTurn)
        {
            SendMessageToChat();
        }
    }

    /**
     * Start the animation of writting in the chat
     * Then send the boss message to chat
     */
    IEnumerator BossIsTalking()
    {
        bossWrittingAnimationStarted = true;
        animatorChat.SetBool("IsBossWritting", true);

        yield return new WaitForSeconds(2);

        animatorChat.SetBool("IsBossWritting", false);
        bossWrittingAnimationStarted = false;

        SendMessageToChat();
    }

    /**
     * Make the text look like it's the boss writting
     */
    void FormatPlayerText()
    {

    }

    /**
     * Make the text look like it's the player writting
     */
    void FormatBossText(TextMeshProUGUI text, Image image)
    {

    }

    void formatAllDialog()
    {
        for (int i = 0; i < TotalTextInDialog; i++)
        {
            if (listOrderDialog[i])
            {
                FormatBossText(
                    listMessages[i].GetComponent<TextMeshProUGUI>(),
                    listMessages[i].GetComponentInChildren<Image>());
            }
            
        }
    }
}