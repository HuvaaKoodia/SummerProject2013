using UnityEngine;
using System.Collections;
using System.Linq;

public class ExplodesOnDestroy : MonoBehaviour,ProjectileModifier {
	GameObject explosion;
	ProjectileMain proj_main;
	public Vector3 extraVector;
	Timer time,startTime;
	
	// Use this for initialization
	void Start () {
		explosion=Resources.Load("Explosion") as GameObject;
		proj_main=GetComponent<ProjectileMain>();
		
	}

	void OnDestroy(){
		
		GameObject obj = Instantiate(explosion,transform.position + extraVector,Quaternion.identity) as GameObject;
		
		var expl = obj.GetComponent<ExplosionScr>();
		
		expl.radius = proj_main.mod_stats.Radius;
		expl.force = proj_main.mod_stats.Knockback;
	}
}
