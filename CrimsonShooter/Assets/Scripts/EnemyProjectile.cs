using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag.Equals("Player"))
        {
            collision.transform.GetComponent<Player>().TakeDamage();
        }
        Destroy(gameObject);
    }
}
