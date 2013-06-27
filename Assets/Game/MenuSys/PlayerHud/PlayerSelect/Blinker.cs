using UnityEngine;
using System.Collections;

public class Blinker : MonoBehaviour
{
	Timer timer;
	public Transform componentToBlink;
	
	
	public PlayerHudMain playerHud;
	
	int controller = 1;
	public PlayerState neededState;
	public bool needsAState=false;
	
	// Use this for initialization
	void Start ()
	{
		//manager = GameObject.Find ("PLAYERDATAS!").GetComponent<PlayerManager> ();
		timer = new Timer (500, iBlink,false);
		//int.TryParse (name [3].ToString (), out controller);
	}

	// Update is called once per frame
	void Update ()
	{
		timer.Update ();
	}

	void iBlink ()
	{	
		if(needsAState){
			if(playerHud.playerManager.pDB.players [controller - 1].state == neededState){
				doIt();	
			}	
		}else{doIt();}
		
	}
	void doIt(){
		if (!playerHud.playerManager.gameStarting) {
			componentToBlink.gameObject.SetActive (!componentToBlink.gameObject.activeSelf);
		}
	}
}