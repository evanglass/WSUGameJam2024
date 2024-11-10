using KinematicCharacterController.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private int maxAmmo;

    private int ammo;

    private Animator animator;
    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        ammo = maxAmmo;
    }
    private void Shoot() {
        animator.SetTrigger("Fire");
        float maxRange = 100f;
        RaycastHit hit;
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.red, 10f);
        PlayerBulletTrail trail = Instantiate(playerBulletTrailPrefab, muzzleTransform.position,muzzleTransform.rotation);
        Instantiate(muzzleFlashPrefab, muzzleTransform.position, Quaternion.identity, transform);
        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxRange, targetLayers)) {

            ITakesShots shotThing = hit.collider.GetComponent<ITakesShots>();

            if(shotThing != null) {
                Debug.Log("Shot " + hit.collider.gameObject);
                shotThing.TakeShot(gunDamage) ;
            }
            if (hit.collider.gameObject.TryGetComponent(out Rigidbody rb)) {
                rb.AddForce(cameraTransform.forward * gunDamage, ForceMode.Impulse);
            }
            trail.Initialize(hit.point);

            GameObject bulletHole =Instantiate(bulletHolePrefab, hit.point + (hit.normal / 100.0f), Quaternion.LookRotation(-hit.normal), hit.transform);
            bulletHole.transform.localScale = new Vector3(0.01f / bulletHole.transform.lossyScale.x, 0.01f / bulletHole.transform.lossyScale.y, 1 / bulletHole.transform.lossyScale.z);
            
            muzzleTransform.GetComponent<AudioSource>().Play();

            ammo--;
            
        } else {
            trail.Initialize(muzzleTransform.position + cameraTransform.forward * maxRange);
        }
    }

    [SerializeField] private PlayerBulletTrail playerBulletTrailPrefab;

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
        if (Input.GetMouseButtonDown(0) && ammo > 0) {
            Shoot();
        }

        if (ammo != maxAmmo && Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Reload");
            animator.SetBool("HasReloaded", false);
            ammo = -1;
        }

        if (ammo == -1 && animator.GetBool("HasReloaded"))
        {
            ammo = maxAmmo;
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
