using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {
	
	public GameObject enemy01;
	public GameObject enemy02;
	public GameObject enemy03;
	
	public Transform[] spawnPoints;
	
	WaveManager wm;							//reference to the WaveManager script
	
	int enemy01_count;
	int enemy02_count;
	int enemy03_count;
	
	public static bool spawnedEnough;		//used to see when we've spawned enough of each enemy
	int numToSpawn;
	
	public static string randomNum;			//used to pick a random spawn spot from the WaveManager script
	
	float timer01;
	float timer02;
	float timer03;
	
	public float timeBetweenSpawns = 1f;		//wait 1 seconds before spawning another enemy
	// Use this for initialization
	void Start () {
		enemy01_count = 0;
		enemy02_count = 0;
		enemy03_count = 0;
		
		wm = GetComponent<WaveManager>();
	}
	
	// Update is called once per frame
	void Update () {
		timer01 += Time.deltaTime;
		timer02 += Time.deltaTime;
		timer03 += Time.deltaTime;
		
		foreach (Transform sp in spawnPoints){
			if(wm.GetWaveNum() % 2 > 0){
				if(sp.name.Equals("Spawnspot01") && spawnedEnough == false && timer01 >= timeBetweenSpawns){
					//spawn the enemy01 here
					Spawn(sp.transform.position, 1, numToSpawn);
					timer01 = 0;
				}
				else if(sp.name.Equals("Spawnspot02") && spawnedEnough == false && timer02 >= timeBetweenSpawns){
					//spawn the enemy02 here
					Spawn(sp.transform.position, 2, numToSpawn);
					timer02 = 0;
				}
				else if(sp.name.Equals("Spawnspot03") && spawnedEnough == false && timer03 >= timeBetweenSpawns){
					//spawn the enemy03 here
					Spawn(sp.transform.position, 3, numToSpawn);
					timer03 = 0;
				}
			}
			else{
				if(sp.name.Equals("Spawnspot0"+randomNum) && spawnedEnough == false && timer01 >= timeBetweenSpawns){
					//spawn the enemy01 here
					Spawn(sp.transform.position, 1, numToSpawn);
					timer01 = 0;
				}
				else if(sp.name.Equals("Spawnspot04") && spawnedEnough == false && timer02 >= timeBetweenSpawns){
					//spawn the enemy02 here
					Spawn(sp.transform.position, 2, numToSpawn);
					timer02 = 0;
				}
				else if(sp.name.Equals("Spawnspot05") && spawnedEnough == false && timer03 >= timeBetweenSpawns){
					//spawn the enemy03 here
					Spawn(sp.transform.position, 3, numToSpawn);
					timer03 = 0;
				}
			}
		}
	}
	
	/// <summary>
	/// Spawn the enemy at the specified position, and spawn the correct amount of enemies of that type.
	/// </summary>
	/// <param name="spawnPosition">Spawn position.</param>
	/// <param name="enemyNum">What kind of enemy is it (identified by a number).</param>
	/// <param name="howMany">How many of that enemy should we spawn.</param>
	public void Spawn(Vector3 spawnPosition, int enemyNum, int howMany){
		
		if(enemyNum == 1 && enemy01_count < numToSpawn){
			Instantiate(enemy01, spawnPosition, Quaternion.identity);
			enemy01_count++;
		}
		else if(enemyNum == 2 && enemy02_count < numToSpawn){
			Instantiate(enemy02, spawnPosition, Quaternion.identity);
			enemy02_count++;
		}
		else if (enemyNum == 3 && enemy03_count < numToSpawn){
			Instantiate(enemy03, spawnPosition, Quaternion.identity);
			enemy03_count++;
		}
		else{
			//we haven't met any of the above conditions, so either there is a bug, or we have spawned enough
			spawnedEnough = true;
		}
	}
	
	public void BeginSpawningNewWave(){
		enemy01_count = 0;
		enemy02_count = 0;
		enemy03_count = 0;
		spawnedEnough = false;
	}
	
	public void setHowMany(int howMany){
		numToSpawn = howMany / 3;
	}
}
