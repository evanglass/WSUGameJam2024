using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Computer : MonoBehaviour, ITakesShots
{
    static int computersOnLevel = 0;
    private void Awake()
    {
        computersOnLevel++;
    }

    public bool TakeShot(float damage)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level 1")
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject go in gos)
            {
                if (go.GetComponent<Wireframe>())
                {
                    go.GetComponent<Wireframe>().UseWireframe = true;
                    if (computersOnLevel == 3)
                    {
                        Debug.Log("A");
                        go.GetComponent<Wireframe>().SetSpecificMats(new int[] { 0, 3 });
                    }
                    else if (computersOnLevel == 2)
                    {
                        Debug.Log("B");
                        go.GetComponent<Wireframe>().SetSpecificMats(new int[] { 1, 5 });
                    }
                    else if (computersOnLevel == 1)
                    {
                        Debug.Log("C");
                        go.GetComponent<Wireframe>().SetSpecificMats(new int[] { 2, 4 });
                    }
                }
            }
        }
        else
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level 1")
            {
                foreach (GameObject go in gos)
                {
                    if (go.GetComponent<Wireframe>())
                    {
                        go.GetComponent<Wireframe>().UseWireframe = true;
                    }
                }
            }
        }
        // COMPUTER DESTROY EFFECT?
        Destroy(gameObject);
        computersOnLevel--;
        if(computersOnLevel == 0)
        {
            GameObject.FindGameObjectWithTag("Elevator").GetComponent<Animator>().SetBool("Open", true);
        }
        return true;
    }
}
