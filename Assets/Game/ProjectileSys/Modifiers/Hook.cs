using UnityEngine;
using System.Collections;

public class Hook : MonoBehaviour,ProjectileModifier
{
	ProjectileMain pro_main;
	PlayerMain plr_main;
	RaycastHit hitInfo;
	LineRenderer line_rend;
	Vector3 direction, startPos;
	float distance = 0f;
	bool collided = false;
	Transform collidedPlayer;
	// Use this for initialization
	void Start ()
	{
		pro_main = GetComponent<ProjectileMain> ();
		plr_main = pro_main.Creator;
		line_rend = gameObject.GetComponentInChildren<LineRenderer> ();	
		direction = plr_main.UpperTorsoDir;
		startPos = plr_main.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log(collided);
		
			
		line_rend.SetPosition (0, plr_main.transform.position + new Vector3 (0f, 0f, 0f));
		
		if (!collided) {
			distance += Time.deltaTime * pro_main.mod_stats.Speed;
			Physics.Raycast (new Ray (startPos, direction), out hitInfo, distance);
			line_rend.SetPosition (1, startPos + direction * distance); 
			if (hitInfo.collider != null) {
			
				
				if (hitInfo.collider.gameObject.tag == "Player") {
				if(gameObject!=hitInfo.collider.gameObject){
					collidedPlayer = collider.gameObject.transform;
					collided= true;
					}
					}
				else{
				collided = true;
				}
				line_rend.SetPosition (1, hitInfo.point);
			
			}
		} else {
			if (collidedPlayer!=null) {
				line_rend.SetPosition (1, collidedPlayer.transform.position);
			} else {
				line_rend.SetPosition (1, hitInfo.point);
			}
		}
	}

	void OnDestroy ()
	{
		Debug.Log ("ONDESTROY!");
		Vector3 heading;
		if (collidedPlayer != null) {
			//Debug.Log(collidedPlayer.position);
		}
		
		if (collided) {
			//Debug.Log(hitInfo.point);
			
			if (collidedPlayer != null) {
				heading = plr_main.transform.position - collidedPlayer.transform.position;
				heading.y = 0f;
				Debug.Log ("Heading: " + heading);
				
				
				collidedPlayer.gameObject.rigidbody.AddForce (heading.normalized * 10000f);
				plr_main.rigidbody.AddForce (-heading.normalized * 10000f);
				
			} else {
				
			}
		}
	}
}
