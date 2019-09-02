using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //public GameObject collisionEffect;
    private ObjectPool objectPool;
    private GameObject target;
    private int damage = 0; //Damage passed from tower script
    public float velocity = 20f;
    private bool targetHit = false;

    // Update is called once per frame
    void Update()
    {
        if (target != null && targetHit == false)
        {
            Seek(target);
        }
        if(target == null)
        {
            objectPool.DisableItem(gameObject);
        }
    }
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetObjectPool(ObjectPool pool)
    {
        this.objectPool = pool;
    }

    private void Seek(GameObject target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);
        if (transform.position == target.transform.position)
        {
            TargetHit(target.transform);
        }
    }

    private void TargetHit(Transform t)
    {
        EnemyController ec = target.GetComponent<EnemyController>();
        if(ec != null && objectPool != null)
        {
            ec.SetCurHealth(ec.GetCurHealth() - damage);
            objectPool.DisableItem(gameObject);
        }
        targetHit = true;
    }
}
