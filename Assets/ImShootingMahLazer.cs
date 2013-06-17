using UnityEngine;
using System.Collections;

public class ImShootingMahLazer : MonoBehaviour {
	ProjectileMain pro_main;
	PlayerMain plr_main;
	
	// Use this for initialization
	void Start () {
		pro_main=GetComponent<ProjectileMain>();
		plr_main = pro_main.Creator;
	}
	
	// Update is called once per frame
	void Update () {
	//Physics.Raycast(plr_main.transform.position, plr_main.);
	}
}
