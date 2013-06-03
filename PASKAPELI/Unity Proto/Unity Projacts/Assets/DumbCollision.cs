using UnityEngine;
using System.Collections;

public class DumbCollision : MonoBehaviour {
	
	public Fireball collidingFireball = null;
	public PlayerScript player = null, collidingPlayer = null;
	public int controller=0;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other) 
	{
		//Debug.Log("Trigger Entered " + other.GetType());
		
		//player = gameObject.GetComponent<PlayerScript>();
		
		collidingFireball = other.GetComponent<Fireball>();
		collidingPlayer = other.GetComponent<PlayerScript>();
		
		//Debug.Log("Trigger Entered " + collidingPlayer.GetType());
			
		
		
		
		if(collidingFireball != null)
		{
			Debug.Log(collidingFireball.getPlayer().getControllerNumber() + " " + controller);
			if(collidingFireball.getPlayer().getControllerNumber() != controller){
				Debug.Log("Destroyed this damn fireball and the person hit");
			Destroy(this.gameObject);
			Destroy(other.gameObject);
		}
		}
		
		if(collidingPlayer !=null)
		{
			//Debug.Log("Collided Player");
			if(collidingPlayer.getControllerNumber() != controller)
			
			{
				Debug.Log("Destroy player and ball");
			Destroy(this.gameObject);
			Destroy(collidingPlayer.gameObject);
			}else
				//Debug.Log("EXITED");
				return ;
			
		}else if(other.tag!="Ground"&&other.tag!="Lava")
		{
				Debug.Log("collides something else than ground!!!");
			Destroy(this.gameObject);
//		}else if(other.tag=="Lava"){
//			Walker scott;
//			if(other.GetComponent<Walker>()!=null){
//				scott = other.GetComponent<Walker>();}
//			else{
//				scott = this.GetComponent<Walker>();
//			}
//			if(scott!=null){
//			scott.isSafe(true);
//			}
		}
	}
	public void setControllerNumber(int i)
	{
		controller = i;
	}
}
