using UnityEngine;
using System.Collections;

public class SlowMoField : MonoBehaviour,ProjectileModifier {
	
	public float slow_down_speed_multi=0.2f;
	// Use this for initialization
	void Start () {
		rigidbody.isKinematic=true;
	}
	
	void OnTriggerStay(Collider other){
		if (other.gameObject.tag=="Projectile"&&!other.collider.isTrigger){
			var proMain=other.GetComponent<ProjectileMain>();
			proMain.SpeedMulti=slow_down_speed_multi;
		}
	}
}
