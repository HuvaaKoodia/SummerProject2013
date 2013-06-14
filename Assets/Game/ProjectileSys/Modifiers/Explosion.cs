using UnityEngine;
using System.Collections;
using NotificationSys;

public class Explosion : MonoBehaviour {
	public float radius, force;
	public Vector3 pos;
	//Explosion newExp;
	/*
	public Explosion (Vector3 pos, float radius, float force){
		this.radius=radius;
		this.force=force;
		this.pos=pos;
	}*/
	// Use this for initialization
	void Start () {
		NotificationCenter.Instance.sendNotification(new Explosion_note(transform.position, 1000000f, 3f));
	}
	
	// Update is called once per frame
	void Update () {
		if(particleSystem.isStopped){
		Destroy(gameObject);
		}
		}
	
}
