using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerManager : MonoBehaviour
{
	public GameObject player_prefab;
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
		var po=MonoBehaviour.Instantiate(player_prefab,spawners[data.controllerNumber-1].position,Quaternion.identity) as GameObject;
		var player=po.GetComponent<PlayerMain>();
		
		player.Data=data;
		data.Player=player;
	}
	
	public void DestroyPlayer(PlayerData player){
		if (player.Player!=null){
			DestroyImmediate(player.Player.gameObject);
		}
	}
}

