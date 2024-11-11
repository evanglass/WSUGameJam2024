using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSceneManager : MonoBehaviour
{
    [SerializeField] private int attackIndex;
    [SerializeField] private int explodeIndex;
    [SerializeField] private int playerDieIndex;
    [SerializeField] private float fadeInRate;
    [SerializeField] private CanvasGroup canvasGroup;

    public string[] dialogues;
    public int dialogueIndex = 0;
    private bool ceoDead = false;
    private bool outroFade = false;

    private void Start()
    {
        GetComponentsInChildren<TextMeshProUGUI>()[0].text = dialogues[0];
    }

    private IEnumerator LoadSceneLate()
    {
        yield return new WaitForSeconds(6);
        SceneManager.Instance.NextScene();
    }
    bool fadeOut = false;
    void Update()
    {
        if (fadeOut) {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, 1, Time.deltaTime * fadeInRate);
            return;
        }


        if (!ceoDead && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("finish attacking")) {
            ceoDead = true;
            dialogueIndex++;
            GetComponent<Animator>().SetBool("Attack PM", false);
            GetComponentsInChildren<TextMeshProUGUI>()[0].text = dialogues[dialogueIndex];
        }
        if (Input.anyKeyDown && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            dialogueIndex++;
            GetComponentsInChildren<TextMeshProUGUI>()[0].text = dialogues[dialogueIndex];
        }
        if(dialogueIndex == attackIndex)
        {
            // attack pm
            GetComponent<Animator>().SetBool("Attack PM", true);
        }
        if(dialogueIndex == explodeIndex)
        {
            GetComponent<Animator>().SetBool("PM Explode", true);
        }

        if(dialogueIndex == playerDieIndex)
        {
            // die
            GetComponent<Animator>().SetBool("Player Implode", true);
        }
        if (dialogueIndex == dialogues.Length - 1) {

            StartCoroutine("LoadSceneLate");
            fadeOut = true;
        }
    }

    bool AnimatorIsPlaying()
    {
        return GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length >
               GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}
