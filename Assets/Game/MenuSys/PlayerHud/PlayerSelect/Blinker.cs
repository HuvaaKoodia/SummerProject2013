using UnityEngine;
using System.Collections;

public class Blinker : MonoBehaviour
{
	Timer timer;
	public GameObject objectToBlink;
	public int delay;
	
	// Use this for initialization
	void Start ()
	{
		timer = new Timer (delay, Blink,false);
	}

	// Update is called once per frame
	void Update ()
	{
		timer.Update ();
	}

	void Blink(){
		objectToBlink.SetActive (!objectToBlink.activeSelf);
	}
}