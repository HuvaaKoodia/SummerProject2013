using UnityEngine;
using System.Collections;

public class animatest1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)){
			StartCoroutine(animationStates());
		}
	}
	
	IEnumerator animationStates(){
		yield return new WaitForSeconds(5);
		animation.CrossFade("Right_shoot");
		yield return new WaitForSeconds(5);
		animation.CrossFade("Left_shoot");
		yield return new WaitForSeconds(5);
		animation.Blend("Right_shoot");
		yield return new WaitForSeconds(5);
	}
}
