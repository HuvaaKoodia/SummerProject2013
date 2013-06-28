using UnityEngine;
using System.Collections;

public class ProximityExplosive : MonoBehaviour,ProjectileModifier
{
	GameObject explosion;
	Timer time;
	bool activated = false;
	GameObject boxCollider;
	// Use this for initialization
	void Start ()
	{
		var exp_pre = Resources.Load ("BoxCollider") as GameObject;
		boxCollider = Instantiate (exp_pre, transform.position, Quaternion.identity) as GameObject;
		boxCollider.transform.parent = transform;
		
		explosion = Resources.Load ("Explosion") as GameObject;
		var expl = explosion.GetComponent<ExplosionScr> ();
		expl.force = GetComponent<ProjectileMain> ().stats.Knockback;
		expl.radius = GetComponent<ProjectileMain> ().stats.Radius;
		time = new Timer (2000, activateMine);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (rigidbody.velocity.magnitude > 0.05f)
			rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		else
			rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
	}
	
	void OnTriggerStay (Collider other)
	{
		if (activated){
			if (other.gameObject.tag == "Player") {
				Destroy (gameObject);
			}
		}
	}
	
	void OnCollisionEnter (Collision other)
	{
		if (activated){
			if (other.gameObject.tag == "Projectile") {
				Destroy (gameObject);
			}
		}
	}

	void OnDestroy(){
		Instantiate (explosion, transform.position, Quaternion.identity);
	}

	void activateMine ()
	{
		activated = true;
	}
	
}
