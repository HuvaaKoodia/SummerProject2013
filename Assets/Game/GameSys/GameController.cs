using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NotificationSys;
using System.Linq;

public class GameController : MonoBehaviour {
	
	public GameoverPanel gameoverPanel;
	public PlayerManager playerManager;
	public TerrainController terrainController;
	
	List<PlayerScoreData> score_list;
	int last_alive=4;
	
	// Use this for initialization
	void Start () {
		gameoverPanel.gameObject.SetActive(false);
		 score_list=new List<PlayerScoreData>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (playerManager.State==GameState.GameOn){
			//check players
			int playing=0,alive=0;
			PlayerData winner=null;
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
				int score=10;
				foreach (var psd in score_list){
					psd.score=score;
					score/=2;
				}
				
				//game over!
				playerManager.changeState(GameState.Gameover);
				winner.Player.freezePlayer();
				
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
	}
	
	void addScore(PlayerData p){
		score_list.Add(new PlayerScoreData(p,0));
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
