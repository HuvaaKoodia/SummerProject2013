using UnityEngine;
using System.Collections;

public class IsTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var g=transform.Find("Graphics") as Transform;
		
		rigidbody.isKinematic=true;
		collider.isTrigger=true;
		
		var c=g.renderer.material.color;
		g.renderer.material.color=new Color(c.r,c.g,c.b,0.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
