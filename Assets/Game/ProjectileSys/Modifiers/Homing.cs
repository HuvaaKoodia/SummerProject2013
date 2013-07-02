using UnityEngine;
using System.Collections;

public class Homing : MonoBehaviour,ProjectileModifier {
	Transform target;
	ProjectileMain projectile;
	BoxCollider boxCollider;
	GameObject ConeCollider;
	float time = 0f,traceTime = 1f;
	// Use this for initialization
	void Start () {
		projectile = GetComponent<ProjectileMain>();

		var cc_pre=Resources.Load("ConeCollider") as GameObject;
		ConeCollider=Instantiate(cc_pre,transform.position,Quaternion.identity) as GameObject;
		ConeCollider.transform.parent=transform;
		ConeCollider.GetComponent<RotateToProjectileScr>().pro=projectile;
		time=traceTime;
	}
	
	// Update is called once per frame
	void Update (){
		if(target!=null){
			projectile.setDirection(Vector3.Slerp(projectile.MoveDirection, (target.position-projectile.transform.position), Time.deltaTime*4));
			time-=Time.deltaTime;
			
			if(time<=0){
				target=null;
				time=traceTime;
			}
		}
	}
	void OnTriggerStay(Collider other){
		if(target!=null){
			return;
		}
		if(other.gameObject.tag=="Player"){
			target=other.transform;
		}
	}
	
}
