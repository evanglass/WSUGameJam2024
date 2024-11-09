using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const int maxHealth = 4;
    public int health = maxHealth;
    private bool dead = false;
    public GameObject healthBar;

    private void Update()
    {
        if(dead)
        {
            // DIE
            transform.position += Vector3.down / 100.0f;
            GetComponent<ExampleCharacterController>().MaxStableMoveSpeed = 0;
            GetComponent<ExampleCharacterController>().MaxAirMoveSpeed = 0;
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {

        }
    }

    public void TakeDamage()
    {
        health--;
        healthBar.GetComponent<HealthBar>().health = (float)health / maxHealth;
        if(health <= 0)
        {
            dead = true;
        }
    }
}
