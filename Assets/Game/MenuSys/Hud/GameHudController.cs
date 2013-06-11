using UnityEngine;
using System.Collections;


public class GameHudController : MonoBehaviour {
	
	public PlayerHudPanel[] playerHuds;
	// Use this for initialization
	void Start () {
		//var playerdata=GameObject.Find("PLAYERDATA!").GetComponent<PlayerManager>();
		
		var players=GameObject.FindGameObjectsWithTag("Player");
		
		for (int i=0;i<4;i++){
			if (players.Length>i){
				var p =players[i].GetComponent<PlayerMain>();
				playerHuds[i].Player=p;
			}
			else{
				playerHuds[i].gameObject.SetActive(false);
			}
			/*
			 * UIAnchor.Side[] sides={UIAnchor.Side.TopLeft,UIAnchor.Side.TopRight,UIAnchor.Side.BottomLeft,UIAnchor.Side.BottomRight};
			 * 
			var obj=Instantiate(playerHud) as Transform;
			var scr=obj.GetComponent<PlayerHudPanel>();
			var pp=p.GetComponent<PlayerMain>();
			
			scr.Player=pp;
			Debug.Log("num: "+pp.controllerNumber+" side; "+sides[pp.controllerNumber-1]);
			scr.setHudOriantation(sides[pp.controllerNumber-1]);
			
			obj.parent=this.transform;
			obj.transform.localScale=Vector3.one;
			obj.transform.position=Vector3.zero;
			*/
		}
	}
	
	// Update is called once per frame
	void Update (){
	
	}
}
