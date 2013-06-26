using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
	public GameObject player_prefab;
	public GameHudController gamehudcontroller;
	public List<Transform> spawners;
	
	public PlayerDatabase pDB{get;private set;}
	
	//timer and stuff
	public bool gameStarting = false;
	public UIButton player1button,player2button,player3button, player4button;
	
	public UILabel counter;
	public string defaultMessage;
	public float timer=5,timerMax=5;
	float totalTime=0f;
	
	
	
	// Use this for initialization
	void Awake() {
		
		//fetch playerDatabase
		pDB=GameObject.FindWithTag("EntityDatabase").GetComponent<PlayerDatabase>();
		
		//DEV.TEMP!
		if (pDB.players.Count==0)
			pDB.CreatePlayers();
		
		//create players
		for(int i=0;i<pDB.players.Count;i++){
			var p = pDB.players[i];
			var po=MonoBehaviour.Instantiate(player_prefab,spawners[i].position,Quaternion.identity) as GameObject;
			var player=po.GetComponent<PlayerMain>();
			
			player.Data=p;
			player._Color=p.color;
			player.controllerNumber=p.controllerNumber;
		}
		
		//defaultMessage=counter.text;
		
		if (gamehudcontroller!=null)
			gamehudcontroller.setPlayerMenus();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(gameStarting){
			
			totalTime -= Time.deltaTime;
			timer=Mathf.Round(totalTime*10)/10;
			
			counter.text=timer.ToString();
			if(timer<=0){
				timer=0;
				//exit level
				Application.LoadLevel(1);
				gameStarting=false;
			}
		}
	}
	/*public Color defColor(){
	return new UnityEngine.Color(0f,0f,0f);
	}*/
	
	public void startCounter(bool start){
		if(start){
			int readyCount=0,playerCount=0;
			
			foreach(playerData pata in pDB.players){
				if(pata.state!=playerState.notConnected){
					playerCount++;
				}
				if(pata.state==playerState.ready){
					readyCount++;
				}
			}
			
		if(readyCount == playerCount && playerCount >= 2 &&!gameStarting){
			gameStarting=start;
			totalTime=timerMax;
			}
		}
		else{
			gameStarting=false;
			counter.text=defaultMessage;
		}
	}
}
