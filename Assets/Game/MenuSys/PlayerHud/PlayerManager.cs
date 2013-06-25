using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum playerState{notConnected, connected, ready}

public class PlayerManager : MonoBehaviour
{
	public GameHudController gamehudcontroller;
	
	public GameObject player_prefab;
	public List<playerData> players= new List<playerData>();
	public bool gameStarting = false;
	public UIButton player1button,player2button,player3button, player4button;
	public Transform spawner1,spawner2,spawner3,spawner4;
	public UILabel counter;
	public string defaultMessage;
	public float timer=5,timerMax=5;
	float totalTime=0f;
	
	// Use this for initialization
	void Awake() {
        DontDestroyOnLoad(transform.gameObject);
		players.Add(new playerData(1,player1button,spawner1));
    	players.Add(new playerData(2,player2button,spawner2));
		players.Add(new playerData(3,player3button,spawner3));
		players.Add(new playerData(4,player4button,spawner4));
		
		//DEV.temp
		players[0].color=Color.blue;
		players[1].color=Color.red;
		players[2].color=Color.green;
		players[3].color=Color.yellow;
		
		//create players
		foreach (var p in players){
			var po=MonoBehaviour.Instantiate(player_prefab,p.Spawner.position,Quaternion.identity) as GameObject;
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
			foreach(playerData pata in players){
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

public class playerData{
	public int controllerNumber;
	public playerState state = playerState.ready;
	public Color color;
	public UIButton button;
	public Transform Spawner;
	
	public int ResourceAmount=100;
	
	public playerData(int controller,UIButton button,Transform spawner){
		this.button=button;
		controllerNumber=controller;
		color = Color.white;
		Spawner=spawner;
	}
}
