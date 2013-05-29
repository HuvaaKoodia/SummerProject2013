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
		terrain=new Transform[10,10];
		
		var pos=Vector3.zero;
		float tile_width=1;//ground_prefab.transform.localScale.x
		
		for (int i=0;i<terrain.GetLength(0);i++)
		{
			if (i%2!=0)
				pos.x=tile_width/2;
			else
				pos.x=0;
			
			for (int j=0;j<terrain.GetLength(1);j++)
			{
				var tile=Instantiate(ground_prefab,pos,Quaternion.identity) as Transform;
				terrain[i,j]=tile;
				tiles.Add(tile);
				pos.x+=tile_width;
			}
			
			pos.z+=tile_width;
		}
		terrain_timer=new Timer(10000,OnTerrainTrigger);
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
