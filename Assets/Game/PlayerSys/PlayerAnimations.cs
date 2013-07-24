using UnityEngine;
using System.Collections;
using NotificationSys;

public class PlayerAnimations{

	PlayerMain player;
	public bool jump_end=true,jump_start=true, jumped;
	public bool jump_has_peaked=false,canJump,dashing,dash_move;
	public float current_jump_y;
	
	Vector3 dash_vector;
	
	public PlayerAnimations(PlayerMain player){
		this.player=player;
	}
		
		
	public void Update () {
		if (dash_move){
			player.rigidbody.velocity = new Vector3(dash_vector.x,current_jump_y, dash_vector.z);
		}
	}
	
	public void jumpStart(){
		jumped=true;
		canJump = false;
		if (jump_start&&!(current_jump_y>1))
			player.StartCoroutine(JumpStart());
	}
	
	public void jumpEnd(){
		if (jump_end&&jumped&&canJump&&current_jump_y<0){
			player.StartCoroutine(JumpEnd());
		}
	}
	
	public void dashStart(Vector3 dash_vector){
		this.dash_vector=dash_vector;
		if (!dashing&&!jumped&&player.onGround)
			player.StartCoroutine(DashStart());
	}
	
		
	public void dashEnd ()
	{
		player.StartCoroutine(DashEnd());
	}
	
	//coroutines
	
	IEnumerator JumpStart(){
		jump_start=false;
		//player.freezeWeapons(true);
		//graphics.setOverheat(false,true);
		player.graphics.setFullbody(true);
		player.graphics.changeFullAnimation("JumpStart");
		yield return new WaitForSeconds(0.1f);
		current_jump_y=player.stats.Jump_speed;
		
		jump_start=true;
		yield return null;
	}
	
	IEnumerator JumpEnd(){
		jumped=false;
		if (jump_has_peaked){
			player.graphics.changeFullAnimation("JumpEnd");
			jump_end=false;
			player.freezePlayer(true);
			player.freezeRotations(true);
			
			//lil aoe
			NotificationCenter.Instance.sendNotification(new Explosion_note(player.transform.position+player.transform.TransformDirection(Vector3.down),10000f,3f));
			player.graphics.particles.STOMP();
			
			yield return new WaitForSeconds(player.graphics.Fullbody.animation["JumpEnd"].length);
		}
		//player.freezeWeapons(false);
		player.graphics.setFullbody(false);
		jump_end=true;
		jump_has_peaked=false;
		
		player.freezePlayer(false);
			player.freezeRotations(false);
		
		player.UpperIsLower();
	}
	
	IEnumerator DashStart(){
		dashing=true;
		player.freezeMovement(true);
		player.freezeWeapons(true);
		player.graphics.setFullbody(true);
		player.graphics.changeFullAnimation("DashStart");
		yield return new WaitForSeconds(0.44f);
		dash_move=true;
		yield return null;
	}
	
	IEnumerator DashEnd(){
		dash_move=false;
		player.graphics.changeFullAnimation("DashEnd");
		yield return new WaitForSeconds(player.graphics.Fullbody.animation["DashEnd"].length);
		
		dashing=false;
		
		player.graphics.setFullbody(false);
		player.freezeMovement(false);
		player.freezeWeapons(false);
		player.graphics.setFullbody(false);
		
		yield return null;
	}

	public void KNOCKBACKHAX (Vector3 force)
	{
		player.StartCoroutine(start_knockback_hax(force,0.3f));
	}
	
	public void KNOCKBACKHAX (Vector3 pos,float force,float radius,float seconds)
	{
		var d=player.transform.position-pos;
		var f=(d).normalized*(force);
		float ss=1;
		if (radius>0)
			ss=Mathf.Max(0, (1-d.magnitude/radius));
		//f*=ss;
		//var e=0;
		//seconds*=ss;
		if (ss>0)
			player.StartCoroutine(start_knockback_hax(f,seconds));
	}
	
	IEnumerator start_knockback_hax(Vector3 force ,float seconds){
		if (dashing) yield return null;
		player.transform.rigidbody.velocity=force;
		
		player.freezeMovement(true);
		yield return new WaitForSeconds(seconds);
		player.freezeMovement(false);
	}
}
