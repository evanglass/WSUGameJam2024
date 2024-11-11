using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
    }

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level 1")
        {
            messageNum = 0;
        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level 2")
        {
            messageNum = 3;
        }

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level 3")
        {
            messageNum = 6;
        }
    }

    public void DisplayMessage(string msg)
    {
        GameObject.FindGameObjectWithTag("MessageText").GetComponent<Animator>().SetTrigger("Appear");
        GameObject.FindGameObjectWithTag("MessageText").GetComponentInChildren<TextMeshProUGUI>().text = msg;
    }

    private int messageNum = 0;
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

    public void OrderedMessage()
    {
        GameObject.FindGameObjectWithTag("MessageText").GetComponent<Animator>().SetTrigger("Appear");
        GameObject.FindGameObjectWithTag("MessageText").GetComponentInChildren<TextMeshProUGUI>().text = orderedMessages[messageNum];
        messageNum++;
    }
}
