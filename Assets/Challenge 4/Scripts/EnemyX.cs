using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    public GameObject playerGoal;
    public GameObject spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Player Goal");
        spawnManager = GameObject.Find("Spawn Manager");

    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
        if (transform.position.y < -2) Destroy(gameObject);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            spawnManager.GetComponent<SpawnManagerX>().addScorePlayer();
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            spawnManager.GetComponent<SpawnManagerX>().addScoreEnemy();
            Destroy(gameObject);
        }

    }

}
