using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterMain : MonoBehaviour {
	
	public Transform projectile_prefab;
	
	bool onGround,canJump;
	float acceleration=50,
		jump_speed=200,jump_speed_max=2000,
		speed_max=2;
	
	float l_axis_x,l_axis_y,r_axis_x,r_axis_y;
	
	Transform graphics,aim_dir;
	
	Timer jump_timer;
	
	public int controllerNumber=1;
	public Color color;
	
	Vector3 last_used_aim_direction;
	
	public List<Transform> Abilities=new List<Transform>();
	List<AbilityContainer> ability_containers;
	
	// Use this for initialization
	void Start () {
		graphics=transform.Find("Graphics").Find ("temp") as Transform;
		
		jump_timer=new Timer(400,OnJumpTimer);
		
		aim_dir=transform.Find("aim_direction") as Transform;
		
		ability_containers=new List<AbilityContainer>();
		
		for (int i=0;i<Abilities.Count;i++){
			var abb=new AbilityContainer();	
			abb.projectile_prefab=projectile_prefab;
			abb.ability_prefab=Abilities[i];
			ability_containers.Add(abb);
		}
	}

	void Update(){
		l_axis_x=Input.GetAxis("L_XAxis_"+controllerNumber);
		l_axis_y=Input.GetAxis("L_YAxis_"+controllerNumber);
		
		r_axis_x=Input.GetAxis("R_XAxis_"+controllerNumber);
		r_axis_y=Input.GetAxis("R_YAxis_"+controllerNumber);
		
		updateAimDir();
		
		if (ability_containers.Count>0&&Input.GetButton("RB_"+controllerNumber)){
			ability_containers[0].UseAbility(transform.position,last_used_aim_direction);
		}
		
		if (ability_containers.Count>1&&Input.GetButton("LB_"+controllerNumber)){
			ability_containers[1].UseAbility(transform.position,last_used_aim_direction);
		}
		
		if (ability_containers.Count>2&&Input.GetAxis("Triggers_"+controllerNumber)<0){
			ability_containers[2].UseAbility(transform.position,last_used_aim_direction);
		}
		
		if (ability_containers.Count>3&&Input.GetAxis("Triggers_"+controllerNumber)>0){
			ability_containers[3].UseAbility(transform.position,last_used_aim_direction);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		rigidbody.WakeUp();

		if (onGround){
			if (l_axis_x<0){
				rigidbody.AddForce(Vector3.left*acceleration);
			}
			
			if (l_axis_x>0){
				rigidbody.AddForce(Vector3.right*acceleration);
			}
			
			if (l_axis_y<0){
				rigidbody.AddForce(Vector3.forward*acceleration);
			}
			
			if (l_axis_y>0){
				rigidbody.AddForce(Vector3.back*acceleration);
			}
			
			//jump
			if (Input.GetButton("A_"+controllerNumber)||Input.GetButton("RS_"+controllerNumber)||Input.GetButton("LS_"+controllerNumber)){
				if (canJump){
					rigidbody.velocity=new Vector3(rigidbody.velocity.x,10,rigidbody.velocity.z);
					canJump=false;
				}
			}
		}
		//restrict movement speed
		var xz_vec=new Vector2(rigidbody.velocity.x,rigidbody.velocity.z);
		
		if (xz_vec.magnitude>speed_max){
			xz_vec=Vector2.ClampMagnitude(xz_vec,speed_max);
		}
		
		rigidbody.velocity=new Vector3(
			xz_vec.x,
			Mathf.Clamp(rigidbody.velocity.y,-jump_speed,jump_speed_max),
			xz_vec.y
			);
		
		graphics.renderer.material.color=color;
		//if (onGround)
		//	graphics.renderer.material.color=Color.green;
		jump_timer.Active=true;
	}
	
	void OnCollisionStay(Collision other){
		foreach (var c in other.contacts){
			if (Vector3.Angle(c.normal,transform.up)<10){
				onGround=true;
				break;
			}
		}
	}
	
	void OnJumpTimer(){
		onGround=false;
		canJump=true;
	}
	
	void OnDestroy(){
		jump_timer.Destroy();
	}
	
		
	private void updateAimDir(){
		var forward=transform.TransformDirection(new Vector3(r_axis_x, 0, -r_axis_y));

		forward.Normalize();
		
		if (forward==Vector3.zero){
			if (last_used_aim_direction==Vector3.zero)
				last_used_aim_direction=Vector3.up;
		}
		else{
			last_used_aim_direction=forward;
		}
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
			