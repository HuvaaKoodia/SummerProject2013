using UnityEngine;
using System.Collections;

public class startogame : MonoBehaviour {
	
	public LevelPlaylist pl;
	
	// Use this for initialization
	void Start () {
		pl.gotoCurrentMap();
	}
}
