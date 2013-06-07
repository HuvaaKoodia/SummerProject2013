using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum playerState{notConnected, connected, ready}
public class PlayerManager : MonoBehaviour
{
	public enum playerState{notConnected, connected, ready}
	public class playerData{
		
		public int controllerNumber;
		public playerState state = playerState.notConnected;
		public Color color;
		public UIButton button;
		public PlayerMain Player;
		
		public playerData(int controller,UIButton button){
			this.button=button;
			controllerNumber=controller;
		}
	}
	
	public List<playerData> players= new List<playerData>();
	
	int playerCount=0;
	//bool player1in = false, player2in = false, player3in = false, player4in = false;
	public bool gameStarting = false;
	//bool player1ready=false, player2ready=false, player3ready=false, player4ready=false;
	public UIButton player1button,player2button,player3button, player4button;
	public UILabel counter;
	public string defaultMessage;
	public float timer=5,timerMax=5;
	float totalTime=0f;
	// Use this for initialization
	void Awake() {
        DontDestroyOnLoad(transform.gameObject);
		players.Add(new playerData(1,player1button));
    	players.Add(new playerData(2,player2button));
		players.Add(new playerData(3,player3button));
		players.Add(new playerData(4,player4button));
	}
	void Start ()
	{
	
		defaultMessage=counter.text;
	}
	
	// Update is called once per frame
	void Update ()
	{
				
		
		for(int i=0; i<4; i++){
		var player=players[i];
		
			playerCount=0;
			foreach(playerData pata in players){
			if(pata.state > 0){
				
			playerCount++;
					
			}
				
			}
			if (Input.GetButtonDown ("Start_"+(i+1))) {
				if(player.state!=playerState.ready)
						player.state++;
					if(player.state==playerState.ready){
					
					Transform transformer,transformer2;
					transformer=player.button.transform.FindChild("ReadyLabel");
					transformer2=player.button.transform.FindChild("ColorLabel");
					
					NGUITools.SetActive(transformer.gameObject, true);
					NGUITools.SetActive(transformer2.gameObject, false);
					}
				if(player.state==playerState.connected){
					startCounter(true);
					player.button.OnPress(true);
					}
				else if(player.state==playerState.ready){
					startCounter(true);
				}
				else{
					startCounter(false);
					player.button.OnPress(true);
						}
			}else if(Input.GetButtonDown("B_"+(i+1))){
				if(player.state>0){
					startCounter(false);
					player.state--;
				}
			}
		
			
		
		}
		
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
	public void startCounter(bool start){
		if(start){
			int readyCount=0;
			foreach(playerData pata in players){
			if(pata.state==playerState.ready){
				readyCount++;
				}
			}
			Debug.Log(readyCount + " " + playerCount + " " + gameStarting);
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