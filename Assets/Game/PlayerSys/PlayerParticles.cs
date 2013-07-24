using UnityEngine;
using System.Collections;

public class PlayerParticles : MonoBehaviour {
	
	public ParticleSystem[] overheat_particles;
	public GameObject exploder,stomp_smoke;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void setOverheat(bool on,bool clear){
		foreach (var p in overheat_particles){
			//p.gameObject.SetActive(on);
			if (on)
				p.Play();
			else{
				p.Stop();
				if (clear)
					p.Clear();
			}
		}
	}
	
	public void Explode(){
		var exp=Instantiate(exploder,transform.position,Quaternion.identity) as GameObject;
		var e=exp.GetComponent<Exploder>();
		e.min_a=3;e.max_a=8;
		e.min_time=0.05f;e.max_time=0.2f;
		e.max_rad=0.6f;
	}
	
	public void STOMP(){
		Instantiate(stomp_smoke,transform.position+Vector3.down*0.45f,Quaternion.identity);
	}
}
