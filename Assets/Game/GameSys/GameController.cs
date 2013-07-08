using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NotificationSys;
using System.Linq;

public enum GameState{Setup,GameOn,Gameover}

public class GameController : MonoBehaviour {
	
	public GameoverPanel gameoverPanel;
	public StartgameCounter startgameCounter;
	public PlayerManager playerManager;
	public TerrainController terrainController;
	
	List<PlayerScoreData> score_list;
	int last_alive=4;
	
	public bool gameStarting = false;
	
	private float timer;
	public float count_down_timer=5;
	
	public GameState State{get;private set;}
	
	// Use this for initialization
	void Start () {
		gameoverPanel.gameObject.SetActive(false);
		score_list=new List<PlayerScoreData>();
				
		State=GameState.Setup;
	}
	PlayerData winner=null;
	// Update is called once per frame
	void Update () {
		
		if(State==GameState.Setup&&gameStarting){
			timer -= Time.deltaTime;
			var t=Mathf.Round(timer*10)/10;
			
			startgameCounter.setTime(t);
			if(timer<=0){
				timer=0;
				gameStarting=false;
				StartGame();
			}
		}
		
		
		if (State==GameState.GameOn){
			//check players
			int playing=0,alive=0;
			
			foreach (var p in playerManager.pDB.players){
				if (p.state==PlayerState.ready){
					playing++;
					if (p.Player!=null){
						alive++;
						winner=p;
					}
				}
			}
			//check destroyed players
			if (last_alive!=alive){
				last_alive=alive;
				
				foreach (var p in playerManager.pDB.players){
					if (p.state==PlayerState.ready){
						if (p.Player==null){
							if (!score_list.Any(item=>item.player==p)){
								addScore(p);
							}
						}
					}
				}
			}
			
			if (alive==1){
				addScore(winner);
				
				//set scores
				int score=50;
				foreach (var psd in score_list){
					psd.score=score;
					score/=2;
					psd.player.ResourceAmount+=score;
				}
				
				//game over!
				changeState(GameState.Gameover);
				winner.Player.freezePlayer();
				winner.Player.toggleInvulnerability();
				
				//hud texts
				gameoverPanel.gameObject.SetActive(true);
				gameoverPanel.setPlayer(winner);
				
				gameoverPanel.setScores(score_list);
				
				//halt terrain
				terrainController.Activate(false);
				terrainController.LowerTerrain();
				NotificationCenter.Instance.sendNotification(new CameraZoom_note(5f,winner.Player.transform));
			}
		}
		if (State==GameState.Gameover){
			if (Input.GetButton("Start_"+winner.controllerNumber)){
				//next round or post game stats
				var lvlDB=GameObject.FindGameObjectWithTag("LevelDatabase") as GameObject;
				if (lvlDB==null){//DEV.DEBUG
					Application.LoadLevel(Application.loadedLevel);
				}
				else{
					var scr=lvlDB.GetComponent<LevelList>();
					scr.gotoNext();
				}
			}
			
			if (Input.GetButton("Back_"+winner.controllerNumber)){
				//next round or post game stats
				Application.LoadLevel("TitleScene");
			}
			
			if (Input.GetButton("Back_"+winner.controllerNumber)){
				//next round or post game stats
				Application.LoadLevel("MainMenu");
			}
		}
	}
	
	void addScore(PlayerData p){
		score_list.Insert(0,new PlayerScoreData(p,0));
	}
	
	void StartGame(){
		startgameCounter.gameObject.SetActive(false);
		terrainController.Activate(true);
		State=GameState.GameOn;
		
		//deleting gibs and projetiles
		var gibs=GameObject.FindGameObjectsWithTag("Gib");
		foreach (var g in gibs){Destroy(g.gameObject);}
		
		var pros=GameObject.FindGameObjectsWithTag("Projectile");
		foreach (var p in pros){Destroy(p);}
		
		
		foreach(var p in playerManager.pDB.players){
			//delete all players and recreate them
			playerManager.DestroyPlayer(p);
			if (p.state==PlayerState.ready){
				playerManager.CreatePlayer(p);
			}
			else{
				//turn off menus for inactive players
				if (p.Hud!=null)
					p.Hud.gameObject.SetActive(false);
			}
		}
	}
	
	public void startCounter(bool start){
		if(start){
			int playerCount=0;
			
			foreach(var pata in playerManager.pDB.players){
				if(pata.state!=PlayerState.notConnected){
					playerCount++;
				}
			}
			//if(readyCount == playerCount && playerCount >= 2 &&!gameStarting){
			if (playerManager.gamehudcontroller.Allready()&&playerCount>=2&&!gameStarting){
				gameStarting=true;
				timer=count_down_timer;
				startgameCounter.gameObject.SetActive(true);
				startgameCounter.setRound(1);
			}
		}
		else{
			gameStarting=false;
			startgameCounter.gameObject.SetActive(false);
		}
	}
	
	public void changeState(GameState state){
		State=state;
		
		//state change effects
	}
}

public class PlayerScoreData{
	public PlayerData player;
	public int score;
	
	public PlayerScoreData(PlayerData data,int score){
		player=data;
		this.score=score;
	}
}
