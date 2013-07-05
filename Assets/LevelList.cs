using UnityEngine;
using System.Collections;

public class LevelList : MonoBehaviour {
	
	public GameObject[] AllLevels;
	public int current_level;
	public string[] Playlist;
	
	void Start(){
		Application.LoadLevel(Playlist[0]);
	}
	
	/// <summary>
	/// Returns the next level name.
	/// </returns>
	public string getNext(){
		current_level=Subs.Add(current_level,0,Playlist.Length);
		return Playlist[current_level];
	}
	/// <summary>
	///Goes to the next level.
	/// </returns>
	public void gotoNext(){
		Application.LoadLevel(getNext());
	}
}
