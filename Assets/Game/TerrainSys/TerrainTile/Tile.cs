using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	
	Vector3 coor;
	public TileData Tile_Group{get;private set;}
	public TileData Tile_Data{get;private set;}

	// Use this for initialization
	void Awake () {
		Tile_Data=new TileData(Vector3.zero,0);
		Tile_Data.setMovementBounds(1,0);
		Tile_Data.setTimeBounds(10000,80000,true);
	}

	// Update is called once per frame
	void Update (){
		Tile_Data.Update();
		
		var pos_y=0f;
		if (Tile_Group!=null){
			pos_y+=Tile_Group.Position.y;
		}
		if (Tile_Data!=null){
			pos_y+=Tile_Data.Position.y;
		}
		
		transform.position=new Vector3(transform.position.x,pos_y,transform.position.z);
	}
	
	//subs
	
	public void setTileGroup(TileData data){
		Tile_Group=data;
	}

	public void setCoordinate(Vector3 coordinate){
		coor=coordinate;
	}
	
	public Vector3 getCoordinate(){
		return coor;
	}
	
	void OnDestroy(){
		if (Tile_Data!=null)
			Tile_Data.Destroy();
	}
	
	public void Activate(bool active){
		Tile_Data.Activate(active);
	}
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
	public TileData(Vector3 startpos, int tileGroup){
		tile_group=tileGroup;
		
		setMovementBounds(1,1);
		
		Position=start_pos=move_target=startpos;
		
		timer=new Timer(0,OnTimerRandom);
		timer.Active=false;
		
		moving_up=Subs.RandomBool();
	}

	// Update is called once per frame
	public void Update () {
		timer.Update();
		
		if (moving_up&&(Position.y<move_target.y)){
			Position+=move_speed*Time.deltaTime;
			if (Position.y>move_target.y){
				Position=new Vector3(Position.x,move_target.y,Position.z);
			}
		}
		if (!moving_up&&(Position.y>move_target.y)){
			Position+=move_speed*Time.deltaTime;
			if (Position.y<move_target.y){
				Position=new Vector3(Position.x,move_target.y,Position.z);
			}
		}
	}
	
	//subs
	public void setMovementBounds(float up,float down){
		max_move_target_up=up;
		max_move_target_down=down;
	}
	public void setTimeBounds(int min,int max,bool random_start){
		time_min=min;
		time_max=max;
		if (random_start){
			randomizeDelay(0,time_max-time_min);
		}
		else{
			timer.Delay=time_max;
			timer.Reset();
		}
		timer.Active=true;
	}
	
	public void MoveUp(){
		MoveUp(max_move_target_up);
	}
	
	public void MoveDown(){
		MoveDown(max_move_target_down);
	}
	
	public void MoveUp(float amount){
		move_target=start_pos+Vector3.up*amount;
		move_speed=Vector3.up;
		moving_up=true;
	}
	
	public void MoveDown(float amount){
		move_target=start_pos+Vector3.down*amount;
		move_speed=Vector3.down;
		moving_up=false;
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
	
	public void Activate(bool on){
		timer.Active=on;
	}
	
	public void Destroy(){	
		timer.Destroy();
	}
	//event primers
	
	public void SetOnTimerRandomDown(){
		timer.Active=true;
		randomizeDelay(0,2000);
		timer.Timer_Event=OnTimerRandomDown;
	}
	
	//events
	public void OnTimerRandom(){
		if (moving_up)
			MoveDown();
		else
			MoveUp();
		
		randomizeDelay();
	}
	
	public void OnTimerRandomDown(){
		MoveDown(200f);
		timer.Active=false;
	}
}
