using UnityEngine;
using System.Collections;

public class PlayerSoundMain : MonoBehaviour {
	
	public AudioSource walk_source;
	// Use this for initialization
	void Start () {
		
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlayWalk(){
		if (!walk_source.isPlaying)
			walk_source.Play();
	}
	public void StopWalk(){
		walk_source.Stop();
	}
}
