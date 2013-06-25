using UnityEngine;
using System.Collections;

public class SoundMain : MonoBehaviour {
	
	public StoreSounds sfx;
	AudioSource onAwake, onAlive, onDeath;
	// Use this for initialization
	void Start () {
	//Sound sources, play onAwake if !=null
		if(sfx!=null){
			if(sfx.onAwake!=null){
				onAwake = gameObject.AddComponent<AudioSource>();
				onAwake.clip=sfx.onAwake;
				onAwake.volume=2.5f;
				onAwake.Play();
			}
			if(sfx.onAlive!=null){
			onAlive = gameObject.AddComponent<AudioSource>();
				onAlive.clip=sfx.onAlive;
				onAlive.loop=true;
				onAlive.volume=1f;
				onAlive.Play();	
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnDestroy(){
		
	}
}
