using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NotificationSys;

public class PlayerMain : MonoBehaviour
{
	public PlayerData Data;
	public PlayerGraphicsScr graphics;
	public PlayerSoundMain sounds;
	public List<AbilityContainer> ability_containers;
	public int controllerNumber = 0;
	//stats
	public PlayerStats stats;
	
	float hp;
	public float HP {
		get{ return hp;}
		set{
			if (invulnerable||public_invulnerability>0) return;
			hp = Mathf.Clamp(value,0,stats.HP);
			if(hp==0) Die();
		}
	}
	
	float mp;
	public float MP{
		get{ return mp;}
		set{ 
			if(value>mp)
				value = mp + ((value-mp)*(1f - (stats.MP_heatgen_reduction * mp / stats.MP)));
			
			if (value>mp){
				MPregenReset();
			}
			if (value>=stats.MP){
				MPoverheatReset();
			}
			mp = Mathf.Clamp(value,0,stats.MP);
		}
	}
	
	public void MPregenReset(){
		MP_regen_multi=stats.MP_regen_multi;
		mp_regen_on=false;
		mp_regen_timer.Active=true;
		mp_regen_timer.Delay=stats.MP_regen_delay;
		mp_regen_timer.Reset();
	}
	
	public void MPoverheatReset(){
		MP_regen_multi=stats.MP_regen_multi;
		mp_regen_on=false;
		mp_regen_timer.Active=true;
		mp_regen_timer.Delay=stats.MP_overheat_delay;
		mp_regen_timer.Reset();
		
		OVERHEAT=true;
	}
	
	public Color _Color{
		set{
			 graphics.setColor(value);
		}
	}
	
	public Vector3 AimDir{get{return last_aim_direction;}}
	public Vector3 UpperTorsoDir{get{return last_upper_direction;}}
	public Vector3 LowerTorsoDir{get{return last_move_direction;}}
	
	public bool OVERHEAT{get;private set;}
	
	//private 
	bool ignoreExplosion=false,on_legit_ground=false;
	bool freeze_movement = false,invulnerable=false;
	bool jump_end=true,jump_start=true,jump_has_peaked=false;
	bool freeze_lower=false,freeze_upper=false,freeze_weapons=false;
	bool onGround, canJump, jumped,destroyed=false,freeze=false,mp_regen_on=false;
	
	float MP_regen_multi=5f,Lower_torso_rotation_deadzone=0.3f;
	float l_axis_x, l_axis_y, r_axis_x, r_axis_y;
	float current_jump_y;
	
	float regen_multi=-1;
	
	int public_invulnerability=0;
	
	Timer legit_timer,onGround_timer,mp_regen_timer;
	Vector3 last_aim_direction,last_move_direction,last_upper_direction;

