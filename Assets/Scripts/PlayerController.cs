using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float speed = 10f;
	public GameObject projectilePrefab;
	public float projectileSpeed;
	public float firingRate = 0.2f;
	public float health = 250f;
	public AudioClip fireSfx;
	public AudioClip deathSfx;
	public float volume = 1f;
	
	
	float xmin;
	float xmax;
		
	void Start () {
		CameraClamp();		
	}
	
	
	void CameraClamp() {
		//Este tipo de Vector3(ViewportToWorldPoint) funciona de manera que X e Y tienen un valor de 0-1(izq y abajo menor), y Z es
		// la distancia entre la camara y el objeto (lo calculamos restando la posicion en z de la camara a la posicion en z del objeto).
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
		//+ padding.
		xmin = leftmost.x + 0.5f;
		xmax = rightmost.x - 0.5f;
	}
	
	void Fire(){
		//Instanciamos proyectiles.
		if (Input.GetKey(KeyCode.Space)) {
			Vector3 offset = new Vector3 (0,1,0);
			GameObject projectile = Instantiate(projectilePrefab, transform.position + offset, Quaternion.identity) as GameObject;
			projectile.rigidbody2D.velocity = new Vector3 (0,projectileSpeed,0);
			//PlayClipAtPoint!!!!
			AudioSource.PlayClipAtPoint(fireSfx,transform.position, volume);
		}
	}
	
	void Update () {
		//InvokeRepeating invoca un metodo cada cierto tiempo (el que se le ponga). Lo usamos para que los proyectiles se creen en un rate,
		//no todos seguidos.
		if (Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.000001f,firingRate);
		}
		if (Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
		
	
		if (Input.GetKey(KeyCode.UpArrow)){
			//Tambien puede escribirse como transform.position += Vector3.left,right... * speed * Time.deltaTime;
			transform.position += new Vector3 (0f, speed * Time.deltaTime, 0f);		
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			transform.position += new Vector3 (0f, -speed * Time.deltaTime, 0f);
		} else if (Input.GetKey(KeyCode.LeftArrow)){
			transform.position += new Vector3 (-speed * Time.deltaTime, 0f,0f);	
		} else if (Input.GetKey(KeyCode.RightArrow)){
			transform.position += new Vector3 (speed * Time.deltaTime, 0f,0f);
		} 
		//Restringimos el movimiento al PlaySpace.
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
				
	}
		
	void OnTriggerEnter2D (Collider2D col) {
		Projectile laser = col.gameObject.GetComponent<Projectile>();
		if (laser) {
			health -= laser.GetDamage();
			laser.Hit();
			if (health <=0f) {
			Destroy(gameObject);
				AudioSource.PlayClipAtPoint(deathSfx,transform.position,0.9f);
				Die();				
			}	
		}
	}
	//Morimos y vamos a la pantalla de morir.
	void Die(){
		Destroy(gameObject);
		LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Win Screen");
	}

}
