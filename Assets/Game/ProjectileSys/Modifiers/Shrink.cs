using UnityEngine;
using System.Collections;

public class Shrink : MonoBehaviour,ProjectileModifier {
	
	public float speed_multi=0.1f;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update (){
		transform.localScale-=Vector3.one*Time.deltaTime*speed_multi;
		if (transform.localScale.x<=0)
			Destroy(gameObject);
	}
}
