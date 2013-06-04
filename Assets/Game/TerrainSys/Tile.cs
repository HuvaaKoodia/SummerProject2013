using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	public static Bounds mesh_bounds;
		
	void Awake(){
		mesh_bounds=GameObject.Find("graphics").renderer.bounds;
	}
	
	
	TileData tile_group;
	
	/*
	Timer timer;
	
	
	Vector3 start_pos,move_target,move_speed;
	bool moving_up;
	float max_move_target_up,max_move_target_down;
	int time_min,time_max;
	*/
	
	// Use this for initialization
	void Start () {
		/*
		timer=new Timer(0,OnTimer);
		
		time_min=20000;
		time_max=200000;
		randomizeDelay(0,time_max/2);
		
		moving_up=Subs.RandomBool();
		start_pos=transform.position;

		max_move_target_up=2f;
		max_move_target_down=1f;
		
		timer.Active=false;
		if(Subs.RandomPercent()<80)
			timer.Active=true;
			*/
	}

	// Update is called once per frame
	void Update () {
		/*
		if (moving_up&&(transform.position.y<move_target.y))
			transform.position+=move_speed*Time.deltaTime;
		if (!moving_up&&(transform.position.y>move_target.y))
			transform.position+=move_speed*Time.deltaTime;
			*/
		if (tile_group!=null){
			transform.position=new Vector3(transform.position.x,tile_group.Position.y,transform.position.z);
		}
	}
	
	//subs
	
	public void setTileGroup(TileData data){
		tile_group=data;
		//timer.Active=false;
	}
	/*
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
	*/

	Vector3 coor;
	
	public void setCoordinate(Vector3 coordinate){
		coor=coordinate;
	}
	
	public Vector3 getCoordinate(){
		return coor;
	}
	
	/*
	void OnGUI(){
		if (coor.y==0){
			var pos=Camera.main.WorldToScreenPoint(transform.position);
			GUI.Box(new Rect(pos.x,pos.y,150,20),"Coor: "+coor);
		}
	}
	*/
}


public class TileData{
	
	public int tile_group;
	public Vector3 Position{get;private set;}

	Timer timer;
	
	Vector3 start_pos,move_target,move_speed;
	
	bool moving_up;
	float max_move_target_up,max_move_target_down;
	int time_min,time_max;
	
	// Use this for initialization
	public TileData(Vector3 startpos, int tileGroup,bool random){
		tile_group=tileGroup;
		
		max_move_target_up=2f;
		max_move_target_down=200f;
		
		Position=start_pos=startpos;
		
		if (random){
			timer=new Timer(0,OnTimer);
			
			time_min=20;
			time_max=2000;
			randomizeDelay(0,time_max/2);
			
			moving_up=Subs.RandomBool();
			

			timer.Active=true;
		}
	}

	// Update is called once per frame
	public void Update () {
		if (moving_up&&(Position.y<move_target.y))
			Position+=move_speed*Time.deltaTime;
		if (!moving_up&&(Position.y>move_target.y))
			Position+=move_speed*Time.deltaTime;
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
	
	public void setTileGroup(int tg){
		tile_group=tg;
	}
	
	void Destroy(){	
		timer.Destroy();
	}
}