	void Start () {
		//last_aim_direction=last_move_direction=Vector3.forward;
		
		legit_timer = new Timer(888, OnLegit);
		onGround_timer= new Timer(200, OnGroundTimer);
		mp_regen_timer= new Timer(stats.MP_regen_delay, OnMPregenTimer);
		
		
		hp=stats.HP;
		mp=stats.MP;
		if (regen_multi==-1)
			mp=0;
			
		//Data set
		ability_containers = new List<AbilityContainer>();
		
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
		legit_timer.Update();
		onGround_timer.Update();
		mp_regen_timer.Update();
		
		l_axis_x = Input.GetAxis ("L_XAxis_" + controllerNumber);
		l_axis_y = Input.GetAxis ("L_YAxis_" + controllerNumber);
		
		r_axis_x = Input.GetAxis ("R_XAxis_" + controllerNumber);
		r_axis_y = Input.GetAxis ("R_YAxis_" + controllerNumber);
		
		updateRotations();
		if (!OVERHEAT&&!freeze&&!freeze_weapons){
			if (ability_containers.Count > 0 && Input.GetButton ("RB_" + controllerNumber)||Input.GetKey(KeyCode.L)){//DEV.KEY
				useAbility(2,false);
			}
			
			if (ability_containers.Count > 1 && Input.GetButton ("LB_" + controllerNumber)){
				useAbility(1,true);
			}
			
			if (ability_containers.Count > 2 && Input.GetAxis ("Triggers_" + controllerNumber) < 0) {
				useAbility(3,false);
			}
			
			if (ability_containers.Count > 3 && Input.GetAxis ("Triggers_" + controllerNumber) > 0) {
				useAbility(0,true);
			}
		}
		//mp regen
		if (mp_regen_on){
			MP+=regen_multi*Time.deltaTime*MP_regen_multi;
			MP_regen_multi+=Time.deltaTime*stats.MP_regen_add;
		}
		
		if (OVERHEAT){
			if (MP<stats.MP*(stats.MP_overheat_threshold_percent*0.01f)){
				OVERHEAT=false;
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		rigidbody.WakeUp ();
		
		if (!freeze&&!freeze_movement){
			bool move=false;
			if (l_axis_x < -Lower_torso_rotation_deadzone){
				move=true;
			}
			if (l_axis_x > Lower_torso_rotation_deadzone){
				move=true;
			}
			if (l_axis_y < -Lower_torso_rotation_deadzone){
				move=true;
			}
			if (l_axis_y > Lower_torso_rotation_deadzone){
				move=true;
			}
			if (move){
				MoveAround(Vector3.forward*stats.Acceleration);
			}
			//jump
			if (Input.GetButton ("A_" + controllerNumber) || Input.GetButton ("LS_" + controllerNumber)) {
				if (onGround&&canJump){
					jumped=true;
					canJump = false;
					//Debug.Log ("Jump force++");
					jumpStart();
					sounds.StopWalk();
				}
			}
		}

		if (jumped||!onGround){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,current_jump_y, rigidbody.velocity.z);
			current_jump_y+=Physics.gravity.y*Time.deltaTime;
			//Debug.Log ("Jump y "+current_jump_y+", grav: "+Physics.gravity.y);
		}
		else{ 
			current_jump_y=rigidbody.velocity.y;
			//Debug.Log ("Jump y reset");
		}
		
		//if (rigidbody.velocity.y<0)
			//onGround=false;
		
		//DEV.input <
		/*
		if (Input.GetButtonDown("Y_" + controllerNumber)){
			NotificationCenter.Instance.sendNotification(new Explosion_note(transform.position,10000f,20f));
		}*/
		
		if (graphics.UpdateEnd()){
			sounds.StopWalk();
		}
		
		//DEV.debug
		//if (controllerNumber==1)
		//	Debug.Log("Legit?: "+on_legit_ground);
	}
	
	
	//DEV. bugs out a bit
	void MoveAround(Vector3 force){	
		//if (jumped||!onGround)
			//restrictMovement();
		
		rigidbody.AddRelativeForce(graphics.LowerTorso.rotation*force);
		
		
		//restrict movement
		var stick_mag=new Vector2(l_axis_x,l_axis_y).magnitude;
		stick_mag=Mathf.Clamp01(stick_mag);
		var mm=new Vector2(rigidbody.velocity.x,rigidbody.velocity.z);
		//if(mm.magnitude>stick_mag*stats.Move_speed){
			mm=mm.normalized*stick_mag*stats.Move_speed;
			rigidbody.velocity=new Vector3(mm.x,rigidbody.velocity.y,mm.y);
		//}
		
		//DEV. WEIRD.SIHT
		if (onGround){
			graphics.AnimationWalk();
			sounds.PlayWalk();
		}
	}
	void restrictMovement(){
		var xz_vec = new Vector2 (rigidbody.velocity.x, rigidbody.velocity.z);
		
		if (xz_vec.magnitude > stats.Move_speed){
			xz_vec = Vector2.ClampMagnitude(xz_vec, stats.Move_speed);
		}
		
		rigidbody.velocity = new Vector3(
			xz_vec.x,
			rigidbody.velocity.y,//Mathf.Clamp (rigidbody.velocity.y, -jump_speed, jump_speed_max),
			xz_vec.y
		);
	}

	void OnCollisionStay(Collision other)
	{	

		int angle=10;
		if (other.gameObject.tag=="Gib")
			angle=30;
		
		//bool not_on_ground_at_all=true;
		foreach (var c in other.contacts) {
			if (Vector3.Angle (c.normal, transform.up) < angle){
				
				onGround_timer.Reset();
				onGround_timer.Active=true;
				onGround = true;
				
				if (other.gameObject.tag=="Hurt"){
					//not_on_ground_at_all=false;
					on_legit_ground=false;
				} else
				if (other.gameObject.tag=="Ground"){
					//not_on_ground_at_all=false;
					if (!on_legit_ground&&!legit_timer.Active){
						legit_timer.Reset(true);
					}
				}
				
				
				jumpEnd();

				jump_has_peaked=false;
				
				if (jumped){
					//Debug.Log ("Jump hit 2");
				}
				
				break;
			}
		}
		//if (not_on_ground_at_all)
		//	on_legit_ground=false;
	}

	void jumpStart(){
		if (jump_start&&!(current_jump_y>1))
			StartCoroutine(JumpStart());
	}
	
	void jumpEnd(){
		//bool started=false;
		if (jumped&&canJump&&jump_end&&current_jump_y<0){
			StartCoroutine(JumpEnd());
			//started=true;
		}
		/*DEV.debug
		if (jump_has_peaked){
			if (started)
				Debug.Log("SUCCESS:::");
			else
				Debug.Log("FAIL");
		 Debug.Log("jumped "+jumped+" canjump "+canJump+" jump end "+jump_end);
		}*/
	}
	
	IEnumerator JumpEnd(){
		jumped=false;
		if (jump_has_peaked){
			graphics.changeFullAnimation("JumpEnd");
			jump_end=false;
			freeze=freeze_lower=freeze_upper=true;
			
			//lil aoe
			NotificationCenter.Instance.sendNotification(new Explosion_note(transform.position+transform.TransformDirection(Vector3.down),10000f,3f));
			
			yield return new WaitForSeconds(graphics.Fullbody.animation["JumpEnd"].length);
		}
		freeze_weapons=false;
		graphics.setFullbody(false);
		jump_end=true;
		jump_has_peaked=false;
		
		freeze=freeze_lower=freeze_upper=false;
		
		UpperIsLower();
	}
	
	IEnumerator JumpStart(){
		jump_start=false;
		freeze_weapons=true;
		graphics.setFullbody(true);
		graphics.changeFullAnimation("JumpStart");
		yield return new WaitForSeconds(0.1f);
		current_jump_y=stats.Jump_speed;
		
		jump_start=true;
		yield return null;
	}
	
	void OnLegit()
	{
		on_legit_ground=true;
		legit_timer.Active=false;
	}

	void OnGroundTimer ()
	{	
		onGround=false;
		on_legit_ground=false;
		
		canJump=true;
		jump_has_peaked=true;
		
		legit_timer.Active=false;
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
	
	void OnDestroy ()
	{
		Data.Player=null;
		NotificationCenter.Instance.removeListener(OnExplosion,NotificationType.Explode);
	}

	private void UpperIsLower(){
		graphics.UpperTorso.rotation=graphics.LowerTorso.rotation;
		
		//var rot=graphics.UpperTorso.rotation*Vector3.forward;
		//rot = new Vector3(rot.x,rot.z,0);
		//last_aim_direction= rot;
	}
	
	private void updateRotations()
	{
		//aim
		var forward = transform.TransformDirection(new Vector3(r_axis_x, 0, -r_axis_y));
		
		if (forward.magnitude>0.1f)
		{
			last_aim_direction = forward.normalized;
		}
		//last_aim_point = transform.position + last_aim_direction;
		
		//movement
		forward=transform.TransformDirection(new Vector3(l_axis_x, 0, -l_axis_y));
		
		if (forward.magnitude>Lower_torso_rotation_deadzone)
		{
			last_move_direction = forward.normalized;//Vector3.Slerp(last_move_direction,forward.normalized,Time.deltaTime*stats.Lower_torso_rotation_multi);
		}
		//last_move_point=transform.position + last_move_direction;
		
		//mecha rotations
		
		//aimrot
		
		if (!freeze_upper){
			var newRotation = Quaternion.LookRotation(transform.TransformDirection(last_aim_direction)).eulerAngles;
        	newRotation.x = newRotation.z = 0;
       		graphics.UpperTorso.rotation =Quaternion.Slerp(graphics.UpperTorso.rotation,Quaternion.Euler(newRotation),Time.deltaTime*stats.Upper_torso_rotation_multi);
			
			//shoot direction
			last_upper_direction=graphics.UpperTorso.rotation*Vector3.forward;
		}
		//moverot
		if(!freeze_lower&&!freeze_movement){
			var newRotation = Quaternion.LookRotation(transform.TransformDirection(last_move_direction)).eulerAngles;
	        newRotation.x = newRotation.z = 0;
	       // graphics.LowerTorso.rotation = Quaternion.Slerp(graphics.LowerTorso.rotation,Quaternion.Euler(newRotation),Time.deltaTime*lower_torso_rotation_multi);
		 	graphics.LowerTorso.rotation=Quaternion.Slerp(graphics.LowerTorso.rotation,Quaternion.Euler(newRotation),Time.deltaTime*stats.Lower_torso_rotation_multi);//Quaternion.Euler(newRotation);
			graphics.Fullbody.rotation=graphics.LowerTorso.rotation;
		}
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
	
	public void freezeMovement(bool freeze){
		freeze_movement=freeze;
	}
	
	public void freezePlayer(){
		freeze=true;
	}
	
	public void toggleInvulnerability(){
		invulnerable=!invulnerable;
	}
	
	public void setInvulnerable(bool on){
		if (on){
			public_invulnerability++;
			return;
		}
		public_invulnerability--;
		
		if (public_invulnerability<0)
			public_invulnerability=0;
	}
		
	void useAbility(int index,bool left_arm){
		
		var ps=ability_containers [index].Ability.Ability.GetComponent<ProjectileStats>();
		//set pos
		var pos=graphics.getShootPosition(left_arm);
		if (ps.DoubleBarreledFunTime)
			pos=graphics.getShootPosition();
		var dir=last_upper_direction;
		//direction=pos-(direction*5);
		if (ability_containers [index].UseAbility(pos,dir)){
			if (ps.DoubleBarreledFunTime)
				graphics.AnimationShoot();
			else
				graphics.AnimationShoot(left_arm);
		}
	}
	
	public bool onLegitGround(){
		return on_legit_ground;
	}
	
	public void setStartRotation (Quaternion rotation)
	{
		graphics.LowerTorso.rotation=rotation;
		last_move_direction=rotation*Vector3.forward;
		
		graphics.UpperTorso.rotation=rotation;
		last_aim_direction=rotation*Vector3.forward;
	}
}
#region temp
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
#endregion
