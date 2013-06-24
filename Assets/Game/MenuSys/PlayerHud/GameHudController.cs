using UnityEngine;
using System.Collections;


public class GameHudController : MonoBehaviour {
	
	public PlayerHudMain[] playerHuds;
	// Use this for initialization
	void Start () {

	}
	
	public void setPlayerMenus(){
		var players=GameObject.FindGameObjectsWithTag("Player");
		
		for (int i=0;i<playerHuds.Length;i++){
			if (players.Length>i){
				var p =players[i].GetComponent<PlayerMain>();
				playerHuds[i].Player=p;
			}
			else{
				playerHuds[i].gameObject.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	
	
	bool not_started=false;
	void Update (){
	
		if (not_started){
			bool allready=true;
			foreach (var ph in playerHuds){
				if (!ph.gameObject.activeSelf) continue;
				
				if (ph.state!=AbilityMenuState.Ready){
					allready=false;
					break;
				}
			}
			if (allready){
				not_started=false;
				///start game
				GameObject.Find("LevelController").GetComponent<TerrainController>().Activate(true);
			}
		}
	}
}
