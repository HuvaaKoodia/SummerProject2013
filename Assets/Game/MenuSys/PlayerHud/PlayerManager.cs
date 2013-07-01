using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameState{Setup,GameOn,Gameover}

public class PlayerManager : MonoBehaviour
{
	public GameObject player_prefab;
	public GameHudController gamehudcontroller;
	public List<Transform> spawners;
	public TerrainController terrain_control;
	
	public PlayerDatabase pDB{get;private set;}
	
	//timer and stuff
	public bool gameStarting = false;
	
	public UILabel counter;
	public string defaultMessage;
	public float timer=5,timerMax=10;
	float totalTime=0f;
	
	public GameState State{get;private set;}
	
	// Use this for initialization
	void Awake() {
		//fetch playerDatabase
		pDB=GameObject.FindWithTag("EntityDatabase").GetComponent<PlayerDatabase>();
		
		//DEV.TEMP!
		if (pDB.players.Count==0)
			pDB.CreatePlayers();

		if (gamehudcontroller!=null)
			gamehudcontroller.setPlayerMenus();
		
		State=GameState.Setup;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(State==GameState.Setup&&gameStarting){
			totalTime -= Time.deltaTime;
			timer=Mathf.Round(totalTime*10)/10;
			
			counter.text=timer.ToString();
			if(timer<=0){
				timer=0;
				gameStarting=false;
				StartGame();
			}
		}
	}
	
	void StartGame(){
		counter.text="";
		terrain_control.Activate(true);
		State=GameState.GameOn;
		
		//deleting gibs and projetiles
		var gibs=GameObject.FindGameObjectsWithTag("Gib");
		foreach (var g in gibs){Destroy(g.gameObject);}
		
		var pros=GameObject.FindGameObjectsWithTag("Projectile");
		foreach (var p in pros){Destroy(p);}
		
		
		//delete all players and recreate them
		foreach(var p in pDB.players){
			DestroyPlayer(p);
			if (p.state==PlayerState.ready)
				CreatePlayer(p);
		}
	}
	
	
	public void startCounter(bool start){
		if(start){
			int playerCount=0;
			
			foreach(var pata in pDB.players){
				if(pata.state!=PlayerState.notConnected){
					playerCount++;
				}
			}
			//if(readyCount == playerCount && playerCount >= 2 &&!gameStarting){
			if (gamehudcontroller.Allready()&&playerCount>=2&&!gameStarting){
				gameStarting=true;
				totalTime=timerMax;
			}
		}
		else{
			gameStarting=false;
			counter.text=defaultMessage;
		}
	}
	
	public void CreatePlayer(PlayerData data){
		var po=MonoBehaviour.Instantiate(player_prefab,spawners[data.controllerNumber-1].position,Quaternion.identity) as GameObject;
		var player=po.GetComponent<PlayerMain>();
		
		player.Data=data;
		data.Player=player;
	}
	
	public void changeState(GameState state){
		State=state;
		
		//change effects
	}
	
	
	public void DestroyPlayer(PlayerData player){
		if (player.Player!=null){
			DestroyImmediate(player.Player.gameObject);
		}
	}
}

