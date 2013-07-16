using UnityEngine;
using System.Collections;

public class MultiplyDMGonCollision : MonoBehaviour,ProjectileModifier {
	
	public float power_add_percent=0.1f;
	ProjectileMain pro;
	void Start () {
		pro=GetComponent<ProjectileMain>();
	}
	
	void OnCollisionEnter(Collision other){
		pro.mod_stats.Power*=1f+power_add_percent;
	}
}
