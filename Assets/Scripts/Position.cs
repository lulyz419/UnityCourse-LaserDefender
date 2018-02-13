using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	void OnDrawGizmos () {
	//Dibuja gizmos en el viewport
		Gizmos.DrawWireSphere(transform.position, 1);
	}
}
