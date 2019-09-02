using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Unity Settings")]
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
    }
    private void Defeated()
    {
        gc.SetCurGold(gc.GetCurGold() + gold);
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
