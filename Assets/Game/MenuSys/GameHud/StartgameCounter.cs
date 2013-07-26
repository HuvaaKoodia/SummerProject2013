using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StartgameCounter : MonoBehaviour {
	
	public UILabel round_label,time_label;
	
	
	
	public void setRound(int round){
		round_label.text=("Round "+round).ToUpper();
	}

	public void setTime(float time)
	{
		time_label.text=(""+time).ToUpper();
	}
}
