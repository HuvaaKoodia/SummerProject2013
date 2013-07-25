using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameoverPanel : MonoBehaviour {
	
	public UILabel winner_label,score_label,continue_label;
	
	public void setPlayer(PlayerData p){
		winner_label.text=("Player "+p.controllerNumber+" wins!").ToUpper();
		continue_label.text=("Player "+p.controllerNumber+" press:\n- start for next round.\n- back for main menu.").ToUpper();
	}

	public void setScores (List<PlayerScoreData> scores)
	{
		string t="";
		foreach(var p in scores){
			t+=("[FFFFFF]Player "+p.player.controllerNumber).ToUpper()+" q" + ("[FFF242]"+p.score+" r\n").ToUpper();
		}
		score_label.text=t;
	}
	
	public void setTie(){
		winner_label.text=("It's a tie").ToUpper();
		score_label.gameObject.SetActive(false);
		continue_label.text=("Press:\n- start for next round.\n- back for main menu.").ToUpper();
	}
}
