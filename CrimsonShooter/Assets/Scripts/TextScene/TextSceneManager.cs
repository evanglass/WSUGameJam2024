using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSceneManager : MonoBehaviour
{
    private string[] dialogues =
    {
        "Dialogue 1 (Press Enter to continue)",
        "Dialogue 2 (Press Enter to continue)",
        "Attack the project manager? (Press Enter to continue)",
        "",
        "(Press Enter to continue)",
        "Dialogue 3 (Press Enter to continue)",
        "Dialogue 4 (Press Enter to continue)",
        "*dies*",
        ""
    };
    public int dialogueIndex = 0;
    private bool ceoDead = false;

    private void Start()
    {
        GetComponentsInChildren<TextMeshProUGUI>()[0].text = dialogues[0];
    }

    void Update()
    {
        if(!ceoDead && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("finish attacking"))
        {
            ceoDead = true;
            dialogueIndex++;
            GetComponent<Animator>().SetBool("Attack PM", false);
            GetComponentsInChildren<TextMeshProUGUI>()[0].text = dialogues[dialogueIndex];
        }
        if(Input.GetButtonDown("Submit") && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            dialogueIndex++;
            GetComponentsInChildren<TextMeshProUGUI>()[0].text = dialogues[dialogueIndex];
        }
        if(dialogueIndex == 3)
        {
            // attack pm
            GetComponent<Animator>().SetBool("Attack PM", true);
        }
        if(dialogueIndex == 5)
        {
            GetComponent<Animator>().SetBool("PM Explode", true);
        }

        if(dialogueIndex == dialogues.Length - 2)
        {
            // die
            GetComponent<Animator>().SetBool("Player Implode", true);
        }
        if(dialogueIndex == dialogues.Length - 1)
        {
            Debug.Log("game over");
            // main menu
        }
    }

    bool AnimatorIsPlaying()
    {
        return GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length >
               GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
