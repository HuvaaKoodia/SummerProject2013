using UnityEngine;
using System.Collections;

public class PressStartToGOTO : MonoBehaviour {
	
	public string Level;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int i=1;i<=4;i++){
			if (Input.GetButtonDown("Start_"+i)){
				Application.LoadLevel(Level);
			}
		}
	}
}
