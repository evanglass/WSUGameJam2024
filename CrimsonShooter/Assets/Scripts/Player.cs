using KinematicCharacterController.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Shoot() {

    }


    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform muzzleTransform;

    [SerializeField] private float gunDamage;




    [SerializeField] private Transform centerOfMass;
    public Transform GetCenterOfMass() {
        return centerOfMass;
    }

    public const int maxHealth = 4;
    public int health = maxHealth;
    private bool dead = false;
    public GameObject healthBar;

    private void Update()
    {
        //if(dead)
        //{
        //    // DIE
        //    GetComponent<ExampleCharacterController>().MaxStableMoveSpeed = 0;
        //    GetComponent<ExampleCharacterController>().MaxAirMoveSpeed = 0;
        //    GetComponent<CapsuleCollider>().enabled = false;
        //}
        //else
        //{
        //    if(Input.GetMouseButtonDown(0))
        //    {
        //        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 1000, LayerMask.GetMask("Enemy"))) {
        //            Destroy(hit.transform.gameObject);
        //        }
        //    }
        //}
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
