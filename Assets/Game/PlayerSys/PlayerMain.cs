using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NotificationSys;

public class PlayerMain : MonoBehaviour
{
	public PlayerData Data;
	public PlayerGraphicsScr graphics;
	public List<AbilityContainer> ability_containers;
	public int controllerNumber = 0;
	bool restrictLegs = false,invulnerable=false;
		
	float hp = 100;
	public float HP {
		get{ return hp;}
		set{
			if (invulnerable) return;
			hp = Mathf.Clamp(value,0,100);
			if(hp==0) Die();
		}
	}
	
	float mp = 100;
	float MP_regen_multi=5f,MP_regen_multi_normal=5f,MP_regen_add=5.5f;
	public float MP {
		get{ return mp;}
		set{ 
			if (mp>value){
				MP_regen_multi=MP_regen_multi_normal;
				mp_regen_on=false;
				mp_regen_timer.Active=true;
				mp_regen_timer.Reset();
			}
			mp = Mathf.Clamp(value,0,100);
		}
	}
	
	public Vector3 AimDir{get{return last_aim_direction;}}
	public Vector3 UpperTorsoDir{get{return last_upper_direction;}}
	public Vector3 LowerTorsoDir{get{return last_move_direction;}}
	
	bool ignoreExplosion=false;
	
	//private 
	bool onGround, canJump, jumped;
	float acceleration = 10000,
		jump_speed = 7,current_jump_y,
		speed_max = 1f;
	float l_axis_x, l_axis_y, r_axis_x, r_axis_y;
	
	Timer jump_timer,onGround_timer,mp_regen_timer;
	Vector3 last_aim_direction,last_move_direction,last_upper_direction;
	//Vector3 last_aim_point,last_move_point;
	bool destroyed=false,freeze=false,mp_regen_on=false;
	
	//DEV.temp color sys
	//public Color _color=Color.white;
	
	public Color _Color{
		set{
			 graphics.setColor(value);
		}
	}
	
	void Awake ()
	{
		last_aim_direction=last_move_direction=Vector3.forward;
		
		jump_timer = new Timer(400, OnJumpTimer);
		onGround_timer= new Timer(200, OnGroundTimer);
		mp_regen_timer= new Timer(1000, OnMPregenTimer);
	}

	void Start () {
		//Data set
		ability_containers = new List<AbilityContainer> ();
		
		for (int i=0; i<Data.Abilities.Count; i++){
			var abb = new AbilityContainer(this,Data.Abilities[i]);
			ability_containers.Add(abb);
		}
		_Color=Data.color;
		controllerNumber=Data.controllerNumber;
		
		NotificationCenter.Instance.addListener(OnExplosion,NotificationType.Explode);
	}

