using UnityEngine;
using System.Collections;

public class SoundMain : MonoBehaviour
{
	
	public StoreSounds sfx;
	AudioSource onAwake, onAlive, onDeath, onCollision;
	bool isDetached = false;
	// Use this for initialization
	void Start ()
	{
		//Sound sources, play onAwake if !=null
		if (sfx != null) {
			if (sfx.onAwake) {
				onAwake = gameObject.AddComponent<AudioSource> ();
				onAwake.clip = sfx.onAwake;
				onAwake.Play ();
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
			if (onAwake&&onAwake.isPlaying){
				off++;
			}
			
			if (off==0)
				Destroy (gameObject);
		}
	}
	
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
	}
	
	bool hax_collision_sound_on=false;
	public void playCollisionSound(){
		if (onCollision){
			if (!onCollision.isPlaying){
				onCollision.Play ();
				hax_collision_sound_on=true;
			}
		}
	}
}
