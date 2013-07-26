using UnityEngine;
using System.Collections;

public class LightIntensityToSize : MonoBehaviour {
	
	public Light light;
	
	float s_i,s_s;
	
	void Start(){
		s_i=light.intensity;
		s_s=transform.localScale.magnitude;
	}
	
	// Update is called once per frame
	void Update () {
		light.intensity=s_i*(transform.localScale.magnitude/s_s);
	}
}
