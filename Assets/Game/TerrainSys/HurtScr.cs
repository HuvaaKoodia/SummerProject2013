using UnityEngine;
using System.Collections;

public class HurtScr : MonoBehaviour {

	public float hurt_multi;
	
	public void OnCollisionStay(Collision other){
		
		if (other.gameObject.tag=="Player"){
			var p=other.gameObject.GetComponent<PlayerMain>();
			p.HP-=Time.deltaTime*hurt_multi;
		}
		if (other.gameObject.tag=="Projectile"){
			Destroy(other.gameObject);
		}
		if (other.gameObject.tag=="Gib"){
			Destroy(other.gameObject);
		}
	}
}