	void Update ()
	{
		jump_timer.Update();
		onGround_timer.Update();
		mp_regen_timer.Update();
		
		if (freeze)
			restrictLegs=true;
		
		l_axis_x = Input.GetAxis ("L_XAxis_" + controllerNumber);
		l_axis_y = Input.GetAxis ("L_YAxis_" + controllerNumber);
		
		r_axis_x = Input.GetAxis ("R_XAxis_" + controllerNumber);
		r_axis_y = Input.GetAxis ("R_YAxis_" + controllerNumber);
		
		updateRotations();
		if (!freeze){
			if (ability_containers.Count > 0 && Input.GetButton ("RB_" + controllerNumber)){
				ability_containers [0].UseAbility (transform.position, last_upper_direction);
			}
			
			if (ability_containers.Count > 1 && Input.GetButton ("LB_" + controllerNumber)){
				ability_containers [1].UseAbility (transform.position,  last_upper_direction);
			}
			
			if (ability_containers.Count > 2 && Input.GetAxis ("Triggers_" + controllerNumber) < 0) {
				ability_containers [2].UseAbility (transform.position,  last_upper_direction);
			}
			
			if (ability_containers.Count > 3 && Input.GetAxis ("Triggers_" + controllerNumber) > 0) {
				ability_containers [3].UseAbility (transform.position,  last_upper_direction);
			}
		}
		//mp regen
		if (mp_regen_on){
			MP+=Time.deltaTime*MP_regen_multi;
			MP_regen_multi+=Time.deltaTime*MP_regen_add;
		}
		
		
		//dev
		
		if (Input.GetButtonDown("X_" + controllerNumber)){
			graphics.toggleFullbody();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		
		rigidbody.WakeUp ();
		
		if (graphics.LowerTorso.animation!=null){
			graphics.LowerTorso.animation.enabled=false;
			graphics.UpperTorso.animation.enabled=false;
		}
		if (!freeze){
			if(!restrictLegs){
				if (l_axis_x < 0) {
					MoveAround(Vector3.left * acceleration);
				}
				
				if (l_axis_x > 0) {
					MoveAround(Vector3.right * acceleration);
				}
				
				if (l_axis_y < 0) {
					MoveAround(Vector3.forward * acceleration);
				}
				
				if (l_axis_y > 0) {
					MoveAround(Vector3.back * acceleration);
				}
	
			}
			//jump
			if (Input.GetButton ("A_" + controllerNumber) || Input.GetButton ("LS_" + controllerNumber)) {
				if (onGround&&canJump){
					jumped=true;
					canJump = false;
					
					current_jump_y=jump_speed;
					//rigidbody.velocity= new Vector3(rigidbody.velocity.x,jump_speed, rigidbody.velocity.z);
				}
			}
		}
		if (jumped||!onGround){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,current_jump_y, rigidbody.velocity.z);
			current_jump_y+=Physics.gravity.y*Time.deltaTime;
		}
		else{
			current_jump_y=rigidbody.velocity.y;
		}
		
		//if (rigidbody.velocity.y<0)
			//onGround=false;
		
		//DEV.input <
		/*
		if (Input.GetButtonDown("Y_" + controllerNumber)){
			NotificationCenter.Instance.sendNotification(new Explosion_note(transform.position,10000f,20f));
		}*/
	}

	void OnCollisionStay(Collision other)
	{	
		if (other.gameObject.tag=="Hurt"){
			HP-=Time.deltaTime*2;

		}
		
		int angle=10;
		if (other.gameObject.tag=="Gib")
			angle=30;
		
		foreach (var c in other.contacts) {
			if (Vector3.Angle (c.normal, transform.up) < angle){
				
				onGround_timer.Reset();
				onGround_timer.Active=true;
				onGround = true;
				
				if (!jump_timer.Active){
					jump_timer.Reset();
					jump_timer.Active=true;
				}
				
				break;
			}
		}
	}
	
	void OnJumpTimer ()
	{
		canJump=true;
		jump_timer.Active = false;
	}
	void OnGroundTimer ()
	{
		onGround=false;
		jumped=false;
		jump_timer.Active=false;
		onGround_timer.Active=false;
	}
	
	void OnMPregenTimer(){
		mp_regen_on=true;
		mp_regen_timer.Active=false;
	}

	void OnExplosion(Notification note){
		if (destroyed) return;
		var exp=(Explosion_note)note;
		if(!ignoreExplosion){
			rigidbody.AddExplosionForce(exp.Force,exp.Position,exp.Radius);
		}
		ignoreExplosion=false;
	}

	//DEV. bugs out a bit
	void MoveAround(Vector3 force){	
		if (jumped||!onGround)
			restrictMovement();
		
		if(new Vector2(rigidbody.velocity.x,rigidbody.velocity.z).magnitude<=speed_max)
			rigidbody.AddForce(force);
		
		//DEV. WEIRD.SIHT
		if (graphics.LowerTorso.animation!=null){
			graphics.LowerTorso.animation.Play();
			graphics.UpperTorso.animation.Play();
		
			graphics.LowerTorso.animation.enabled=true;
			graphics.UpperTorso.animation.enabled=true;
		}
	}
		
	void restrictMovement(){
	
		var xz_vec = new Vector2 (rigidbody.velocity.x, rigidbody.velocity.z);
		
		if (xz_vec.magnitude > speed_max){
			xz_vec = Vector2.ClampMagnitude(xz_vec, speed_max);
		}
		
		rigidbody.velocity = new Vector3 (
			xz_vec.x,
			rigidbody.velocity.y,//Mathf.Clamp (rigidbody.velocity.y, -jump_speed, jump_speed_max),
			xz_vec.y
			);
	}

