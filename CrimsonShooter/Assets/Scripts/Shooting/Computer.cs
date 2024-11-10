using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Computer : MonoBehaviour, ITakesShots
{
    [SerializeField] private Texture webglTexture;
    static int computersOnLevel = 0;
    private void Awake()
    {
        computersOnLevel++;
    }

    public bool TakeShot(float damage)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject go in gos)
        {
            if (go.GetComponent<Wireframe>())
                go.GetComponent<Wireframe>().UseWireframe = true;
            
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
