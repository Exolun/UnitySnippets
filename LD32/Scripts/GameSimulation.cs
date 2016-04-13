using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSimulation : MonoBehaviour {
	public GameObject[] Wave1Spawners;
	public GameObject[] Wave2Spawners;
	public GameObject[] Wave3Spawners;


	private List<GameObject[]> allWaves = new List<GameObject[]>();
	private GameObject[] currentWave;
	private int enemiesAlive = 0;
	
	void Start () {
		this.allWaves.Add (Wave1Spawners);
		this.allWaves.Add (Wave2Spawners);
		this.allWaves.Add (Wave3Spawners);

		this.startNextWave();
	}
	
	void Update () {

		int totalRemaining = 0;
		foreach (var spawner in currentWave) {
			totalRemaining += spawner.GetComponent<SpawnerController>().GetNumRemaining();
		}

		var totalALive = totalRemaining + enemiesAlive;


		//More waves exist, spawn next
		if (totalALive == 0 && allWaves.Count > 0) {
			this.startNextWave();
		}
		//Win condition!  All enemies killed
		else if (allWaves.Count == 0 && totalALive == 0) {
			GameObject.Find("UI").SendMessage("ShowVictoryDialog");
		}

	}

	public void IncrementEnemiesAlive(){
		enemiesAlive++;
	}

	public void DecrementEnemiesAlive(){
		enemiesAlive--;
	}

	private void startNextWave(){
		var next = allWaves[0];
		allWaves.Remove (next);
		foreach (var spawner in next) {
			spawner.SetActive(true);
		}

		currentWave = next;
	}
}
