using UnityEngine;
using System.Collections;
using System.Linq;

public class ExplodesOnContact : MonoBehaviour {
	GameObject explosion;
	ProjectileMain proj_main;
	Timer time,startTime;
	
	// Use this for initialization
	void Start () {
		explosion=Resources.Load("Explosion") as GameObject;
		proj_main=GetComponent<ProjectileMain>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnDestroy(){
		
		GameObject obj = Instantiate(explosion,transform.position,Quaternion.identity) as GameObject;
		Explosion expl = obj.GetComponent<Explosion>();
		
		expl.radius = proj_main.stats.Radius;
		expl.force = proj_main.stats.Knockback;
			}
}
