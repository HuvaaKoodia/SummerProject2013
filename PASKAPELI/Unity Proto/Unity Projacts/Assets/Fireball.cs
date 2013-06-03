using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {
	
	public PlayerScript player;
	public bool collided = false, isntNull=false; 
	
	private IRagePixel ragePixel;
	public float projectileSpeed = 500f;
	public Vector3 moveDirection = new Vector3(Mathf.Cos(0f), Mathf.Sin(0f), 0f);
	public float rotation =0;
	//public float angle = 0f;
	public float timer = 10f;
	public Transform parentTrans;
	
	
	void Start () 
	{
		//Load GFX
		ragePixel = GetComponent<RagePixelSprite>();
		
		
		transform.Rotate(Vector3.up,rotation);
		
	}
	
	// Update is called once per frame
	void Update () {
		//ragePixel.PlayNamedAnimation("BURN", false);
		//Timer for expiring
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			Destroy(this.gameObject);
		}
		
		//Movement
		transform.Translate(new Vector3(1,0,1) * Time.deltaTime * projectileSpeed);
		
		
	}
	
	public float angleRotation=0f;
	//public Flying_ball flyingBall;
	
	
    
	public void setPlayer(PlayerScript daddy)
	{
		player = daddy;
	}
	public PlayerScript getPlayer()
	{
		return player;
	}
	public void setDirection(Vector3 direction)
	{
		rotation=0;
		
			rotation = (Mathf.Atan2(-direction.y, direction.x));
		
		rotation*=360/(2*Mathf.PI);
		
		rotation+=45;
	}
}