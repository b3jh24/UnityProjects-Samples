using UnityEngine;
using System.Collections.Generic;

public class EnemyBehavior : MonoBehaviour {

	public Transform[] waypoints;		//the array of all of our waypoints and their positions

	public Transform playerBase;		//the player's base - aka the point at which we stop following waypoints

	public GameObject player;			//reference to the Player

	Rigidbody2D enemyRig;

	public float speed = 2f;

	List<Transform> openSet;			//the waypoints we haven't been to yet

	public int attackRadius = 2;		//anything in 2 units in attackable

	public float chargingSpeed = 3f;	//the speed the enemy will charge at the player with
	
	WaveManager wm;

	public float attackAmt = 10f;
	public float baseAttackModifier = 1.6f;
	

	// Use this for initialization
	void Start () {

		//The enemy adds itself to the list of current enemies
		wm = GameObject.FindGameObjectWithTag("GameController").GetComponent<WaveManager>();
		wm.AddEnemyToList(this.gameObject);

		enemyRig = GetComponent<Rigidbody2D>();
		openSet = new List<Transform>();
		foreach(Transform wp in waypoints){
			openSet.Add(wp);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Transform nearestWP = FindNearestWaypoint();

		if(Vector2.Distance(transform.position, nearestWP.position) < .1f){
			//we've arrived at the "closest" waypoint
			//let's ditch the old one, and find a new one
			openSet.Remove(nearestWP);
			FindNearestWaypoint();
		}
		else{
			//we haven't gotten to the closest waypoint yet, so keep moving
			enemyRig.velocity = Movement(nearestWP);
			if((transform.position - playerBase.position).sqrMagnitude <= 12.25){
				//at this point, we are close enough to begin attacking the player's base, so let's move there
				enemyRig.velocity = (playerBase.position - transform.position).normalized * chargingSpeed;
			}
		}

		if(CanAttack()){
			Attack();
		}


	}

	Transform FindNearestWaypoint(){
		float minDistance = Mathf.Infinity;
		Transform closestWP = null;
		foreach (Transform wp in openSet){
			float distance = (wp.position - transform.position).sqrMagnitude;
			if(distance < minDistance){
				//we've found the "closest waypoint"
				minDistance = distance;
				closestWP = wp;
			}
		}
		return closestWP;
	}

	/// <summary>
	/// Returns the Vector2 to move to based on position and waypoints
	/// </summary>
	Vector2 Movement(Transform t){
		Transform waypointToMoveTo = t;			//the closest waypoint that we found
		return (waypointToMoveTo.position - transform.position).normalized * speed;
	}


	/// <summary>
	/// Determines whether the enemy can attack.
	/// </summary>
	/// <returns><c>true</c> if this instance can attack; otherwise, <c>false</c>.</returns>
	bool CanAttack(){
		if(Physics2D.OverlapCircle(transform.position,attackRadius,LayerMask.GetMask("PlayerLayer"))){
			//if we detect that the player is in our attack range
			return true;
		}
		return false;
	}

	void Attack(){
		//do stuff
		GameObject playerInstance = GameObject.FindGameObjectWithTag("Player");
		enemyRig.velocity = (playerInstance.transform.position - transform.position).normalized * chargingSpeed;
	}


	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Player"){
			PlayerHealth ph = col.gameObject.GetComponent<PlayerHealth>();
			ph.TakeDamage(attackAmt);
			Destroy(gameObject);
			Debug.Log("Banzai!");
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "PlayerBase"){
			BaseBehavior bb = col.GetComponent<BaseBehavior>();
			bb.TakeDamage (attackAmt * baseAttackModifier);
			Destroy(gameObject);
		}
	}
}
