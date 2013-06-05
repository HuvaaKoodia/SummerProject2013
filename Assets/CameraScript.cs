using UnityEngine;
using System.Collections;
using NotificationSys;

public class CameraScript : MonoBehaviour {
	
	private Vector3 look_at_pos,move_to_pos;
	
	Vector3 move_dir,move_dir_r,look_pos,look_pos_r;
	float dis,speed;
	
	// Use this for initialization
	void Start () {
		NotificationCenter.Instance.addListener(OnZoomNote,NotificationType.CameraZoom);
		move_to_pos=transform.position;
	}
	
	// Update is called once per frame
	
	void Update (){
		
		
		move_dir=move_to_pos-transform.position;
		move_dir.Normalize();
		
		look_pos=look_at_pos-transform.position;
		look_pos.Normalize();
		
		move_dir_r=move_dir;
		//move_dir_r=Vector3.Lerp(move_dir_r,move_dir,Time.deltaTime);
		look_pos_r=Vector3.Lerp(look_pos_r,look_pos,Time.deltaTime);
		
		dis=Vector3.Distance(transform.position,move_to_pos);
		speed=Time.deltaTime;
		if (dis>0.05f){
			transform.position+=move_dir_r*speed;
		}
		
		transform.LookAt(transform.position+look_pos_r);
	}
	
	public void LookAt(Vector3 pos){
		look_at_pos=pos;
	}
		
	public void OnZoomNote(Notification note){
		var n=(CameraZoom_note)note;
		LookAt(look_at_pos+Vector3.up*0.3f);
		move_to_pos+=transform.rotation*Vector3.forward*Vector3.Distance(look_pos,transform.position)*n.Amount;
		
	} 
}
