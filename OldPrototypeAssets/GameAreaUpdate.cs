using UnityEngine;
using System.Collections;

public class GameAreaUpdate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	Vector3 scale = transform.localScale;
		scale=scale/40;
		transform.localScale -= scale*Time.deltaTime; 
		
	}
}
