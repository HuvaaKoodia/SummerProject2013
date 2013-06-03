using UnityEngine;
using System.Collections;

public class ScaleFits : MonoBehaviour {
	public Material mats;
	private int counter=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	Vector3 scale = transform.localScale;
		scale=scale/40;
		transform.localScale -= scale*Time.deltaTime; 
		counter+=500;
		renderer.material.SetTextureScale("Green greens", new Vector3(counter,0, counter));
	}
}
