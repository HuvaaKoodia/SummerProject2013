using UnityEngine;
using System.Collections;

public class MenuPanelXSupeHyper : MonoBehaviour {
	
	public float base_w=16,base_h=9, xMovement;
	// Use this for initialization
	void Start () {
		scaleTrix(base_w, base_h);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void scaleTrix(float x, float y){
		float radnom_tar=(y/x)*Camera.main.aspect;
		var vektorimuuttuja=transform.localPosition;
		vektorimuuttuja.x= 330*Mathf.Sign(vektorimuuttuja.x)*radnom_tar;
		transform.localPosition = vektorimuuttuja;
		
	}
}
