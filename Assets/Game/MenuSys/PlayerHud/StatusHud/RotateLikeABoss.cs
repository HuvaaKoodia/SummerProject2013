using UnityEngine;
using System.Collections;

public class RotateLikeABoss : MonoBehaviour {
	
	public float speed,cycleLenght,minimumRotationSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	transform.Rotate(Vector3.forward*Mathf.Max( (Mathf.Abs(Mathf.Sin(Time.time*cycleLenght))*speed), minimumRotationSpeed));
	}
}
