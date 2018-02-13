using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float damage = 100f;
	
	public float GetDamage(){
		return damage;
	}
	
	public void Hit (){
		//por alguna razon no pilla esto en otros scripts.
		Destroy(gameObject);
	}
}
