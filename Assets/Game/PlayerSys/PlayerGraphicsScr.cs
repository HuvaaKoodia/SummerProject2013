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
	
	bool animation_walk,animation_shoot=false,current_shoot_arm_left=true;	

	public void AnimationWalk(){
		if (LowerTorso.animation!=null){
			UpperTorso.animation.Blend("walk");
			LowerTorso.animation.Play();
			
			LowerTorso.animation.enabled=true;
			UpperTorso.animation.enabled=true;
			animation_walk=true;
		}
	}
	public void AnimationShoot(){
		//if (animation_shoot) return;
		if (UpperTorso.animation!=null){
			if (current_shoot_arm_left){
				UpperTorso.animation.Stop("Left_shoot");
				UpperTorso.animation.Blend("Left_shoot");
			}
			else{
				UpperTorso.animation.Stop("Right_shoot");
				UpperTorso.animation.Blend("Right_shoot");
			}
			current_shoot_arm_left=!current_shoot_arm_left;

			UpperTorso.animation.enabled=true;
			//animation_shoot=true;
		}
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
			UpperTorso.animation["walk"].enabled=false;
			return true;
		}
		return false;
		
	}
	
	
	public Vector3 getShootPosition(){
		if (current_shoot_arm_left)
			return left_cannon.transform.position;
		return right_cannon.transform.position;
	}
}
