using UnityEngine;
using System.Collections;

public class ProjectileMain : MonoBehaviour {
	
	//DEV.TEMP projectile type variables
	public bool DestroyOnGround=false;
	Timer life_time;
	Transform graphics;
	
	// Use this for initialization
	void Start () {
		life_time=new Timer(10000,OnDeath);
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
	
	public void OnCollision(Collision other){
		if (other.gameObject.tag=="Ground"){
			if (DestroyOnGround)
				Destroy(this);
		}
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
