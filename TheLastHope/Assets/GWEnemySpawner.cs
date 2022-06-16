using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GWEnemySpawner : MonoBehaviour {

    public bool stopSpawning;

    [Range(0, 300)]
    public float spawnRadius;

    [Range(0, 15)]
    public float spawnInterval;

    public float remainingTimeTillSpawn;

    public GameObject spawnedEnemiesContainer;

    private Vector3 lastSpawnPos;


    public GWEnemyController[] enemiesCollection;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

        if (this.stopSpawning) {
            return;
        }

        if (this.remainingTimeTillSpawn < 0) {
            this.SpawnRandom();
            this.remainingTimeTillSpawn = this.spawnInterval;
        }
        this.remainingTimeTillSpawn -= Time.deltaTime;
    }

    void OnDrawGizmos() {

        if (GWPawnController.instance) {
            Gizmos.DrawWireSphere(GWPawnController.instance.transform.position, this.spawnRadius);
        }

        if (this.lastSpawnPos != Vector3.zero) {
            Gizmos.DrawSphere(this.lastSpawnPos, 5);
        }
    }

    void SpawnRandom() {


        int randomIndex = Random.Range(0, this.enemiesCollection.Length - 1);
        GWEnemyController spawnedEnemy = GameObject.Instantiate(this.enemiesCollection[randomIndex], this.spawnedEnemiesContainer.transform);

        this.lastSpawnPos = Random.insideUnitSphere * this.spawnRadius + GWPawnController.instance.transform.position;

        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(this.lastSpawnPos, out myNavHit, 1000,-1)) {
            this.lastSpawnPos = myNavHit.position;
        }
        else {
            throw new System.Exception("Could not get SamplePosition on NavMesh!");
        }

        
        spawnedEnemy.transform.position = this.lastSpawnPos;
        

    }
}
