using UnityEngine;
using System.Collections;

public class EngineController : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Timer.clearTimers();
		NotificationCenter.resetInstance();
	}
	
	// Update is called once per frame
	void Update (){
		Timer.UpdateTimers();
		
		if (Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel(0);
		}
		
		Debug.Log("amount: "+Timer.timers.Count);
	}
}
