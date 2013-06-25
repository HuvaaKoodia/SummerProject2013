using UnityEngine;
using System.Collections;

public class IsExplosive : MonoBehaviour,ProjectileModifier {
	GameObject explosion;
	Timer time;
	bool activated=false;
	GameObject boxCollider;
	// Use this for initialization
	void Start () {
		var exp_pre=Resources.Load("BoxCollider") as GameObject;
		boxCollider=Instantiate(exp_pre, transform.position,Quaternion.identity) as GameObject;
		boxCollider.transform.parent=transform;
		
		explosion=Resources.Load("Explosion") as GameObject;
		Explosion expl = explosion.GetComponent<Explosion>();
		expl.force=GetComponent<ProjectileMain>().stats.Knockback;
		expl.radius=GetComponent<ProjectileMain>().stats.Radius;
		time=new Timer(2000 ,activateMine);
		rigidbody.collisionDetectionMode=CollisionDetectionMode.Discrete;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay(Collider other){
		
	if (other.gameObject.tag=="Player"&&activated){
			Instantiate(explosion,transform.position,Quaternion.identity);
			
		Destroy(gameObject);
		}
	}
	void activateMine(){
	activated=true;
	}
	
}
