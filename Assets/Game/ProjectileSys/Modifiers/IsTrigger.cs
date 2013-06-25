using UnityEngine;
using System.Collections;

public class IsTrigger : MonoBehaviour,ProjectileModifier {

	void Start () {
		rigidbody.isKinematic=true;
		collider.isTrigger=true;
	}
}
