using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour {
	public UICamera cam_Comp;
	public UIButton start_But, opt_but, quit_butt; 
	// Use this for initialization
	void Start () {
			cam_Comp = GetComponent<UICamera>();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject selected = UICamera.selectedObject;
		if(Input.GetButtonDown("A_1") || Input.GetButtonDown("Start_1")){
			if(selected == start_But.gameObject){
			Application.LoadLevel(0);
			}
			else if(selected == opt_but.gameObject){
			
			}
			else if(selected == quit_butt.gameObject){
			
			}
			}
	}
}
