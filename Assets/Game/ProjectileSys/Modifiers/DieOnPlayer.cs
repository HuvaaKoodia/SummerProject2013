using UnityEngine;
using System.Collections;

public class DieOnPlayer : MonoBehaviour,ProjectileModifier {

	
	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag=="Player")
			Destroy(gameObject);
	}
}
