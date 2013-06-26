using UnityEngine;
using System.Collections;

public class ProjectileMain : MonoBehaviour {
	
	public ProjectileStats stats;
	public PlayerMain Creator;
	
	public Timer life_time;
	Transform graphics;
	public SoundMain sound;
	public StoreSounds sfx;
	
	//movement
	public float SpeedMulti;
	public bool GravityOn=false;
	
	private Vector3 move_direction;
	public Vector3 MoveDirection {get{return move_direction;}}
	public float MoveSpeed {get;private set;}
	
	//stats
	public float Power,Knockback;
	
	
	// Use this for initialization
	void Awake () {
		life_time=new Timer(1000,OnDeath,false);
		graphics=transform.Find("Graphics") as Transform;
		SpeedMulti=oldSpeedMulti=1f;
	}
	void Start(){
		if(sound!=null)
		sound.sfx=sfx;
		

		rigidbody.useGravity=GravityOn;
	}
	// Update is called once per frame
	void FixedUpdate () {
		//rigidbody.isKinematic=true;
		//transform.position+=move_direction;
		//rigidbody.velocity=rigidbody.velocity.normalized*MoveSpeed*SpeedMulti;
		
		//setDirection(rigidbody.velocity);
		
		if (SpeedMulti!=1){
			if (oldSpeedMulti==1){
				//move_direction.x*=SpeedMulti;
				//move_direction.z*=SpeedMulti;
				move_direction*=SpeedMulti;
				rigidbody.velocity=move_direction;
			}	
		}
		else{
			if (oldSpeedMulti!=1){
				//move_direction.x/=oldSpeedMulti;
				//move_direction.z/=oldSpeedMulti;
				move_direction/=SpeedMulti;
				rigidbody.velocity=move_direction;
			}
		}
		
		//if (GravityOn){
			//rigidbody.useGravity=true;
			//setDirection(rigidbody.velocity);
			//move_direction.y+=Time.deltaTime*Physics.gravity.y*SpeedMulti;
			//rigidbody.velocity=MoveDirection;
		//}
		
		//rigidbody.velocity=move_direction;
		//transform.position+=move_direction*SpeedMulti*Time.deltaTime;
		
		oldSpeedMulti=SpeedMulti;
		SpeedMulti=1;
	}

	void Update(){
		life_time.Update();
	}
	
	float oldSpeedMulti=1f;

	//DEV.
	public void changeMaterialColor(Color color){
		graphics.renderer.material.color=color;
	}
	
	public void OnDeath(){
		if (gameObject!=null)
			Destroy(gameObject);
	}
	
	public void OnDestroy(){

		if(sound!=null){
			sound.detach();
		}

		//life_time.Destroy();
	}
	
	void OnCollisionEnter(Collision other){
		if(sound!=null){
			sound.playCollisionSound();
			if (other.gameObject.tag=="Player"){
				var player=other.gameObject.transform.GetComponent<PlayerMain>();
				player.HP-=Power;//other.impactForceSum.magnitude/10;
				Debug.Log("DMG: "+Power);
			}
		}
		
		//move_direction=MoveSpeed*(other..transform.position-transform.position);
		//move_direction=other.relativeVelocity;
	}
	
	
	/// <summary>
	/// Use on start up.
	/// Sets the move direction to a new direction and speed.
	/// </param>
	public void setDirection(Vector3 direction,float speed){
		MoveSpeed=speed;
		move_direction=direction*MoveSpeed;
		rigidbody.velocity=move_direction;
	}
	/// <summary>
	/// Sets the move direction to a new direction.
	/// Preserves speed.
	/// </summary>
	public void setDirection(Vector3 newDir){
		move_direction=newDir;
		rigidbody.velocity=move_direction;
	}
	/*
	/// <summary>
	/// Resets the move direction.
	/// </summary>
	public void setMoveDirection(Vector3 direction)
	{
		move_direction=direction;
		rigidbody.velocity=MoveDirection;
	}
	
	/// <summary>
	/// adds velocity to the move direction.
	/// Takes speed multi into consideration.
	/// </summary>
	public void addMoveDirection(Vector3 direction)
	{
		move_direction+=direction*SpeedMulti;
		rigidbody.velocity=MoveDirection;
	}
	*/
}
