using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameoverPanel : MonoBehaviour {
	
	public UILabel winner_label,score_label;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setPlayer(PlayerData p){
		winner_label.text="Player "+p.controllerNumber+" wins!";
	}

	public void setScores (List<PlayerScoreData> scores)
	{
		string t="";
		foreach(var p in scores){
			t+="Player "+p.player.controllerNumber+" +"+p.score+" r\n";
		}
		score_label.text=t;
	}
}
