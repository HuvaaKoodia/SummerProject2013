using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainController : MonoBehaviour {
		
	public Transform ground_prefab;
	
	private Tile[,] terrain;
	private List<Tile> tiles=new List<Tile>();
	
	private List<TileData> tile_groups=new List<TileData>();
	
#pragma warning disable
	Timer terrain_timer;
	
	// Use this for initialization
	void Start (){
		
		var hexa_size=Tile.mesh_bounds.size;
		
		terrain=new Tile[20,20];
		
		var pos=Vector3.zero;
		float tile_w=hexa_size.x,tile_h=hexa_size.z;//ground_prefab.transform.localScale.x
		
		Vector3 coor=new Vector3(0,0,0);
		
		for (int i=0;i<terrain.GetLength(0);i++)
		{
			/*if (i%2!=0)
				pos.x=tile_w*(3f/4f);
			else
				pos.x=0;
			
						
			if (i!=0&&(i%2==0)){
				coor.x-=1;
			}
			coor.y=i;
			
			for (int j=0;j<terrain.GetLength(1);j++)
			{
				var tile=Instantiate(ground_prefab,pos,Quaternion.identity) as Transform;
				terrain[i,j]=tile;
				tiles.Add(tile);
				pos.x+=tile_w+(tile_w*0.5f);
				
				var tt=tile.GetComponent("Tile") as Tile;
				
				//coor.x=j;
				//coor.y=0;
				tt.setCoordinate(new Vector3(coor.x+j,coor.y,-((coor.x+j)+coor.y)));
			}
			
			pos.z+=tile_h/2;
			*/
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
				var ts=tile.GetComponent("Tile") as Tile;
				terrain[i,j]=ts;
				tiles.Add(ts);
				ts.setCoordinate(new Vector3(coor.x+j,coor.y,-((coor.x+j)+coor.y)));
				
				pos.z+=tile_h;
			}
			
			//pos.x+=tile_w+(tile_w*0.5f);
			pos.x+=tile_w*(3f/4f);
		}
		terrain_timer=new Timer(10000,OnTerrainTrigger);
		//terrain_timer.Active=false;
		
		//create land shape DEV. cicle
		
		int xx=terrain.GetLength(0)/2,yy=terrain.GetLength(1)/2;
		var center_point=terrain[xx,yy].getCoordinate();
		var radius=xx-1;
		
		for (int i=0;i<terrain.GetLength(0);i++)
		{
			for (int j=0;j<terrain.GetLength(1);j++)
			{
				var ts=terrain[i,j];
				if (HexaDistanceInside(center_point,ts.getCoordinate(),radius)){
					ts.gameObject.SetActive(true);
				}
				else
					ts.gameObject.SetActive(false);
			}
		}
		
		//group up
		
		int group_amount=radius+1;
		for (int g=0;g<=group_amount;g++){
			radius=(group_amount-g);
			var data=new TileData(Vector3.zero,g,false);
			tile_groups.Add(data);
			
			foreach (var ts in tiles){
				if (HexaDistanceInside(center_point,ts.getCoordinate(),radius)){
					ts.setTileGroup(data);
				}
			}
		}
		terrain[xx,yy].setTileGroup(null);
			/*
			foreach (var t in tiles){
				if (Vector2.Distance(center_point,new Vector2(t.transform.position.x,t.transform.position.z))<radius){
					var tt=t.GetComponent("Tile") as Tile;
					tt.setTileGroup(data);
				}
			}*/
		
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
	
	// Update is called once per frame
	void Update (){
		foreach (var tg in tile_groups){
			tg.Update();
		}
		
	}
	
	private int current_group_to_go=1;
	
	private void OnTerrainTrigger(){
		//var tile=tiles[Random.Range(0,tiles.Count)];
		//tile.gameObject.SetActive(!tile.gameObject.activeSelf);
		
		if (current_group_to_go<tile_groups.Count)
		{
			tile_groups[current_group_to_go].MoveDown();
			
			current_group_to_go++;
		}
	}
	
	private bool HexaDistanceInside(Vector3 center,Vector3 tile,int distance){
		
		/*Vector3 centerz=new Vector3(center.x,center.y,-(center.x+center.y));
		Vector3 tilez=new Vector3(tile.x,tile.y,-(tile.x+tile.y));
		*/
		
		Vector3 dif=center-tile;
		if (center.z<tile.z){
			dif=tile-center;
		}
		
		//float dis=Mathf.Max(dif.x,dif.y,dif.z);
		float dis=Mathf.Max(Mathf.Abs(dif.x),Mathf.Abs(dif.y),Mathf.Abs(dif.z));
		
		return dis<=distance;
	}
}
