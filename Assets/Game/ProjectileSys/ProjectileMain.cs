using UnityEngine;
using System.Collections;

public class ProjectileMain : MonoBehaviour {
	
	public Timer life_time;
	Transform graphics;

	// Use this for initialization
	void Awake () {
		life_time=new Timer(1000,OnDeath);
		graphics=transform.Find("Graphics") as Transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/// <summary>
	///Does not normalize.
	/// </param>
	public void setDirection(Vector3 direction,float speed){
		rigidbody.velocity=direction*speed;
	}

	//DEV.
	public void changeMaterialColor(Color color){
		graphics.renderer.material.color=color;
	}
	
	public void OnDeath(){
		Destroy(gameObject);
	}
	
	public void OnDestroy(){
		life_time.Destroy();
	}
}
