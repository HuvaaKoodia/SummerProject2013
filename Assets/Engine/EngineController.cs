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
		
		if (Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	void OnGUI(){
		
		int a=0;
		foreach (var t in Timer.timers)
		{
			if(t.Active)
				a++;
		}
		
		GUI.Box (new Rect(10,10,100,50),"Timers: "+Timer.timers.Count+"\nActive: "+a);
	}
}
