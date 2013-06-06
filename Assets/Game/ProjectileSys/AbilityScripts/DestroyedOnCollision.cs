using UnityEngine;
using System.Collections;

public class DestroyedOnCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision other){
		Destroy(gameObject);
	}
}
