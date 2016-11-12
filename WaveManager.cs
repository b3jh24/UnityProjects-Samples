using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour {

	public Transform[] spawnPoints;

	private int waveNum;					//the current wave number
	private int startingNumEnemies = 6;		//the first wave will spawn 15 total enemies - 5 of each type
	
	SpawnEnemy spawner;						//reference to the SpawnEnemy script

	List<GameObject> currentEnemies;		//List of the enemies that are currently alive 
										//- used to determine when to change waves

	float timeSinceWaveStart;

	float timeTillWaveBegins = 4;		//we wait 4 seconds before beginning wave
	float timer;						//used to track time until wave begins - different from timeSinceWaveStart
	// Use this for initialization
	void Start () {
		waveNum = 1;
		spawner = GetComponent<SpawnEnemy>();
		spawner.setHowMany(numberOfEnemiesPerWave());
		currentEnemies = new List<GameObject>();
	}


	/// <summary>
	/// Algorithm for determining the number of enemies to be spawned each wave.
	/// </summary>
	/// <returns>The number of enemies per wave.</returns>

	void Update(){
		timeSinceWaveStart += Time.deltaTime;
		if(currentEnemies.Count == 0 && timeSinceWaveStart >= 1){
			//if there are no current enemies i.e. they're all dead && the wave didn't just begin, start new wave
			timer+= Time.deltaTime;
			if(timer >= timeTillWaveBegins){
				StartNewWave();
				Debug.Log("Starting new wave...");
			}
		}

	//Debug Purposes
		if(Input.GetKeyDown(KeyCode.V)){
			currentEnemies.Clear();
		}

		if(Input.GetKeyDown(KeyCode.B)){
			foreach (Object o in currentEnemies){
				Debug.Log("Current Enemies: "+o);
			}
		}
	}

	int numberOfEnemiesPerWave(){
		//right now, this is just a linear function that increases by 3 each wave - simple, but should work
		//because we divide this number by 3 in the SpawnEnemy class, we only increase each enemy type by 1 each round
		return startingNumEnemies + (3 * waveNum);
	}


	void StartNewWave(){
		waveNum++;
		spawner.setHowMany(numberOfEnemiesPerWave());
		SpawnEnemy.randomNum = Random.Range(1,3).ToString();
		spawner.BeginSpawningNewWave();
		timeSinceWaveStart = 0;
		timer = 0;
	}

	public int GetWaveNum(){
		return waveNum;
	}


	/// <summary>
	/// Used by SpawnEnemy to add enemies to the list
	/// Adds the enemy to currentEnemies list.
	/// </summary>
	/// <param name="enemy">Enemy to add.</param>
	public void AddEnemyToList(GameObject enemy){
		currentEnemies.Add(enemy);
	}

	/// <summary>
	/// Used by EnemyBehavior script
	/// Removes the enemy from currentEnemy list.
	/// </summary>
	/// <param name="enemy">Enemy to remove.</param>
	public void RemoveEnemyFromList(GameObject enemy){
		Debug.Log(enemy);
		if(!currentEnemies.Contains(enemy)){
			Debug.LogError("No such enemy exists in our list!!");
		}
		else{
			currentEnemies.Remove(enemy);
		}
	}


}
