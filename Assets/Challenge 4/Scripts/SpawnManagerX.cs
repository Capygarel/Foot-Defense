using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    private int currentEnemyCount;
    private int maxEnemy = 0;
    private int waveCount = 1;

    private int scorePlayer = 0;
    private int scoreEnemy = 0;


    public GameObject player; 

    // Update is called once per frame
    void Update()
    {
        currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (currentEnemyCount == 0)
        {
            if (scorePlayer >= scoreEnemy)
                maxEnemy++;
            Debug.Log("Vague n°" + waveCount + "  , nombre d'ennemis : " + maxEnemy);
            SpawnEnemyWave(maxEnemy);
        }

    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }

    public void addScorePlayer()
    {
        scorePlayer++;
    }

    public void addScoreEnemy()
    {
        scoreEnemy++;
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end

        // If no powerups remain, spawn a powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
        {
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            enemyPrefab.GetComponent<EnemyX>().speed = waveCount * 20;
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        waveCount++;
        ResetPlayerPosition(); // put player back at start

    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition ()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

    }

}
