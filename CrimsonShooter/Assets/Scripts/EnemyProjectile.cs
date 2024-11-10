using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {
    [SerializeField] private float velocity;
    private Rigidbody rb;

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
        }
        Destroy(gameObject);
    }
}
