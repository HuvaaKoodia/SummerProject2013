using UnityEngine;
using System.Collections;


public class GameHudController : MonoBehaviour {
	
	public PlayerHudMain[] playerHuds;
	
	public void setPlayerMenus(){
		var playerDatabase=GameObject.FindWithTag("EntityDatabase").GetComponent<PlayerDatabase>();
		var players=playerDatabase.players;
		
		for (int i=0;i<playerHuds.Length;i++){
			if (players.Count>i){
				playerHuds[i].playerData=players[i];
				players[i].Hud=playerHuds[i];
			}
			else{
				playerHuds[i].gameObject.SetActive(false);
			}
		}
	}
	
	public bool start_when_all_ready;
	void Update (){
	
	if (start_when_all_ready){
			if (Allready()){
				start_when_all_ready=false;
				///start game
				GameObject.Find("LevelController").GetComponent<TerrainController>().Activate(true);
			}
		}
	}
	
	public bool Allready(){
		foreach (var ph in playerHuds){
				//if (!ph.gameObject.activeSelf) continue;
				
				if (ph.playerData.state==PlayerState.ready&&ph.state!=AbilityMenuState.Ready){
					return false;
				}
			}
		return true;
	}
}
