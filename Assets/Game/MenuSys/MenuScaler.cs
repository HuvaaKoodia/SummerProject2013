using UnityEngine;
using System.Collections;

public class MenuScaler : MonoBehaviour {
	
	public float base_w=16,base_h=9, xMovement;
	// Use this for initialization
	void Start () {
		scaleTrix(base_w, base_h);
	}
	
	
	
	void scaleTrix(float x, float y){
		float radnom_tar=(y/x)*Camera.main.aspect;
		var ls=transform.localScale;
		transform.localScale=new Vector3(ls.x*radnom_tar,ls.y*radnom_tar,ls.z);
		
	}
}
