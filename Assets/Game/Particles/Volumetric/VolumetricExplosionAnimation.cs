using UnityEngine;
using System.Collections;

public class VolumetricExplosionAnimation : MonoBehaviour {
	
	public bool reverse_scale=false;
	
	public float 
		max_scale=1,
		interference_multi=1f,color_fade_multi=0.000001f,graphic_fade_multi=2f,scale_multi=0.05f,speed_multi=2f;
	
	public float scale_start=0,graphic_fade_start=1;
	
	float time=0,scale_temp;
	
	
	void Start(){
		scale_temp=scale_start;
		if (reverse_scale)
			scale_temp=max_scale-0.01f;
		
		transform.localScale=Vector3.one*scale_temp;
	}
	
	void Update () {
		time+=Time.deltaTime*speed_multi;
		
		float ld=Time.time * interference_multi*speed_multi;
		float r = Mathf.Sin(ld * (2 * Mathf.PI)) * 0.5f + 0.25f;
		float g = Mathf.Sin((ld + 0.33333333f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float b = Mathf.Sin((ld + 0.66666667f) * 2 * Mathf.PI) * 0.5f + 0.25f;
		float correction = 1 / (r + g + b);
		r *= correction;
		g *= correction;
		b *= correction;
		renderer.material.SetVector("_ChannelFactor", new Vector4(r,g,b,0));
		
		float 
			t=time*color_fade_multi,
			min=Mathf.Min(0.5f,t),
			max=Mathf.Min(1,0.5f+t);
		
		renderer.material.SetVector("_Range", new Vector4(min,max,0,0));
		
		float clip=Mathf.Max(0,graphic_fade_start-time*graphic_fade_multi);
		renderer.material.SetFloat("_ClipRange",clip);
			
		if (reverse_scale)
			scale_temp-=(scale_temp-scale_start)*scale_multi*speed_multi;
		else
			scale_temp+=(max_scale-scale_temp)*scale_multi*speed_multi;

		transform.localScale=Vector3.one*scale_temp;
			
		if (clip==0||transform.localScale.magnitude<0)
			Destroy(gameObject);
	}
}
