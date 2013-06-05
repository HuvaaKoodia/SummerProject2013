using UnityEngine;
using System.Collections;

public class AbilityData : MonoBehaviour {
	public string name;
	public float speed, size, damage;
	public AbilityData(string name, float speed, float size, float damage){
		this.name = name;
		this.speed = speed;
		this.size = size;
		this.damage = damage;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
