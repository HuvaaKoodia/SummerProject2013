using UnityEngine;
using System.Collections;

public class playerInsideArea : MonoBehaviour {
	GameObject[] gameObjects;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObjects = GameObject.FindGameObjectsWithTag("Player");
		Walker white;
		foreach(GameObject g in gameObjects){
		white = g.GetComponent<Walker>();
			if(white!=null) {
				if(collider.bounds.Contains(white.transform.position)){
					Debug.Log("trueDat");
				g.GetComponent<Walker>().isSafe(true);
					Debug.Log("trueDat");
		}else{
				g.GetComponent<Walker>().isSafe(false);
					Debug.Log("natTrue");
			
		}
		}
			Debug.Log("wuznull" + gameObjects.Length);
		}
	}
	
//	void onTriggerEnter(Collider other)
//	{
//		Debug.Log("ENTER");
//		Walker walker = other.transform.GetComponent<Walker>();
//		walker.isSafe(true);
//	}
//	void onTriggerExit(Collider other)
//	{
//		Debug.Log("EXIT");
//		Walker walker = other.transform.GetComponent<Walker>();
//		walker.isSafe(false);
//	Destroy(other.gameObject);
//	}
}
