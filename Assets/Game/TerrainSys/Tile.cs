using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public static Bounds mesh_bounds;
	
	Timer timer;
	
	Vector3 start_pos,move_target,move_speed;
	bool moving_up;
	float max_move_target_up,max_move_target_down;
	int time_min,time_max;
	
	// Use this for initialization
	void Start () {
		timer=new Timer(0,OnTimer);
		
		time_min=5000;
		time_max=20000;
		randomizeDelay(0,time_max/2);
		
		moving_up=Subs.RandomBool();
		start_pos=transform.position;

		max_move_target_up=2f;
		max_move_target_down=1f;
		
		timer.Active=false;
		if(Subs.RandomPercent()<80)
			timer.Active=true;
	}
	
	void Awake(){
		mesh_bounds=GameObject.Find("graphics").renderer.bounds;
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
	
	void OnTimer(){
		moving_up=!moving_up;
		if (moving_up)
			MoveUp();
		else
			MoveDown();
		
		randomizeDelay();
	}
	
	void randomizeDelay(){
		timer.Delay=Random.Range(time_min,time_max);
		timer.Reset();
	}
	
	void randomizeDelay(int min,int max){
		timer.Delay=Random.Range(min,max);
		timer.Reset();
	}
	
	void OnDestroy(){	
		timer.Destroy();
	}
}
