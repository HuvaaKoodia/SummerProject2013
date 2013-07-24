using UnityEngine;
using System.Collections;

public class SizeToLifeTime : MonoBehaviour,ProjectileModifier {
	
	public float size_change_threshold=0.75f;
	ProjectileMain pro;
	Vector3 start_scale;
	
	// Use this for initialization
	void Start () {
		pro=GetComponent<ProjectileMain>();
		start_scale=transform.localScale;
	}
	
	// Update is called once per frame
	void Update (){
		float per=1f-pro.life_time.Percent;
		float scale=1f;
		if (per>=size_change_threshold)
			scale=(1-per)/(1-size_change_threshold);
		//scale=1f;
		transform.localScale=start_scale*scale;
	}
}
