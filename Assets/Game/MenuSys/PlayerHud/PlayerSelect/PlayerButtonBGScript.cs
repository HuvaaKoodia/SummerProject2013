using UnityEngine;
using System.Collections;

public class PlayerButtonBGScript : MonoBehaviour {

	public void setColor(Color color){
		
		if(GetComponent<UISprite>()!=null){
			GetComponent<UISprite>().color=color;
		}
		if(GetComponent<UILabel>()!=null){
			GetComponent<UILabel>().color=color;
		}	
	}
}
