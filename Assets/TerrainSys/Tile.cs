using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	Timer timer;
	
	Vector3 start_pos,move_target,move_speed;
	bool moving_up;
	float max_move_target_up,max_move_target_down;
	
	// Use this for initialization
	void Start () {
		timer=new Timer(0,OnTimer);
		randomizeDelay();
		
		moving_up=Subs.RandomBool();
		start_pos=transform.position;
		
		move_speed=Vector3.up;
		
		max_move_target_up=0.5f;
		max_move_target_down=0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (moving_up&&(transform.position.y<move_target.y))
			transform.position+=move_speed*Time.deltaTime;
		if (!moving_up&&(transform.position.y>move_target.y))
			transform.position+=move_speed*Time.deltaTime;
	}
	
	//subs
	public void MoveUp(){
		move_target=start_pos+Vector3.up*max_move_target_up;
		move_speed=Vector3.up;
	}
	
	public void MoveDown(){
		move_target=start_pos+Vector3.down*max_move_target_down;
		move_speed=Vector3.down;
	}
	
	
	
	//
	void OnTimer(){
		moving_up=!moving_up;
		if (moving_up)
			MoveUp();
		else
			MoveDown();
		
		randomizeDelay();
	}
	
	void randomizeDelay(){
		timer.Delay=Random.Range(2500,5000);
		timer.Reset();
	}
	
	void OnDestroy(){	
		timer.Destroy();
	}
}
