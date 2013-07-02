using UnityEngine;
using System.Collections;
using System.Linq;

public class ExplodesOnDestroy : MonoBehaviour,ProjectileModifier {
	GameObject explosion;
	ProjectileMain proj_main;
	Timer time,startTime;
	
	// Use this for initialization
	void Start () {
		explosion=Resources.Load("Explosion") as GameObject;
		proj_main=GetComponent<ProjectileMain>();
		
	}

	void OnDestroy(){
		
		GameObject obj = Instantiate(explosion,transform.position,Quaternion.identity) as GameObject;
		var expl = obj.GetComponent<ExplosionScr>();
		
		expl.radius = proj_main.stats.Radius;
		expl.force = proj_main.stats.Knockback;
	}
}
