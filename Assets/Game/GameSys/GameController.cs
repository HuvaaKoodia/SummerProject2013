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
	
	MusicSys music_sys;
	
	LevelPlaylist level_stats;
	GameplayStats stats;
	
	List<PlayerScoreData> score_list;
	int last_alive=4;
	
	public bool GameStarting{get;private set;}
	
	private float timer;
	public float count_down_timer=5;
	
	public GameState State{get;private set;}
	
	List<int> scores;
	
	// Use this for initialization
	void Start (){
		var mus=GameObject.FindGameObjectWithTag("MusicSys");
		if (mus)
			music_sys=mus.GetComponent<MusicSys>();
		
		level_stats=GameObject.FindGameObjectWithTag("LevelPlaylist").GetComponent<LevelPlaylist>();
		
		gameoverPanel.gameObject.SetActive(false);
		score_list=new List<PlayerScoreData>();
		
		State=GameState.Setup;
		
		var entDB=GameObject.FindGameObjectWithTag("EntityDatabase");
		stats=entDB.GetComponent<GameplayDatabase>().Stats[0];
		
		scores=new List<int>();
		scores.Add(stats.Score_first);
		scores.Add(stats.Score_second);
		scores.Add(stats.Score_third);
		scores.Add(stats.Score_fourth);
		
		if (music_sys)
			music_sys.StopCurrent();
	}
	PlayerData winner=null;
	// Update is called once per frame
	void Update () {
		
		if(State==GameState.Setup&&GameStarting){
			timer -= Time.deltaTime;
			var t=Mathf.Round(timer*10)/10;
			
			startgameCounter.setTime(t);
			if(timer<=0){
				timer=0;
				GameStarting=false;
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
				if (winner.Player!=null&&winner.Player.onLegitGround()){
					addScore(winner);
					
					//set score
					for (int i=0;i<score_list.Count;i++){
						var psd = score_list[i];
						
						var score= scores[i];
						psd.score=score;
						psd.player.ResourceAmount+=score;
					}
					
					//game over!
					changeState(GameState.Gameover);
					//winner.Player.freezePlayer();
					winner.Player.toggleInvulnerability();
					
					//hud texts
					gameoverPanel.gameObject.SetActive(true);
					gameoverPanel.setPlayer(winner);
					
					gameoverPanel.setScores(score_list);
					
					//halt terrain
					terrainController.Activate(false);
					terrainController.LowerTerrain();
					//NotificationCenter.Instance.sendNotification(new CameraZoom_note(5f,winner.Player.transform));
					
					NotificationCenter.Instance.sendNotification(new CameraZoom_note(0.95f,true));
				}
			}
			
			if (alive==0){
				winner=null;
				changeState(GameState.Gameover);

				//hud texts
				gameoverPanel.gameObject.SetActive(true);
				gameoverPanel.setTie();
				
				terrainController.Activate(false);
			}
		}
		if (State==GameState.Gameover){
			if (winner!=null){
				PressStart(winner.controllerNumber);
				PressBack(winner.controllerNumber);
			}
			else{
				foreach (var p in playerManager.pDB.players){
					PressStart(p.controllerNumber);
					PressBack(p.controllerNumber);
				}
			}
		}
	}
	
	void PressStart(int controllerNumber){
		if (Input.GetButton("Start_"+controllerNumber)){
			//next round or post game stats
			var lvlDB=GameObject.FindGameObjectWithTag("LevelPlaylist");
			if (lvlDB==null){//DEV.DEBUG
				Application.LoadLevel(Application.loadedLevel);
			}
			else{
				var scr=lvlDB.GetComponent<LevelPlaylist>();
				scr.gotoNextMap();
			}
		}
	}
	
	void PressBack(int controllerNumber){
		if (Input.GetButton("Back_"+controllerNumber)){
			//next round or post game stats
			Application.LoadLevel("TitleScene");
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
		
		if (music_sys)
			music_sys.StartGameTrack();
	}
	
	public void startCounter(bool start){
		if(start){
			int playerCount=0;
			
			foreach(var pata in playerManager.pDB.players){
				if(pata.state==PlayerState.ready){
					playerCount++;
				}
			}
			//if(readyCount == playerCount && playerCount >= 2 &&!gameStarting){
			if (playerManager.gamehudcontroller.Allready()&&playerCount>=2&&!GameStarting){
				GameStarting=true;
				timer=count_down_timer;
				startgameCounter.gameObject.SetActive(true);
				startgameCounter.setRound(level_stats.CurrentRound);
			}
		}
		else{
			GameStarting=false;
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
