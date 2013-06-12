using UnityEngine;
using System.Collections;

public class DieOnCollisionPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag=="Player")
			Destroy(gameObject);
	}
}
