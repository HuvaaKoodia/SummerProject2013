using UnityEngine;
using System.Collections;

public class MenuScaler : MonoBehaviour {
	
	public float base_w=16,base_h=9;
	
	// Use this for initialization
	void Start () {
		float radnom_tar=(base_h/base_w)*Camera.main.aspect;
		var ls=transform.localScale;
		transform.localScale=new Vector3(ls.x*radnom_tar,ls.y*radnom_tar,ls.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
