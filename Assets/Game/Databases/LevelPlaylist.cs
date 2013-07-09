using UnityEngine;
using System.Collections;

public class LevelPlaylist : MonoBehaviour {
	
	int current_map;
	public string[] Scene_list,Level_list;
	
	void Start(){
	}
	
	/// <summary>
	/// Returns the next scene name and forwards the current map index.
	/// </returns>
	public string getScene(){
		var s=Scene_list[current_map];
		current_map=Subs.Add(current_map,0,Scene_list.Length);
		return s;
	}
	/// <summary>
	///Goes to the next map.
	/// </returns>
	public void gotoNextMap(){
		Application.LoadLevel(getScene());
	}
	
	public string getCurrentSceneName ()
	{
		return Scene_list[current_map];
	}
	
	public string getCurrentLevelName ()
	{
		return Level_list[current_map];
	}
}
