using UnityEngine;
using System.Collections;

public class GraphicsToRotation : MonoBehaviour,ProjectileModifier {
	
	public float spin_speed=100;
	public Vector3 additional_rotation=Vector3.zero;
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
		var dir_pos=pro.transform.position+ (pro.rigidbody.velocity);
		//pro.graphics.transform.rotation=Quaternion.Euler(pro.transform.TransformDirection(dir_pos));
		pro.graphics.transform.LookAt(dir_pos);
		pro.graphics.transform.Rotate(Vector3.forward*Time.time*spin_speed);
		//pro.graphics.transform.Rotate(additional_rotation);
		pro.graphics.transform.rotation*=Quaternion.Euler(additional_rotation);
	}
}
