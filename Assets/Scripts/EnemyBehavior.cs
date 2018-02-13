using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public float health = 150f;
	public GameObject projectilePrefab;
	public float projectileSpeed = 10f;
	public float shotsPerSecond = 0.5f;
	public int scoreValue = 150;
	public AudioClip fireSfx;
	public AudioClip deathSfx;
	public GameObject explosionParticles;
	
	private ScoreKeeper scoreKeeper;
	
	void Start () {
	//recordar que no estas buscando el componente dentro del script sino dentro del gameObject.
		scoreKeeper = GameObject.Find("ScoreTxt").GetComponent<ScoreKeeper>();
		
	}
	
	void Update () {
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability){
			Fire();
		}
		
	}
		
	void OnTriggerEnter2D (Collider2D col) {
		Projectile laser = col.gameObject.GetComponent<Projectile>();
		//eh mira.If para triggers.
		if (laser) {
			health -= laser.GetDamage();
			if (health <=0f) {
				Destroy(gameObject);
				scoreKeeper.Score(scoreValue);
				AudioSource.PlayClipAtPoint(deathSfx,transform.position,0.6f);
				Instantiate(explosionParticles,transform.position,Quaternion.identity);
			}	
		}
	}
	
	
	void Fire(){
		Vector3 startPosition = transform.position + new Vector3 (0,-1,0);
		GameObject projectile = Instantiate(projectilePrefab, startPosition, Quaternion.identity) as GameObject;
		projectile.rigidbody2D.velocity = new Vector3(0,-projectileSpeed,0);
		AudioSource.PlayClipAtPoint(fireSfx,transform.position,0.5f);
	}
}
