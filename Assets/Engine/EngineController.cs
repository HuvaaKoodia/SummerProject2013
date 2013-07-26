using UnityEngine;
using System.Collections;
using NotificationSys;

public class EngineController : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Timer.clearTimers();
		NotificationCenter.resetInstance();
	}
	
	//Update is called once per frame
	void Update (){
		Timer.UpdateTimers();
		
		if (Input.GetKey(KeyCode.T)){
			Application.LoadLevel("TitleScene");
		}
		if (Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel(Application.loadedLevel);
		}
		
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
