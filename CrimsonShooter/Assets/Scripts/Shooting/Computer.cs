using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Computer : MonoBehaviour, ITakesShots

{
    [SerializeField] private Transform brokenComputer;
    static int computersOnLevel = 0;
    [SerializeField] private UnityEvent thirdExtraEffect;
    private void Awake()
    {
        computersOnLevel++;
    }

    // all objects except Level 1 environment
    // go.GetComponent<Wireframe>().UseWireframe = true; // enable wireframe for object
    // go.GetComponent<Wireframe>().UseWireframe = false; // disable wireframe for object
    //
    // level 1 object
    // go.GetComponent<Wireframe>().SetSpecificMats(new int[] { 0, 3 }, true); // enable wireframe for materials 0 and 3 for object
    // go.GetComponent<Wireframe>().SetSpecificMats(new int[] { 0, 3 }, false); // disable wireframe for materials 0 and 3 for object
    bool isDead = false;


    public bool TakeShot(float damage)
    {
        if (isDead) {
            return true;
        }
        isDead = true;
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level 1")
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject go in gos)
            {
                if (go.GetComponent<Wireframe>())
                {
                    go.GetComponent<Wireframe>().UseWireframe = true;
                    if (go.name == "Level 1")
                    {
                        if (computersOnLevel == 3)
                        {
                            go.GetComponent<Wireframe>().SetSpecificMats(new int[] { 0, 3 }, true);
                        }
                        else if (computersOnLevel == 2)
                        {
                            go.GetComponent<Wireframe>().SetSpecificMats(new int[] { 1, 5 }, true);
                        }
                        else if (computersOnLevel == 1)
                        {
                            go.GetComponent<Wireframe>().SetSpecificMats(new int[] { 2, 4 }, true);
                        }
                    }
                }
            }
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level 3")
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("EffectSet" + (4-computersOnLevel));
            foreach (GameObject go in gos)
            {
                if (go.GetComponent<Wireframe>())
                {
                    go.GetComponent<Wireframe>().UseWireframe = true;
                }
            }
        }
        else
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject go in gos)
            {
                if (go.GetComponent<Wireframe>())
                {
                    go.GetComponent<Wireframe>().UseWireframe = true;
                }
            }
        }
        // COMPUTER DESTROY EFFECT?

        MessageManager.Instance.OrderedMessage();

        Destroy(gameObject);
        Instantiate(brokenComputer, transform.position, transform.rotation);
        computersOnLevel--;
        if(computersOnLevel == 0)
        {
            thirdExtraEffect.Invoke();
            if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Level 3")
                GameObject.FindGameObjectWithTag("Elevator").GetComponent<Animator>().SetBool("Open", true);
        }
        return true;
    }
}
