using UnityEngine;
using System.Collections;

public class LookAtMainCamera : MonoBehaviour {
	
	public Vector3 additional_rotation;
	Quaternion add_rot;
	// Use this for initialization
	void Start () {
		add_rot=Quaternion.Euler(additional_rotation);
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Camera.main.transform);
		transform.rotation*=add_rot;
	}
}
