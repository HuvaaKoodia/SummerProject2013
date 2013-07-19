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
			if (sfx.onAwake != null) {
				onAwake = gameObject.AddComponent<AudioSource> ();
				onAwake.clip = sfx.onAwake;
				onAwake.Play ();
			}
			if (sfx.onAlive != null) {
				onAlive = gameObject.AddComponent<AudioSource> ();
				onAlive.clip = sfx.onAlive;
				onAlive.loop = true;
				onAlive.Play ();	
			}
			if (sfx.onCollision != null) {
				onCollision = gameObject.AddComponent<AudioSource> ();
				onCollision.clip = sfx.onCollision;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isDetached) {
			if (onDeath!= null&&!onDeath.isPlaying){
				Destroy (gameObject);
			}
		}
	}
	
	public void detach ()
	{
		if (sfx != null) {
			if (sfx.onDeath == null) {
				return;//no death sound ->don't detach
			}
			if (sfx.onDeath!= null)
			{
				onDeath = gameObject.AddComponent<AudioSource> ();
				onDeath.clip = sfx.onDeath;
				onDeath.enabled=true;
				onDeath.Play();
			} 
			
		}
		else
			return;//no sounds -> don't detach
		transform.parent = null;
		playCollisionSound();
		isDetached = true;
		onDeath.enabled=true;
		
		
		enabled=true;
		
		
	}
	public void playCollisionSound(){
		if (onCollision!=null){
			if (!onCollision.isPlaying){
				onCollision.Play ();
			}
		}
	}
}
