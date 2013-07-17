using UnityEngine;
using System.Collections;

public class PlayerGraphicsScr : MonoBehaviour {
	
	public Transform Mecha,LowerTorso,UpperTorso,LowerPelvis,ExplosionDummy,Fullbody,left_cannon,right_cannon;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (LowerPelvis!=null){
			var pos=LowerPelvis.position;
			pos.y-=0.5f;
			UpperTorso.position=pos;
		}
	}
	
	//DEV.TEMP
	public void setColor(Color color){
		foreach (Renderer t in Mecha.GetComponentsInChildren(typeof(Renderer))){
			t.material.color=color;
		}
		
		ExplosionDummy.gameObject.SetActive(true);
		foreach (Renderer t in ExplosionDummy.GetComponentsInChildren(typeof(Renderer))){
			t.material.color=color;
		}
		ExplosionDummy.gameObject.SetActive(false);
		
		Fullbody.gameObject.SetActive(true);
		foreach (Renderer t in Fullbody.GetComponentsInChildren(typeof(Renderer))){
			t.material.color=color;
		}
		Fullbody.gameObject.SetActive(false);
	}
	
	public void DisengageParts(){
		ExplosionDummy.gameObject.SetActive(true);
		//set rotations correctly
		if (isFullbody()){
			ExplosionDummy.FindChild("LowerTorso").rotation=Fullbody.rotation;
			ExplosionDummy.FindChild("UpperTorso").rotation=Fullbody.rotation;
		}
		else{
			ExplosionDummy.FindChild("LowerTorso").rotation=LowerTorso.rotation;
			ExplosionDummy.FindChild("UpperTorso").rotation=UpperTorso.rotation;
		}
		foreach (Rigidbody r in ExplosionDummy.GetComponentsInChildren(typeof(Rigidbody))){
			r.transform.parent=null;
		}
	}
	
	bool animation_walk,current_shoot_arm_left=true;	

	public void AnimationWalk(){
		if (LowerTorso.animation!=null){
			UpperTorso.animation.Blend("Walkcycle");
			LowerTorso.animation.Play();
			
			LowerTorso.animation.enabled=true;
			UpperTorso.animation.enabled=true;
			animation_walk=true;
		}
	}
	public void AnimationShoot(){
		if (AnimationShoot(current_shoot_arm_left)){
			current_shoot_arm_left=!current_shoot_arm_left;
		}
	}
	
	public bool AnimationShoot(bool left_arm){
		if (UpperTorso.animation!=null){
			if (left_arm){
				UpperTorso.animation.Stop("Left_shoot");
				UpperTorso.animation.Blend("Left_shoot");
			}
			else{
				UpperTorso.animation.Stop("Right_shoot");
				UpperTorso.animation.Blend("Right_shoot");
			}

			UpperTorso.animation.enabled=true;
			return true;
		}
		return false;
	}
	
	public void setFullbody(bool on){
		Mecha.gameObject.SetActive(!on);
		Fullbody.gameObject.SetActive(on);
	}
	
	public void toggleFullbody(){
		setFullbody(!Fullbody.gameObject.activeSelf);
	}
	
	public void changeFullAnimation(string animation){
		Fullbody.animation.Play(animation);
	}

	public bool isFullbody ()
	{
		return Fullbody.gameObject.activeSelf;
	}

	public bool UpdateEnd()
	{
		bool on=animation_walk;
		animation_walk=false;
		if (!on){
			LowerTorso.animation.enabled=false;
			UpperTorso.animation["Walkcycle"].enabled=false;
			return true;
		}
		return false;
		
	}
	
	
	public Vector3 getShootPosition(){
		return getShootPosition(current_shoot_arm_left);
	}
	
	public Vector3 getShootPosition(bool left_arm){
		if (left_arm)
			return left_cannon.transform.position;
		return right_cannon.transform.position;
	}
}
