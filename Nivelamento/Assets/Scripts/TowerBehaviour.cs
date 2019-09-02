using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    /*bool alive;

    public GameObject projectilePrefab;
    GameObject projectile;

    void Start()
    {
        alive = true;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 3f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if(hitObject.GetComponent<PlayerController>())
            {
                if(projectile == null)
                {
                    projectile = Instantiate(projectilePrefab) as GameObject;
                    projectile.GetComponent<Projectile>().ShootAt(hitObject);
                    projectile.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    projectile.transform.rotation = transform.rotation;
                }
            }
        }
    }*/

    private Transform target;
    public float range = 5;

    public string enemyTag = "Enemy";

    public float fireRate = 1;
    private float fireCountdown = 0;

    public GameObject projectilePrefab;
    public Transform firePoint;
    private GameObject projectileObj;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if(target == null)
        {
            return;
        }

        if(fireCountdown <= 0)
        {
            if(projectileObj == null)
            {
                Shoot();
                fireCountdown = 1 / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        projectileObj = (GameObject)Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectile = projectileObj.GetComponent<Projectile>();

        if(projectile != null)
        {
            projectile.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
