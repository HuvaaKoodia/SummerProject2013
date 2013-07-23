using UnityEngine;
using System.Collections;

public class DestroyOnStop : MonoBehaviour,ProjectileModifier {

	// Update is called once per frame
	void Update () {
		if (rigidbody.velocity.magnitude<0.05f){
			Destroy(gameObject);
		}
	}

}
