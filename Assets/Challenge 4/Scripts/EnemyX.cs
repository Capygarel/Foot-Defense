using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    public GameObject playerGoal;
    public GameObject spawnManager;
    private int minXBound = -23;
    private int maxXBound = 23;
    private int minZBound = -13;
    private int maxZBound = 34;
    private int minYBound = -1;
    private int maxYBound = 10;

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
        if (transform.position.y < minYBound || maxYBound < transform.position.y || transform.position.x < minXBound || maxXBound < transform.position.x || transform.position.z < minZBound || maxZBound < transform.position.z)
            Destroy(gameObject);

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
