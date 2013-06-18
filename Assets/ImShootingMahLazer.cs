using UnityEngine;
using System.Collections;

public class ImShootingMahLazer : MonoBehaviour {
	ProjectileMain pro_main;
	PlayerMain plr_main;
	RaycastHit hitInfo;
	LineRenderer line_rend;
	
	// Use this for initialization
	void Start () {
		pro_main=GetComponent<ProjectileMain>();
		plr_main = pro_main.Creator;
		line_rend=gameObject.GetComponentInChildren<LineRenderer>();	
	}
	
	// Update is called once per frame
	void Update () {
	Physics.Raycast(new Ray(plr_main.transform.position,plr_main.UpperTorsoDir),out hitInfo , 10f);
		
		line_rend.SetPosition(0, plr_main.transform.position+new Vector3(0f,0.5f,0f));
		if(hitInfo.collider!=null){
			line_rend.SetPosition(1, hitInfo.point);
		}
		else{
			line_rend.SetPosition(1, plr_main.transform.position+(plr_main.UpperTorsoDir.normalized*10));
		}
			
	}
}
