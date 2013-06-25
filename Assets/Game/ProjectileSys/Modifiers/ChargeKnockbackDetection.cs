using UnityEngine;
using System.Collections;

public class ChargeKnockbackDetection : MonoBehaviour,ProjectileModifier {
	PlayerMain player;
	// Use this for initialization
	void Start () {
	player=GetComponent<ProjectileMain>().Creator;	
	}
	
	// Update is called once per frame
	void Update () {
	transform.position = player.transform.position;
	}
	
	void OnTriggerEnter(Collider other){
		Debug.Log("KNOCK KNOCK!");
	if(other.gameObject.tag=="Player" && other.gameObject != player.gameObject){
			Vector3 heading = other.gameObject.transform.position - player.transform.position;
			heading.y=0f;
			heading=heading.normalized;
		other.gameObject.rigidbody.AddForce(heading*10000+new Vector3(0f,5f,0f));
		}
	}
}
