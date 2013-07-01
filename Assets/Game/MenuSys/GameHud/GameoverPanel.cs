using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameoverPanel : MonoBehaviour {
	
	public UILabel winner_label,score_label,continue_label;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setPlayer(PlayerData p){
		winner_label.text="Player "+p.controllerNumber+" wins!";
		continue_label.text="Player "+p.controllerNumber+" press start for next round.";
	}

	public void setScores (List<PlayerScoreData> scores)
	{
		string t="";
		foreach(var p in scores){
			t+="[FFFFFF]Player "+p.player.controllerNumber+" +[FFF242]"+p.score+" r\n";
		}
		score_label.text=t;
	}
}
