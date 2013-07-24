using UnityEngine;
using System.Collections;
using NotificationSys;

public class ExplosionScr1 : MonoBehaviour
{
	public float radius, force,seconds=1f;

	// Use this for initialization
	void Start (){
		NotificationCenter.Instance.sendNotification (new Explosion_note (transform.position, force*10000, radius));
		NotificationCenter.Instance.sendNotification (new Knockback_note (transform.position, force, radius,seconds,null));
	}
	
	
}
