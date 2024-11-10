using KinematicCharacterController.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayers;
    private void Shoot() {
        float maxRange = 100f;
        float damage = 50f;
        RaycastHit hit;
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.red, 10f);
        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxRange, targetLayers)) {

            ITakesShots shotThing = hit.collider.GetComponent<ITakesShots>();

            if(shotThing != null) {
                Debug.Log("Shot " + hit.collider.gameObject);
                if (shotThing.TakeShot(damage)) {
                    if (hit.collider.gameObject.TryGetComponent(out Rigidbody rb)) {
                        rb.AddForce(cameraTransform.forward * damage, ForceMode.Impulse);
                    }
                }
            }


        }
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
        if (Input.GetMouseButtonDown(0)) {
            Shoot();
        }
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