	void OnDestroy ()
	{
		jump_timer.Destroy();
		Data.Player=null;
		NotificationCenter.Instance.removeListener(OnExplosion,NotificationType.Explode);
	}
		
	private void updateRotations()
	{
		//aim
		var forward = transform.TransformDirection(new Vector3(r_axis_x, 0, -r_axis_y));
		
		if (forward.magnitude>0.5f) {
			last_aim_direction = forward.normalized;
		}
		//last_aim_point = transform.position + last_aim_direction;
		
		//movement
		forward=transform.TransformDirection(new Vector3(l_axis_x, 0, -l_axis_y));
		
		if (forward.magnitude>0.5f){
			last_move_direction = forward.normalized;
		}
		//last_move_point=transform.position + last_move_direction;
		
		//mecha rotations
		
		//aimrot
		var newRotation = Quaternion.LookRotation(transform.TransformDirection(last_aim_direction)).eulerAngles;
        newRotation.x = newRotation.z = 0;
       	graphics.UpperTorso.rotation = Quaternion.Slerp(graphics.UpperTorso.rotation,Quaternion.Euler(newRotation),Time.deltaTime*4);
		//moverot
		if(!restrictLegs){
			newRotation = Quaternion.LookRotation(transform.TransformDirection(last_move_direction)).eulerAngles;
	        newRotation.x = newRotation.z = 0;
	        graphics.LowerTorso.rotation = Quaternion.Slerp(graphics.LowerTorso.rotation,Quaternion.Euler(newRotation),Time.deltaTime*4);
			graphics.Fullbody.rotation=graphics.LowerTorso.rotation;
		}
		//shoot direction
		last_upper_direction=graphics.UpperTorso.rotation*Vector3.forward;
	}
	/// <summary>
	/// Ignores the next explosion.
	/// </summary>
	public void IgnoreExplosion ()
	{
		ignoreExplosion=true;
	}
	
	public void Die(){
		hp=0;
		destroyed=true;
		graphics.DisengageParts();
		
		NotificationCenter.Instance.sendNotification(new Explosion_note(transform.position,10000f,20f));
		
		Destroy(gameObject);
	}
	
	public void restrictLegMovement(bool restrict){
		restrictLegs=restrict;
	}
	
	public void freezePlayer(){
		freeze=true;
	}
	
	public void toggleInvulnerability(){
		invulnerable=!invulnerable;
	}
}

/*DEV. mouse 
		var mpos=Input.mousePosition+Vector3.up*4;
		var mouse_pos_dif=mpos-Camera.main.WorldToScreenPoint(transform.position);
		mouse_pos_dif=Vector3.ClampMagnitude(mouse_pos_dif,1);	
		mouse_pos_dif.z=mouse_pos_dif.y;
		mouse_pos_dif.y=0;

		//ability usage
		if (Input.GetMouseButtonDown(0)){
			
			var forward=transform.TransformDirection(mouse_pos_dif);
			var obj=Instantiate(projectile_prefab,transform.position+forward*2,Quaternion.identity) as Transform;
			var pro=obj.GetComponent<ProjectileMain>();
			pro.setDirection(forward,10);
			
			//Debug.Log("f: "+forward);
		}
		*/


/* DEV. keyboard
			if (Input.GetKey(KeyCode.A)){
				rigidbody.AddForce(Vector3.left*acceleration);
			}
			
			if (Input.GetKey(KeyCode.D)){
				rigidbody.AddForce(Vector3.right*acceleration);
			}
			
			if (Input.GetKey(KeyCode.W)){
				rigidbody.AddForce(Vector3.forward*acceleration);
			}
			
			if (Input.GetKey(KeyCode.S)){
				rigidbody.AddForce(Vector3.back*acceleration);
			}
			
			//jump
			if (Input.GetKeyDown(KeyCode.Space)){
				if (canJump){
					rigidbody.AddForce(Vector3.up*jump_speed);
					canJump=false;
				}
			}
			*/
			