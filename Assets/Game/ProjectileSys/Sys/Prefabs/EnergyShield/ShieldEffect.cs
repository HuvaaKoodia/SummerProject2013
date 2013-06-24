using UnityEngine;
using System.Collections;

public class ShieldEffect : MonoBehaviour {
	
	void StartEffect(){
		StartCoroutine(ShrinkShield());
	}
	
	IEnumerator ShrinkShield(){
		while(true){
			transform.localScale-=Vector3.one*Time.deltaTime*10;
			if (transform.localScale.x<=0){
				Destroy(transform.gameObject);
				break;
			}
			yield return new WaitForSeconds(Time.deltaTime);
		}
	}
}
