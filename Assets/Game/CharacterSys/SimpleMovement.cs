using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class SimpleMovement : MonoBehaviour {
    public float speed = 500.0F;
	public int controllerNumber;
	//public Walker walker;
	public bool casting=false;
	
	void Start(){
		controllerNumber = GetComponent<PlayerScript>().getControllerNumber();
		//walker = GetComponent<Walker>();
		//Debug.Log(walker + " " + controllerNumber);
	}
    void Update() {
        CharacterController controller = GetComponent<CharacterController>();
        
        Vector3 forward = transform.TransformDirection(new Vector3(Input.GetAxis("L_XAxis_"+controllerNumber), 0, -Input.GetAxis("L_YAxis_"+controllerNumber)));
		if(forward != Vector3.zero){
			forward.Normalize();
		}
		/*
    	if(walker != null){
			casting = walker.isCasting();
		}
		if(!casting){
		controller.SimpleMove(forward * speed*Time.deltaTime*10);
		}*/
	}
}