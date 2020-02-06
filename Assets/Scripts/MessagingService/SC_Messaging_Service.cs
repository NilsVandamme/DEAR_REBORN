using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SC_Messaging_Service : MonoBehaviour
{

    public GameObject chatPanel;

    [Tooltip("Mettre dans l'ordre")]
    public GameObject[] listDiscussionOrder;

    Animator animatorChat;
    int count = 0;

    List<Message> messageList = new List<Message>();


    private void Start()
    {
        animatorChat = gameObject.GetComponent<Animator>();
    }

    /* 
     * Write the passed message in the chat
     * Different appearence if it's the boss or the player
     * */
    public void SendMessageToChat() // , Message.MessageType messageType
    {
        // Message creation
        Message newMessage = new Message();

        GameObject newText = Instantiate(listDiscussionOrder[count], chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();

        // Send message
        messageList.Add(newMessage);

        count++;
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
}


[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}