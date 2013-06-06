using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum playerState{notConnected, connected, ready}
public class PlayerManager : MonoBehaviour
{
	public enum playerState{notConnected, connected, ready}
	public class playerData{
		
		public playerState state = playerState.notConnected;
		public Color color;
		public UIButton button;
	public playerData(UIButton button){
		this.button=button;
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
		players.Add(new playerData(player1button));
    	players.Add(new playerData(player2button));
		players.Add(new playerData(player3button));
		players.Add(new playerData(player4button));
	}
	void Start ()
	{
	
		defaultMessage=counter.text;
	}
	
	// Update is called once per frame
	void Update ()
	{
		playerCount=0;
		for(int i=0; i<4; i++){
		var player=players[i];
			if (Input.GetButtonDown ("Start_"+(i+1))) {
				if(player.state!=playerState.ready)
						player.state++;
					
				if(player.state==playerState.connected){
					startCounter(true);
					player.button.OnPress(true);
					}
				else{
					startCounter(false);
					player.button.OnPress(true);
						}
			}
		
		if(player.state>0){playerCount++;}
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
		if(readyCount==playerCount&&playerCount>=2&&!gameStarting){
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
