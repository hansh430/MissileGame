using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnner : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] spawnPositions;
    public GameObject[] coins;
    public GameObject[] coinsSpawnPositions;
   
    public float spawnRate;
    public bool isSpawn;
    private float nextSpawn; 
    void Start()
    {
        isSpawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextSpawn > 0)
        {
            nextSpawn -= Time.deltaTime;
        }
        if(nextSpawn <= 0 && isSpawn)
        {
            EnemySpawn();
            CoinSpawn();
        }
     
    }

    void EnemySpawn()
    {
        nextSpawn = spawnRate;
        Vector2 position = spawnPositions[Random.Range(0, spawnPositions.Length)].transform.position;
        GameObject enemyClone = Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector2(position.x, position.y), transform.rotation);
        enemyClone.transform.parent = transform;
        enemyClone.SetActive(true);
    }

    void CoinSpawn()
    {
        nextSpawn = spawnRate;
        Vector2 position = coinsSpawnPositions[Random.Range(0, coinsSpawnPositions.Length)].transform.position;
        GameObject coinClone = Instantiate(coins[Random.Range(0, coins.Length)], new Vector2(position.x, position.y), transform.rotation);
        coinClone.transform.parent = transform;
        coinClone.SetActive(true);
    }
}
