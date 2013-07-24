using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {
	
	AudioSource sfx;
	public AudioClip menuSelect, menuCancel, menuMove;
	
	// Use this for initialization
	void Start () {
	sfx = gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void playCancel(){
		sfx.clip=menuCancel;
		sfx.Play();
	}
	public void playSelect(){
		sfx.clip=menuSelect;
		sfx.Play();
	}
	public void playMove(){
		sfx.clip=menuMove;
		sfx.Play();
	}
}
