using UnityEngine;
using System.Collections;

public class SoundMain : MonoBehaviour
{
	
	public StoreSounds sfx;
	AudioSource onAwake, onAlive, onDeath, onCollision;
	bool isDetached = false;
	bool hax_collision_sound_on=false, hax_awake_sound_on=true;
	// Use this for initialization
	void Start ()
	{
		//Sound sources, play onAwake if !=null
		if (sfx != null) {
			if (sfx.onAwake) {
				//onAwake = gameObject.AddComponent<AudioSource> ();
				//onAwake.clip = sfx.onAwake;
				//onAwake.playOnAwake=false;
				//onAwake.Play ();
			}
			if (sfx.onAlive) {
				onAlive = gameObject.AddComponent<AudioSource> ();
				onAlive.clip = sfx.onAlive;
				onAlive.loop = true;
				onAlive.Play ();
			}
			if (sfx.onCollision) {
				onCollision = gameObject.AddComponent<AudioSource> ();
				onCollision.clip = sfx.onCollision;
				onCollision.playOnAwake=false;
			}
			
			if (sfx.onDeath){
				onDeath = gameObject.AddComponent<AudioSource> ();
				onDeath.clip = sfx.onDeath;
				onDeath.enabled=true;
				onDeath.playOnAwake=false;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isDetached){
			int off=0;
			if (onDeath&&onDeath.isPlaying){
				off++;
			}
			if (onCollision&&(onCollision.isPlaying||hax_collision_sound_on)){
				off++;
				hax_collision_sound_on=false;
			}
			if (onAwake&&(onAwake.isPlaying||hax_awake_sound_on)){
				off++;
				
				if (hax_awake_sound_on){
					//onAwake.Play();
					//onAwake.timeSamples=awake_time;
				}
				
				hax_awake_sound_on=false;
			}
			
			if (off==0)
				Destroy (gameObject);
		}
		else{
			if (onAwake){
				//awake_time=onAwake.timeSamples;
			}
		}
	}
	int awake_time=0;
	public void detach ()
	{

		
		transform.parent = null;
		isDetached = true;

		enabled=true;
		
		if (onDeath){
			onDeath.enabled=true;
			onDeath.Play();
		}
		
		if (onCollision&&hax_collision_sound_on){
			onCollision.enabled=true;
			onCollision.Play();
		}
		
		if (onAwake){
			onAwake.enabled=true;
		}

	}
	
	
	public void playCollisionSound(){
		if (onCollision){
			if (!onCollision.isPlaying){
				onCollision.Play ();
				hax_collision_sound_on=true;
			}
		}
	}
}
