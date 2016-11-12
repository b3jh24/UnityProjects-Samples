using UnityEngine;
using System.Collections;

public class RespawnPlayer : MonoBehaviour {

	public float respawnTimer = 5;			//5 seconds until the player respawns

	public GameObject playerRespawn;

	[System.NonSerialized]
	public static bool canRespawn = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(canRespawn){
			respawnTimer -= Time.deltaTime;
			if(respawnTimer <= 0){
				BeginRespawn();
			}
		}
	}

	void BeginRespawn(){
		canRespawn = false;
		respawnTimer = 5;
		Instantiate(playerRespawn, new Vector3(-2.5f,0,0), Quaternion.identity);
	}


	/// <summary>
	/// Sets the object that we will respawn - exactly as it previously existed.
	/// </summary>
	/// <param name="whatToRespawn">The GameObject to respawn.</param>
	public void SetRespawn(GameObject whatToRespawn){
		playerRespawn = whatToRespawn;
		Debug.Log("PlayerRespawn: "+playerRespawn);
	}
}
