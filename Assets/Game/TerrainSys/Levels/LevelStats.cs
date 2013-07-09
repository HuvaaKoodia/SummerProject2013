using UnityEngine;
using System.Collections;

public class LevelStats : MonoBehaviour {
	
	public string 
		Name
		;
	
	public int 
		Terrain_deterioration_time=12225,
		Tile_random_time_min=2000,
		Tile_random_time_max=6000,
		Tile_random_low_change=20,
		Tile_random_high_change=10
		;
	
	public float 
		Tile_random_high_height=1f,
		Tile_random_low_height=0.5f;
}
