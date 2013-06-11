using UnityEngine;
using System.Collections;

public class Blinker : MonoBehaviour
{
	Timer timer;
	public Transform componentToBlink;
	PlayerManager manager;
	int controller = 1;
	public PlayerManager.playerState neededState;
	public bool needsAState=false;
	
	// Use this for initialization
	void Start ()
	{
		manager = GameObject.Find ("PLAYERDATAS!").GetComponent<PlayerManager> ();
		timer = new Timer (500, iBlink);
		int.TryParse (name [3].ToString (), out controller);
	}

	// Update is called once per frame
	void Update ()
	{
		timer.Update ();
	}

	void iBlink ()
	{	if(needsAState){
		if(manager.players [controller - 1].state == neededState){
			doIt();	
			}	
		}else{doIt();}
		
	}
	void doIt(){
		if (!manager.gameStarting) {
			componentToBlink.gameObject.SetActive (!componentToBlink.gameObject.activeSelf);
		}
	}
}