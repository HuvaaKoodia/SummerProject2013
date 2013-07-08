using UnityEngine;
using System.Collections;

public class DestroyScr : MonoBehaviour {

	public void OnTriggerExit(Collider other){
		
		if (other.gameObject.tag=="Player"){
			Destroy(other.gameObject);
		}
		if (other.gameObject.tag=="Projectile"){
			Destroy(other.gameObject);
		}
		if (other.gameObject.tag=="Gib"){
			Destroy(other.gameObject);
		}
	}
}
