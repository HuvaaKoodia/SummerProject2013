using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainController : MonoBehaviour {
		
	public Transform ground_prefab;
	
	private Transform[,] terrain;
	private List<Transform> tiles=new List<Transform>();
	
#pragma warning disable
	Timer terrain_timer;
	
	// Use this for initialization
	void Start (){
		
		var hexa_size=Tile.mesh_bounds.size;
		
		terrain=new Transform[14,14];
		
		var pos=Vector3.zero;
		float tile_w=hexa_size.x,tile_h=hexa_size.z;//ground_prefab.transform.localScale.x
		
		for (int i=0;i<terrain.GetLength(0);i++)
		{
			if (i%2!=0)
				pos.x=tile_w*(3f/4f);
			else
				pos.x=0;
			
			for (int j=0;j<terrain.GetLength(1);j++)
			{
				var tile=Instantiate(ground_prefab,pos,Quaternion.identity) as Transform;
				terrain[i,j]=tile;
				tiles.Add(tile);
				pos.x+=tile_w+(tile_w*0.5f);
			}
			
			pos.z+=tile_h/2;
		}
		terrain_timer=new Timer(1000,OnTerrainTrigger);
	}
	
	// Update is called once per frame
	void Update (){
		
	}
	
	private void OnTerrainTrigger(){
		var tile=tiles[Random.Range(0,tiles.Count)];
		
		tile.gameObject.SetActive(!tile.gameObject.activeSelf);
		//tiles.Remove(tile);
	}
}
