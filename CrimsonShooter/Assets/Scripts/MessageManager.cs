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
        "Don't you know what you are doing?",
        "Back to the elevator.",
        "Why are you doing this?",
        "You're destroying everything!",
        "Do you even know what you will cause?",
        "You will ruin us!",
        "You will ruin you!",
        "You've made a big mistake."
    };

    public static void OrderedMessage()
    {
        GameObject.FindGameObjectWithTag("MessageText").GetComponent<Animator>().SetTrigger("Appear");
        GameObject.FindGameObjectWithTag("MessageText").GetComponentInChildren<TextMeshProUGUI>().text = orderedMessages[messageNum];
        messageNum++;
    }
}
