using UnityEngine;
using System.Collections;

public class ProjectileMain : MonoBehaviour {
	
	//stats
	public ProjectileStats original_stats;
	public ProjectileStatsContainer mod_stats;
	public PlayerMain Creator;
	
	public Timer life_time;
	
	//audiovisuals
	public SoundMain sound;
	public StoreSounds sfx;
	public Transform graphics{get;private set;}
	
	//movement
	public float SpeedMulti;
	
	private Vector3 move_direction;
	public Vector3 MoveDirection {get{return move_direction;}}
	public float MoveSpeed {get;private set;}
	
	// Use this for initialization
	void Awake (){
		life_time=new Timer(10000,OnDeath,false);
		graphics=transform.Find("Graphics") as Transform;
		SpeedMulti=oldSpeedMulti=1f;
	}
	void Start(){
		if(sound!=null)
		sound.sfx=sfx;
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
				MoveSpeed*=SpeedMulti;
				rigidbody.velocity=move_direction;
			}	
		}
		else{
			if (oldSpeedMulti!=1){
				move_direction.x/=oldSpeedMulti;
				move_direction.z/=oldSpeedMulti;
				//move_direction/=oldSpeedMulti;
				MoveSpeed/=SpeedMulti;
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
		}
		if (other.gameObject.tag=="Player"){
			var player=other.gameObject.transform.GetComponent<PlayerMain>();
			player.HP-=mod_stats.Power;//other.impactForceSum.magnitude/10;
		}
		if (other.gameObject.tag=="Projectile"){
			if (mod_stats.HP>0){
				var pro=other.gameObject.transform.GetComponent<ProjectileMain>();
				if (pro!=last_hit){
					last_hit=pro;
					mod_stats.HP-=pro.mod_stats.Power;
					if (mod_stats.HP<=0){
						Destroy(gameObject);
					}
				}
			}
		}
		//move_direction=MoveSpeed*(other..transform.position-transform.position);
		//move_direction=other.relativeVelocity;
	}
	
	ProjectileMain last_hit;//HAX!
	
	
	/// <summary>
	/// Use on start up.
	/// Sets the move direction to a new direction and speed.
	/// Normalizes direction.
	/// </param>
	public void setDirection(Vector3 direction,float speed){
		MoveSpeed=speed;
		move_direction=direction.normalized*MoveSpeed;
		rigidbody.velocity=move_direction;
	}
	/// <summary>
	/// Sets the move direction to a new direction.
	/// Preserves original speed.
	/// </summary>
	public void setDirection(Vector3 newDir){
		move_direction=newDir.normalized*rigidbody.velocity.magnitude;
		rigidbody.velocity=move_direction;
	}
	
	/// <summary>
	/// Sets the move velocity to a new one.
	/// </summary>
	public void setVelocity(Vector3 newDir){
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
