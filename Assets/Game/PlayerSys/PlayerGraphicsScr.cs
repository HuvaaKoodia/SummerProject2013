using UnityEngine;
using System.Collections;

public class PlayerGraphicsScr : MonoBehaviour {
	
	public Transform Mecha,LowerTorso,UpperTorso;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void setColor(Color color){
		foreach (Renderer t in Mecha.transform.GetComponentsInChildren(renderer.GetType())){
			t.material.color=color;
		}
	}
}
