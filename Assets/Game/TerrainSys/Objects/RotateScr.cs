using UnityEngine;
using System.Collections;

public class RotateScr : MonoBehaviour {
	
	
	public Vector3 speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles=new Vector3(transform.eulerAngles.x+speed.x,transform.eulerAngles.y+speed.y,transform.eulerAngles.z+speed.z);
	}
}
