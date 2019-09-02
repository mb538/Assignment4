using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("Enemy Path")]
    public Transform start;
    public Transform destination;

    [Header("Enemies")]
    public GameObject enemy;
    public GameObject boss;

    [Header("Wave Settings")]
    public float waveDuration = 20f;
    public float spawnRate = 1f;
    private int waveNumber = 1;
    //private float timeLeft = 0f;

    [Header("GameUI Settings")]
    public WaveCounter waveCounter;

    IEnumerator coroutine;

    void Start()
    {
        start = GameObject.Find("Start").transform;
        destination = GameObject.Find("Destination").transform;
        waveCounter = GameObject.Find("GameUI/WaveCounter").GetComponent<WaveCounter>();
        waveNumber = 1;
        if(start != null && destination != null && waveCounter != null)
        {
            InvokeRepeating("StartWaveCoroutine", 0, waveDuration); // Begins to send waves
        }
    }

    public void StartWaveCoroutine()
    {
        coroutine = SendWave(waveNumber, spawnRate);
        StartCoroutine(coroutine);
    }
    IEnumerator SendWave(int wave, float spawnRate)
    {
        waveCounter.ShowWaveCounter(wave);
        for (int i = 0; i < wave; i++)
        {   
            if(wave % 3 == 0 && i == 2)
            {
                SpawnEnemy(boss);
            }
            else
            {
                SpawnEnemy(enemy);
            }
            yield return new WaitForSeconds(spawnRate); // Time between the enemy spawns during the wave
        }
        waveCounter.HideWaveCounter();
        waveNumber++;
    }
 
    public void SpawnEnemy(GameObject enemy)
    {
        GameObject enemyGO = (GameObject)Instantiate(enemy, start.position, start.rotation);
        EnemyController ec = enemyGO.GetComponent<EnemyController>();
        if (ec != null)
        {
            ec.SetDest(destination);
        }
    }
}
