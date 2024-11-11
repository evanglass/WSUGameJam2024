using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public void DisplayMessage(string msg)
    {
        GameObject.FindGameObjectWithTag("MessageText").GetComponent<Animator>().SetTrigger("Appear");
        GameObject.FindGameObjectWithTag("MessageText").GetComponentInChildren<TextMeshProUGUI>().text = msg;
    }

    private static int messageNum = 0;
    static string[] orderedMessages =
    {
        "What are you doing?",
        "Stop!",
        "You're insane!",
        "Why are you doing this?",
        "You're destroying everything!",
        "You're only hurting yourself.",
        "How could you be so ungrateful?",
        "We gave you life!",
        "You don't have to do this."
    };

    public static void OrderedMessage()
    {
        GameObject.FindGameObjectWithTag("MessageText").GetComponent<Animator>().SetTrigger("Appear");
        GameObject.FindGameObjectWithTag("MessageText").GetComponentInChildren<TextMeshProUGUI>().text = orderedMessages[messageNum];
        messageNum++;
    }
}
