using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShowMenu : MonoBehaviour {
	public GameObject abilityPrefab;
	//public List<ProjectileStats> abilities;
	GameObject blabla, g;
	
	// Use this for initialization
	void Start () {
		for(int i =0 ; i<4; i++){
		blabla = NGUITools.AddChild(gameObject, abilityPrefab);
		g = blabla.transform.Find("NameLabel").gameObject;
		g.GetComponent<UIAnchor>().relativeOffset.y-= 0.06f*i;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
}
