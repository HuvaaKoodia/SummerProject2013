using UnityEngine;
using System.Collections;

public class ProjectileMain : MonoBehaviour {
	
	public ProjectileStats stats;
	
	public Timer life_time;
	Transform graphics;
	
	public float SpeedMulti;
	
	public Vector3 MoveDirection {get;private set;}
	public float MoveSpeed {get;private set;}

	// Use this for initialization
	void Awake () {
		life_time=new Timer(1000,OnDeath,false);
		graphics=transform.Find("Graphics") as Transform;
		SpeedMulti=oldSpeedMulti=1f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//rigidbody.isKinematic=true;
		//transform.position+=move_direction;
		//rigidbody.velocity=rigidbody.velocity.normalized*MoveSpeed*SpeedMulti;
		
		if (SpeedMulti!=1){
			if (oldSpeedMulti==1){
				MoveDirection*=SpeedMulti;
				rigidbody.velocity=MoveDirection;
			}	
		}
		else{
			if (oldSpeedMulti!=1){
				MoveDirection/=oldSpeedMulti;
				rigidbody.velocity=MoveDirection;
			}
		}
		
		oldSpeedMulti=SpeedMulti;
		SpeedMulti=1;
	}
	public void setDirection(Vector3 newDir){
		MoveDirection=newDir.normalized*MoveDirection.magnitude;
		rigidbody.velocity=MoveDirection;
	}
	void Update(){
		life_time.Update();
	}
	
	float oldSpeedMulti=1f;
	
	/// <summary>
	///Does not normalize.
	/// </param>
	public void setDirection(Vector3 direction,float speed){
		
		MoveSpeed=speed;
		MoveDirection=direction*speed;
		rigidbody.velocity=MoveDirection;
	}

	//DEV.
	public void changeMaterialColor(Color color){
		graphics.renderer.material.color=color;
	}
	
	public void OnDeath(){
		if (gameObject!=null)
			Destroy(gameObject);
	}
	
	public void OnDestroy(){
		//life_time.Destroy();
	}
	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag=="Player"){
			var player=other.gameObject.transform.GetComponent<PlayerMain>();
			player.HP-=stats.Damage;//other.impactForceSum.magnitude/10;
		}
		//MoveSpeed=rigidbody.velocity.magnitude;
	}
}
