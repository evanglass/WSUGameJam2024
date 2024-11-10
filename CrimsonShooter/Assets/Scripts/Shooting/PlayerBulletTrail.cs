using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBulletTrail : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float duration = 1f;
    public void Initialize(Vector3 position) {
        target = position - transform.position;
        target.Normalize();

    }
    private Vector3 target;
    private float timer;
    private void Update() {
        transform.position += target * speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > duration) {
            Destroy(gameObject);
        }
    }
    
}
