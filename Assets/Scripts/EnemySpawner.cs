﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;
	
	private bool movingRight = true;
	private float xmax;
	private float xmin;
	

	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1,0,distanceToCamera));
		xmin = leftBoundary.x;
		xmax = rightBoundary.x;
		
		SpawnUntilFull();
		
	}
	
	
	public void OnDrawGizmos () {
		Gizmos.DrawWireCube(transform.position, new Vector3 (width, height,0));
	}
	
	// Update is called once per frame
	void Update () {
	
		if (movingRight) {
			//o: transform.position += new Vector3 (speed*Time.deltaTime, 0,0)
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		
		float rightEdgeOfFormation = transform.position.x + (0.5f*width);
		float leftEdgeOfFormation = transform.position.x - (0.5f*width);
		
		if (leftEdgeOfFormation < xmin) {
			movingRight = true;
		} else if (rightEdgeOfFormation > xmax) {
			movingRight = false;
		}
		
		if (AllMembersDead()){
			print ("Eh que se han muerto todos");
			SpawnUntilFull();
		}
						
	}
	
	void SpawnEnemies(){
		foreach (Transform child in transform) {
			//Instanciamos el Game Object DENTRO de EnemyFormation, como hijos.
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}
	
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if (freePosition){
			GameObject enemy = Instantiate(enemyPrefab,freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition()){
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
	
	Transform NextFreePosition (){
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;		
	}
	
	//Vemos si los enemigos estan muertos o k.
	bool AllMembersDead(){
		//no entiendo esta mierda.
		foreach(Transform childPositionGameObject in transform){
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}
	
		
}
