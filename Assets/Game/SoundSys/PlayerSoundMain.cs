using UnityEngine;
using System.Collections;

public class PlayerSoundMain : MonoBehaviour {
	
	public AudioSource walk_source,shoot_source;

	public void PlayShoot(AudioClip audio){
		shoot_source.PlayOneShot(audio);
	}
	
	public void PlayWalk(){
		if (!walk_source.isPlaying)
			walk_source.Play();
	}
	public void StopWalk(){
		walk_source.Stop();
	}
}
