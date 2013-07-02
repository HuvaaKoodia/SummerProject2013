using UnityEngine;
using System.Collections;
using NotificationSys;

public class ExplosionScr : MonoBehaviour
{
	public float radius, force;

	// Use this for initialization
	void Start (){
		NotificationCenter.Instance.sendNotification (new Explosion_note (transform.position, force, radius));
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (particleSystem.isStopped) {
			Destroy (gameObject);
		}
	}
	
}
