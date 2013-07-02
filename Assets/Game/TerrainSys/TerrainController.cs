using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NotificationSys;

public class TerrainController : MonoBehaviour {
	
	public int LevelWidth,LevelHeight;
	
	public int 
		terrain_deterioration_timer=12225,
		tile_random_timer_min=2000,tile_random_timer_max=6000;
	
	//public PlayerMain[] Players;
	
	public Transform player_prefab;
	public Transform ground_prefab;
	public Transform tile_mesh;
	public Bounds mesh_bounds;
	
	private Tile[,] terrain;
	private List<Tile> tiles=new List<Tile>();
	
	private List<TileData> tile_groups=new List<TileData>();
	
	CameraScript main_camera;
	
#pragma warning disable
	Timer terrain_timer;
	
	Vector3 center_point;
	
	// Use this for initialization
	void Awake (){
		//var spawn_positions=new List<Tile>();
		
		mesh_bounds=tile_mesh.renderer.bounds;
		main_camera=GameObject.Find("Main Camera").GetComponent<CameraScript>();
		
		var hexa_size=mesh_bounds.size;
		
		terrain=new Tile[LevelWidth,LevelHeight];
		
		var pos=Vector3.zero;
		float tile_w=hexa_size.x,tile_h=hexa_size.z;//ground_prefab.transform.localScale.x
		
		//creating tiles
		Vector3 coor=new Vector3(0,0,0);
		
		for (int i=0;i<terrain.GetLength(0);i++)
		{
			if (i%2!=0)
				pos.z=tile_h/2;
			else
				pos.z=0;
			
			if (i!=0&&(i%2==0)){
				coor.x-=1;
			}
			coor.y=i;
			
			for (int j=0;j<terrain.GetLength(1);j++)
			{
				var tile=Instantiate(ground_prefab,pos,Quaternion.identity) as Transform;
				tile.transform.parent=transform;
				var ts=tile.GetComponent("Tile") as Tile;
				terrain[i,j]=ts;
				tiles.Add(ts);
				ts.setCoordinate(new Vector3(coor.x+j,coor.y,-((coor.x+j)+coor.y)));
				
				if (Subs.RandomPercent()<20){
					ts.Tile_Data.setMovementBounds(2f,0f);
					ts.Tile_Data.setTimeBounds(tile_random_timer_min,tile_random_timer_max,false);
				}

				pos.z+=tile_h+0.01f;
			}
			
			//pos.x+=tile_w+(tile_w*0.5f);
			pos.x+=tile_w*(3f/4f);
		}

		//create land shape DEV. circle
		
		int xx=terrain.GetLength(0)/2,yy=terrain.GetLength(1)/2;
		center_point=terrain[xx,yy].getCoordinate();
		var radius=xx-1;
		
		for (int i=0;i<terrain.GetLength(0);i++)
		{
			for (int j=0;j<terrain.GetLength(1);j++)
			{
				var ts=terrain[i,j];
				if (!HexaDistanceInside(center_point,ts.getCoordinate(),radius)){
					ts.gameObject.SetActive(false);
				}					
			}
		}
		
		//group up tiles
		int group_amount=radius+1;
		for (int g=0;g<group_amount;g++){
			radius=(group_amount-g);
			var data=new TileData(Vector3.zero,g);
			data.setMovementBounds(0.03f,0.03f);
			data.setTimeBounds(100,1000,true);
			tile_groups.Add(data);
			
			foreach (var ts in tiles){
				if (HexaDistanceInside(center_point,ts.getCoordinate(),radius)){
					ts.setTileGroup(data);
				}
			}
		}
		terrain[xx,yy].setTileGroup(null);//center not moving
		terrain[xx,yy].Tile_Data.Activate(false);

		terrain_timer=new Timer(terrain_deterioration_timer,OnTerrainTrigger);
		
		main_camera.transform.position=new Vector3(terrain[xx,yy].transform.position.x,main_camera.transform.position.y,main_camera.transform.position.z);
		main_camera.LookAtCenter(terrain[xx,yy].transform.position);
		main_camera.DetachPlane();
		
		//pause level
		Activate(false);
		
		foreach (var t in tile_groups){
			t.Activate(false);
		}
	}
	
	public void Activate(bool active){
		foreach (var t in tiles){
			t.Activate(active);
		}
		if (!active){
			foreach (var t in tile_groups){
				t.Activate(active);
			}
		}
		terrain_timer.Active=active;
	}
	
	public void LowerTerrain(){
		foreach (var t in tiles){
			t.Tile_Data.MoveDown();
		}
	}
	
	// Update is called once per frame
	void Update (){
		terrain_timer.Update();
		
		foreach (var tg in tile_groups){
			tg.Update();
		}
		
		//DEV!!!
		if (Input.GetKeyDown(KeyCode.Space)){
			Activate(!terrain_timer.Active);
		}
	}
	
	private int current_group_to_go=0;
	private bool all_fall_on_once=false;
	
	private void OnTerrainTrigger(){
		//var tile=tiles[Random.Range(0,tiles.Count)];
		//tile.gameObject.SetActive(!tile.gameObject.activeSelf);
		
		if (current_group_to_go<tile_groups.Count)
			{
			//jitter
			if (current_group_to_go+1<tile_groups.Count){
				tile_groups[current_group_to_go+1].Activate(true);
			}
			
			
			if (all_fall_on_once){
				tile_groups[current_group_to_go].MoveDown();
				}
			else{
				foreach (var t in tiles){
					if (!t.gameObject.activeSelf) continue;
					if (t.Tile_Group==null) continue;
					//DEV. list in group? Too static? Could make a cascade!
					if (t.Tile_Group.tile_group==tile_groups[current_group_to_go].tile_group){
						t.Tile_Data.SetOnTimerRandomDown();
					}
				}
			}
		
			//zoom camera
			if (current_group_to_go>0)
				NotificationCenter.Instance.sendNotification(new CameraZoom_note(0.1f));
			
			current_group_to_go++;
		}

		
	}
	
	private bool HexaDistanceInside(Vector3 center,Vector3 tile,int distance){
		Vector3 dif=center-tile;
		if (center.z<tile.z){
			dif=tile-center;
		}

		float dis=Mathf.Max(Mathf.Abs(dif.x),Mathf.Abs(dif.y),Mathf.Abs(dif.z));
		return dis<=distance;
	}
	
	/*
	void OnGUI(){
		for (int i=0;i<terrain.GetLength(0);i++)
		{
			for (int j=0;j<terrain.GetLength(1);j++)
			{
				var ts=terrain[i,j];
				if (!ts.gameObject.activeSelf) continue;
				int xxx=60;
				if (i%2==0)
					xxx=0;
				var pos=new Vector2(10+xxx+j*130,10+i*25);
				GUI.Box(new Rect(pos.x,pos.y,120,20),""+ts.getCoordinate());
			}
		}
	}*/
}
