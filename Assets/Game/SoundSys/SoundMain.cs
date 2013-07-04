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
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isDetached) {
			if (!onDeath.isPlaying) {
				Destroy (gameObject);
			}
		}
	}
	
	public void detach ()
	{	
		enabled=true;
		if (sfx != null) {
			if (sfx.onDeath == null||sfx.onCollision == null) {
				return;
			}else{
				onDeath = gameObject.AddComponent<AudioSource> ();
				onDeath.enabled=true;
				onDeath.clip = sfx.onDeath;
				onDeath.Play ();	
			}
		}
		transform.parent = null;
		isDetached = true;
		
	}
	public void playCollisionSound(){
		if (sfx != null) {
			if (sfx.onCollision != null) {
				
				onCollision = gameObject.AddComponent<AudioSource> ();
				onCollision.clip = sfx.onCollision;
				onCollision.Play ();	
			}
		}
	}
}
