using UnityEngine;
using System.Collections;

public class VolumetricExplosionAnimation : MonoBehaviour {
	
	public float loopduration=1f,fade_speed=0.000001f,clip_multi=2f,scale_multi=0.05f,time_multi=2f;
	
	float time=0;
	
	void Start(){
		transform.localScale=Vector3.one*0.01f;
	}
	
	void Update () {
		time+=Time.deltaTime*time_multi;
		
		float ld=Time.time * loopduration*time_multi;
		float r = Mathf.Sin(ld * (2 * Mathf.PI)) * 0.5f + 0.25f;
		float g = Mathf.Sin((ld + 0.33333333f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float b = Mathf.Sin((ld + 0.66666667f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float correction = 1 / (r + g + b);
		r *= correction;
		g *= correction;
		b *= correction;
		renderer.material.SetVector("_ChannelFactor", new Vector4(r,g,b,0));
		
		float 
			t=time*fade_speed,
			min=Mathf.Min(0.5f,t),
			max=Mathf.Min(1,0.5f+t);
		
		renderer.material.SetVector("_Range", new Vector4(min,max,0,0));
		
		float clip=Mathf.Max(0,1-time*clip_multi);
		renderer.material.SetFloat("_ClipRange",clip);
		
		float scale=((time*scale_multi));
		transform.localScale=(Vector3.one*scale);
		
		if (clip==0||transform.localScale.magnitude<0)
			Destroy(gameObject);
	}
}
