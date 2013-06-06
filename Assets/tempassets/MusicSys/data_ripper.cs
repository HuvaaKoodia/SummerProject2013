using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class data_ripper : MonoBehaviour {
	
	public AudioClip clip;
	
	List<float[]> samples;
	List<Transform> pilars;
	
	// Use this for initialization
	void Start () {
		
		samples=new List<float[]>();
		
		
		Debug.Log("Startyh");
		
		//float[] samps=new float[256];
		StartCoroutine(coroutine());
		Debug.Log("Endyh");
			/*			
		float[] samps=new float[256];
		
		for (int i=0;i<amount;i+=amount/1000){
			clip.GetData(samps,i);
			samples.Add(samps);
		}
		Vector3 pos=Vector3.zero;
		foreach (var a in samples){
			foreach (var f in a){
				var cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				
				cube.transform.localScale=new Vector3(1,1+f*2,1);
				cube.transform.position=pos;
				pos.x+=2;
			}	
			pos.x=0;
			pos.z+=2;
		}
		*/
	}
	
	IEnumerator coroutine(){
		int amount=clip.samples;
		float[] samps=new float[256];
		
		for (int i=0;i<amount;i+=amount/10){
			clip.GetData(samps,i);
			samples.Add(samps);
		}
		Vector3 pos=Vector3.zero;
		foreach (var a in samples){
			foreach (var f in a){
				var cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
				
				cube.transform.localScale=new Vector3(1,1+f*2,1);
				cube.transform.position=pos;
				pos.x+=2;
			}	
			pos.x=0;
			pos.z+=2;
		}
		return null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
