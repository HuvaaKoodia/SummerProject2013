using UnityEngine;
using System.Collections;

public class IsTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		rigidbody.isKinematic=true;
		collider.isTrigger=true;
		
		
		
		//DEV. move to Graphics mod
		var g=transform.Find("Graphics") as Transform;
		var c=g.renderer.material.color;
		g.renderer.material.color=new Color(c.r,c.g,c.b,0.5f);
	}
	
	// Update is called once per frame
	void Update (){
	
	}
}
