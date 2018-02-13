using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) {
	//no queremos destruir el collider, sino el objeto al que pertenece el collider que colisiona, 
	//asi que llamamos al objeto dentro del collider.(col.gameObject)
		Destroy(col.gameObject);
	}
}
