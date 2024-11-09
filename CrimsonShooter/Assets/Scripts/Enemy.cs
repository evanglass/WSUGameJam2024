using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float stopDistance = 4;
    public float shootDelay = 2.0f;
    public float shootSpeed = 3.0f;

    public GameObject enemyProjectile;
    public Transform enemyProjectileSpawnPoint;

    private float shootTimer = 0;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    //void Update()
    //{
    //    agent.SetDestination(target.position);
    //    Vector3 direction = target.position - transform.position;
    //    if(Physics.Raycast(transform.position, direction, out RaycastHit hit, stopDistance + 1, LayerMask.GetMask("Player","Obstacles")))
    //    {
    //        if(hit.transform.tag.Equals("Player"))
    //        {
    //            agent.stoppingDistance = stopDistance;
    //            shootTimer += Time.deltaTime;
    //            if(shootTimer >= shootDelay)
    //            {
    //                shootTimer = 0;
    //                ShootProjectile();
    //            }
    //        }
    //        else
    //        {
    //            agent.stoppingDistance -= 0.1f;
    //            shootTimer = 0;
    //        }
    //    }
    //    else
    //    {
    //        agent.stoppingDistance = stopDistance;
    //        shootTimer = 0;
    //    }
    //}

    void ShootProjectile()
    {
        GameObject projectile = Instantiate(enemyProjectile, enemyProjectileSpawnPoint.position, enemyProjectileSpawnPoint.rotation);
        projectile.GetComponent<Rigidbody>().velocity = (target.position - enemyProjectileSpawnPoint.position).normalized * shootSpeed;
    }
}
