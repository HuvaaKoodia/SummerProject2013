using UnityEngine;
using System.Collections;

public class PE_AutoDestroy : MonoBehaviour {

	void Update () {
		if (particleSystem.isStopped)
			Destroy(gameObject);
	}
}
