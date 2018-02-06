using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.UpArrow)){
			print ("uparrow");
		}
		
	
	}
}
