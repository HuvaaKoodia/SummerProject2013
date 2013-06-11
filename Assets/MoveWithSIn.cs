using UnityEngine;
using System.Collections;

public class MoveWithSIn : MonoBehaviour {
	public bool isRightSide;
	UIAnchor anchor;
	public float maxMoveAmount;
	float timer;
	// Use this for initialization
	void Start () {
	maxMoveAmount=.1f;
		timer=0;
		}
	
	// Update is called once per frame
	void Update () {
		timer+=Time.deltaTime;
		anchor = GetComponent<UIAnchor>();
		if(isRightSide){
			anchor.relativeOffset.x= maxMoveAmount + maxMoveAmount*Mathf.Cos(timer*3.5f);
		}else
			anchor.relativeOffset.x = -maxMoveAmount - maxMoveAmount*Mathf.Cos(timer*3.5f);
			
	}
}
