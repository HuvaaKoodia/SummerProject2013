using UnityEngine;
using System.Collections;
using NotificationSys;

public class CameraScript : MonoBehaviour {
	
	public float initial_y_offset;
	
	private Vector3 look_at_pos,move_to_pos;
	
	Vector3 move_dir,move_dir_r,look_pos,look_pos_r;
	float dis,speed;
	
	// Use this for initialization
	void Start () {
		NotificationCenter.Instance.addListener(OnZoomNote,NotificationType.CameraZoom);
		move_to_pos=transform.position;
		
		//look_at_pos=Vector3.up*initial_y_offset;
	}
	
	// Update is called once per frame
	
	void Update (){
		
		
		move_dir=move_to_pos-transform.position;
		move_dir.Normalize();
		
		look_pos=look_at_pos-transform.position;
		look_pos.Normalize();
		
		move_dir_r=move_dir;
		//move_dir_r=Vector3.Lerp(move_dir_r,move_dir,Time.deltaTime);
		if (Vector3.Angle(look_pos,look_pos_r)>0.01f)
			look_pos_r=Vector3.Lerp(look_pos_r,look_pos,Time.deltaTime);
		
		dis=Vector3.Distance(transform.position,move_to_pos);
		speed=Time.deltaTime;
		if (dis>0.05f){
			transform.position+=move_dir_r*speed;
		}
		
		transform.LookAt(transform.position+look_pos_r);
	}
	
	public void LookAtCenter(Vector3 center){
		look_at_pos=center+Vector3.up*initial_y_offset;
		transform.LookAt(look_at_pos);
	}
	
	void LookAt(Vector3 pos){
		look_at_pos+=pos;
	}
		
	public void OnZoomNote(Notification note){
		var n=(CameraZoom_note)note;
		if (n.Target==null){
			LookAt(Vector3.up*0.3f);
			move_to_pos+=transform.rotation*Vector3.forward*Vector3.Distance(look_pos,transform.position)*n.Amount;
		}else{
			Ray ray=new Ray(n.Target.position,transform.position-n.Target.position);
			move_to_pos=ray.GetPoint(n.Amount);
			look_at_pos=n.Target.position;
		}
	}
	
	public void DetachPlane(){
		//DEV. detach plane
		var plane=transform.Find("Plane");
		if (plane==null) return;
		plane.transform.parent=null;
	}
}
