using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Unity Settings")]
    public GameObject defeatedEffect;
    private Transform destination;
    public GameObject enemy;
    public NavMeshAgent agent;
    private GameController gc;

    [Header("Enemy Settings")]
    public int maxHealth = 100;
    public int damage = 20;
    private int curHealth;
    public int gold = 40;

    void Start()
    {
        curHealth = maxHealth;
        agent = gameObject.GetComponent<NavMeshAgent>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        if(destination != null)
        {
            InvokeRepeating("CalculatePath", 0, 2f); 
        }
    }
    private void CalculatePath()
    {
        agent.SetDestination(destination.position);
        if(agent.remainingDistance <= 1f && !agent.pathPending)
        {
            gc.SetCurHealth(gc.GetCurHealth() - damage);
            print("Damage Taken " + gc.GetCurHealth());
            Destroy(this.gameObject);
        }
        if(curHealth <= 0)
        {
            Defeated();
        }
        if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            print("Enemy has no valid path");
           
        }
        if (agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            print("Unable to reach destination partial");
        }
    }
   
    private void Defeated()
    {
        gc.SetCurGold(gc.GetCurGold() + gold);
        if(defeatedEffect != null)
        {
            GameObject effect = (GameObject)Instantiate(defeatedEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(effect, 3f);
        }
        Destroy(gameObject);
    }
    public void SetDest(Transform destination)
    {
        this.destination = destination;
    }
    public int GetCurHealth()
    {
        return curHealth;
    }
    public void SetCurHealth(int health)
    {
        this.curHealth = health;
    }
}
