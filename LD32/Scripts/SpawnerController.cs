using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour {

	public GameObject EnemyToSpawn;
	public float SpawnDelay = 2;
	public int NumberToSpawnPerRound = 5;
	public int TotalToSpawn = 100;	
	public Vector2 SpawnXVariance;
	public Vector2 SpawnYVariance;

	private float nextSpawnTime  = 0;    
	private int numberSpawned = 0;

	
	public int GetNumRemaining()
	{
		return TotalToSpawn - numberSpawned;
	}
	
	void Update () {
		if (Time.time > nextSpawnTime && numberSpawned < TotalToSpawn) {            
			nextSpawnTime = Time.time + SpawnDelay;
			var player = GameObject.Find("Player");
			
			for(int i = 0; i < NumberToSpawnPerRound;i++){
				var spawnPosition = new Vector3(transform.position.x + Random.Range(SpawnXVariance.x, SpawnXVariance.y), transform.position.y + Random.Range(SpawnYVariance.x, SpawnYVariance.y), transform.position.z);
				GameObject enemy = (GameObject)Instantiate(EnemyToSpawn, spawnPosition, transform.rotation);
				var enemyController = enemy.GetComponent<EnemyController>();       
				enemyController.TargetPlayer = player;
				numberSpawned++;
			}            
		}            
	}
}
