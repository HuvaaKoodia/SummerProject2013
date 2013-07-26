using UnityEngine;
using System.Collections;

public class Enlarge : MonoBehaviour,ProjectileModifier {

	public float speed_multi=0.25f;
	
	
	// Update is called once per frame
	void Update (){
		transform.localScale+=Vector3.one*Time.deltaTime*speed_multi;
	}
}
