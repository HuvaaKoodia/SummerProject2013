using UnityEngine;
using System.Collections;

public class GraphicsToRotation : MonoBehaviour,ProjectileModifier {
	
	ProjectileMain pro;
	// Use this for initialization
	void Start () {
		pro=GetComponent<ProjectileMain>();
		setToRot();
	}
	
	// Update is called once per frame
	void Update () {
		setToRot();
	}
	void setToRot(){
		pro.graphics.transform.LookAt(pro.graphics.transform.position+ (pro.rigidbody.velocity));
	}
}
