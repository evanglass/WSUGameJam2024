using KinematicCharacterController.Examples;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayers;
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private GameObject bulletHolePrefab;
    [SerializeField] private SceneDefinitionContainer menuScene;
    [SerializeField] private Image vignette;
    [SerializeField] private CanvasGroup deathMessage;
    [SerializeField] private int maxAmmo;

    [SerializeField] private int ammo;
    [SerializeField] private bool meleeOnly;

    private Animator animator;
    private void Awake() {
        animator = GetComponentInChildren<Animator>();
        ammo = maxAmmo;
        health = maxHealth;
        Time.timeScale = 1f;
    }
    private void Shoot() {
        if (meleeOnly)
            return;
        animator.SetTrigger("Fire");
        float maxRange = 100f;
        RaycastHit hit;
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward, Color.red, 10f);
        PlayerBulletTrail trail = Instantiate(playerBulletTrailPrefab, muzzleTransform.position,muzzleTransform.rotation);
        Instantiate(muzzleFlashPrefab, muzzleTransform.position, Quaternion.identity, transform);
        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxRange, targetLayers)) {

            ITakesShots shotThing = hit.collider.GetComponent<ITakesShots>();

            if(shotThing != null) {
                shotThing.TakeShot(gunDamage) ;
            }
            if (hit.collider.gameObject.TryGetComponent(out Rigidbody rb)) {
                rb.AddForce(cameraTransform.forward * gunDamage, ForceMode.Impulse);
            }
            trail.Initialize(hit.point);

            GameObject bulletHole =Instantiate(bulletHolePrefab, hit.point + (hit.normal / 100.0f), Quaternion.LookRotation(-hit.normal), hit.transform);
            bulletHole.transform.localScale = new Vector3(0.01f / bulletHole.transform.lossyScale.x, 0.01f / bulletHole.transform.lossyScale.y, 1 / bulletHole.transform.lossyScale.z);
            
            ammo--;
            muzzleTransform.GetComponent<AudioSource>().Play();
            
        } else {
            trail.Initialize(muzzleTransform.position + cameraTransform.forward * maxRange);
        }
    }
    private void Melee()
    {
        animator.SetTrigger("Melee");
        float maxRange = 2f;
        RaycastHit hit;
        if (Physics.SphereCast(cameraTransform.position, .2f, cameraTransform.forward, out hit, maxRange, targetLayers))
        {

            ITakesShots shotThing = hit.collider.GetComponent<ITakesShots>();

            if (shotThing != null)
            {
                shotThing.TakeShot(gunDamage);
            }
            if (hit.collider.gameObject.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(Vector3.up * meleeDamage, ForceMode.Impulse);
            }

        }
    }

    [SerializeField] private PlayerBulletTrail playerBulletTrailPrefab;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private GameObject gunObj;

    [SerializeField] private float gunDamage;
    [SerializeField] private float meleeDamage;




    [SerializeField] private Transform centerOfMass;
    public Transform GetCenterOfMass() {
        return centerOfMass;
    }

   [SerializeField] private float maxHealth = 1.1f;

    public float health;
    private bool dead = false;
    public GameObject healthBar;

    [SerializeField] private float healthRecoveryRate;

    private void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level 3")
        {
            SetMelee();
        }
    }

    public void SetMelee()
    {
        if (gunObj == null)
        {
            gunObj.SetActive(false);
        }
        meleeOnly = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuScene.Compile();
            SceneManager.Instance.ChangeScene(menuScene.SceneDefinition);
        }

        health = Mathf.MoveTowards(health, maxHealth, healthRecoveryRate * Time.deltaTime);
        vignette.color = new Color(vignette.color.r, vignette.color.g, vignette.color.b, ((float)maxHealth - (float)health) / (float)maxHealth);

        if (dead) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.Instance.ReloadScene();
            }

            healthRecoveryRate = 0;
            Time.timeScale = 0f;

            return;
        }

        if (Input.GetMouseButtonDown(1)) {
            Melee();
        }
        if (Input.GetMouseButtonDown(0) && ammo > 0) {
            Shoot();
        }

        if (ammo != maxAmmo && ammo >= 0 && Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<AudioSource>().Play();
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
        health -= 1f;
        if(health <= 0)
        {
            dead = true;
            deathMessage.alpha = 1;
        }
    }
}
