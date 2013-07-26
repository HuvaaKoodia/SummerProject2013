using UnityEngine;
using System.Collections;

public class ProximityExplosive : MonoBehaviour,ProjectileModifier
{
	Timer time;
	bool activated = false;
	public Vector3 extraVector;
	//GameObject boxCollider;
	
	// Use this for initialization
	void Start ()
	{		
		var expl_dest = GetComponent<ExplodesOnDestroy>() as ExplodesOnDestroy;
		expl_dest.extraVector= extraVector;
		time = new Timer (2000, activateMine);
	}
	
	// Update is called once per frame
	void Update ()
	{
		time.Update();
		if (rigidbody.velocity.magnitude > 0.05f)
			rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		else
			rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
	}
	
	/*void OnTriggerStay (Collider other)
	{
		if (activated){
			if (other.gameObject.tag == "Player") {
				Destroy (gameObject);
			}
		}
	}*/
	
	void OnCollisionEnter (Collision other)
	{
		if (activated){
			if (other.gameObject.tag == "Projectile"||other.gameObject.tag=="Player") {
				Destroy (gameObject);
			}
		}
	}

	void activateMine ()
	{
		gameObject.layer=LayerMask.NameToLayer("Projectile");
		activated = true;
	}
	
}
