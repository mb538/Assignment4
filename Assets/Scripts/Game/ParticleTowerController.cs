using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTowerController : MonoBehaviour
{
    [Header("Turret Attributes")]
    public GameObject upgrade;
    public int damage = 10;
    public float fireRate = 2f;
    public float range = 10f;
    public int cost = 30;
    public int upgradeCost = 50;

    [Header("Unity Settings")]
    public GameObject shootEffect;
    public GameObject projectile;
    public Transform firepoint;
    public ObjectPool pool;

    private GameObject target;
    private bool active = true;
    IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        pool = GameObject.Find("ParticleObjectPool").GetComponent<ObjectPool>();
        coroutine = CountDown();
        StartCoroutine(coroutine);
        InvokeRepeating("SearchForTargets", 0f, 1f);
    }
    private void Update()
    {
        
    }
    IEnumerator CountDown()
    {
        while (active)
        {
            if (target != null)
            {
                Shoot();
                yield return new WaitForSeconds(fireRate);
            }
            yield return null;
        }
    }
    private void SearchForTargets()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = enemy;
            }
        }
        if (closestEnemy != null && shortestDistance <= range)
        {
            target = closestEnemy;
        }
        else
        {
            target = null;
        }
    }
    private void Shoot()
    {
        GameObject _projectile = pool.GetItem(); // Get object from pool
        _projectile.transform.position = firepoint.position;
        ProjectileController pc = _projectile.GetComponent<ProjectileController>();
        if (pc != null)
        {
            pc.SetTarget(target);
            pc.SetDamage(damage);
            pc.SetObjectPool(pool);
        }
    }

}
