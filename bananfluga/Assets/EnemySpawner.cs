using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyType;
    public BoxCollider spawnRegion;
    public float spawnInterval = 0.5f;

    public GameObject ground;
    int minEnemies = 150;
    float secondsUntilNextSpawn = 0.0f;


	// Use this for initialization
	void Start () {
        int numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        for(int i = numEnemies; i < minEnemies; i++)
        {
            SpawnEnemy();
        }
	}

    void SpawnEnemy() {
        Vector3 spawnLocation = new Vector3(Random.Range(spawnRegion.bounds.min.x, spawnRegion.bounds.max.x), ground.GetComponent<MeshCollider>().bounds.max.y  + 1, Random.Range(spawnRegion.bounds.min.z, spawnRegion.bounds.max.z));
        GameObject.Instantiate(enemyType, spawnLocation, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
        int numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        for (int i = numEnemies; i < minEnemies; i++)
        {
            SpawnEnemy();
            //Debug.Log("Spawning!");
        }

        secondsUntilNextSpawn -= Time.deltaTime;
        if(secondsUntilNextSpawn <= 0)
        {
            SpawnEnemy();
            secondsUntilNextSpawn += spawnInterval;
        }
    }
}
