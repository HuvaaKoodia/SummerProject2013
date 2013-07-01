using UnityEngine;
using System.Collections;

public class animatest : MonoBehaviour {

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
		yield return new WaitForSeconds(10);
		animation.CrossFade("shoot");
		yield return new WaitForSeconds(3);
		animation.Blend("walk");
		yield return new WaitForSeconds(3);
	}
	
	
}
