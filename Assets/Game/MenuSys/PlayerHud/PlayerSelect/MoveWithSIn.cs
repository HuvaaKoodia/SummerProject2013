using UnityEngine;
using System.Collections;

public class MoveWithSIn : MonoBehaviour {
	public bool isRightSide;
	UIAnchor anchor;
	float maxMoveAmount,base_x_pos;
	float timer;
	bool set_start_pos=true;
	// Use this for initialization
	void Start () {
		maxMoveAmount=.05f;
		
		timer=0;
		
	}
	
	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;
		//anchor = GetComponent<UIAnchor>();
		if (set_start_pos){
			base_x_pos=transform.position.x;
			set_start_pos=false;
		}
		var cos=maxMoveAmount*(1f+Mathf.Cos(timer*3.5f));
		if(isRightSide){
			transform.position=new Vector3(base_x_pos+cos,transform.position.y,transform.position.z);
		}
		else{
			transform.position=new Vector3(base_x_pos-cos,transform.position.y,transform.position.z);
		}
	}
}
