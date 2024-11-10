using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {
    [SerializeField] private float velocity;
    private Rigidbody rb;
    public GameObject bulletHolePrefab;
    private Vector3 prevPosition;

    private void OnEnable() {
        rb = GetComponent<Rigidbody>();
    }
    public void Fire(Vector3 dir) {
        rb.velocity = dir.normalized * velocity;
        timer = 0f;
    }

    private float lifespan = 10f;
    private float timer;
    private void Update() {
        timer += Time.deltaTime;
        if (timer > lifespan) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag.Equals("Player")) {
            collision.transform.GetComponent<Player>().TakeDamage();
            Destroy(gameObject);
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            Physics.Raycast(collision.GetContact(0).point, transform.position - collision.GetContact(0).point, out RaycastHit hit, 1000, LayerMask.GetMask("Obstacles"));
            Instantiate(bulletHolePrefab, hit.point + (hit.normal / 100.0f), Quaternion.LookRotation(-hit.normal));
            Destroy(gameObject);
        }
    }
}
