using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerManager : MonoBehaviour
{
	public GameHudController gamehudcontroller;
	public List<Transform> spawners;
	
	public PlayerDatabase pDB{get;private set;}

	// Use this for initialization
	void Awake() {
		//fetch playerDatabase
		pDB=GameObject.FindWithTag("EntityDatabase").GetComponent<PlayerDatabase>();
		
		//DEV.TEMP!
		if (pDB.players.Count==0)
			pDB.CreatePlayers();

		if (gamehudcontroller!=null)
			gamehudcontroller.setPlayerMenus();
	}
		
	public void CreatePlayer(PlayerData data){
		var spawner=spawners[data.controllerNumber-1];
		var po=MonoBehaviour.Instantiate(pDB.PlayerPrefab,spawner.position,Quaternion.identity) as GameObject;
		var player=po.GetComponent<PlayerMain>();
		
		player.setStartRotation(spawner.rotation);
		player.stats=pDB.player_stats;
		player.Data=data;
		data.Player=player;
	}
	
	public void DestroyPlayer(PlayerData player){
		if (player.Player!=null){
			DestroyImmediate(player.Player.gameObject);
		}
	}
}

