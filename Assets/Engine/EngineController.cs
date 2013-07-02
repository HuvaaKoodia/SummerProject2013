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
			int level=0;
			for (int i=(int)KeyCode.Alpha0;i<=(int)KeyCode.Alpha9;i++){
				if (Input.GetKeyDown((KeyCode)i)){
					Application.LoadLevel(level);
				}
				level++;
			}
		}
		if (Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel(Application.loadedLevel);
		}
		
		if (Input.GetKeyDown(KeyCode.Escape)){
			Application.Quit();
		}
	}
}
