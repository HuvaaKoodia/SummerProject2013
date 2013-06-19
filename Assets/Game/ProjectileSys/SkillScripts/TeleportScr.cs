using UnityEngine;
using System.Collections;
using System.Linq;

public class TeleportScr :MonoBehaviour, SkillScript {

	public void UseSkill(PlayerMain player){
		player.StartCoroutine(doJump(player));
	}
	
	IEnumerator doJump(PlayerMain player){
		int i=0;
		while (i<5){
			Debug.Log("JUMP! "+i);
			//yield return jump(player);
			player.StartCoroutine(Jump(player));
			yield return new WaitForSeconds(0);
			Debug.Log("JUMP DONE! "+i);
			i++;
		}
		yield return null;
	}
	
	IEnumerator Jump(PlayerMain player){
		float 
			dis=0.5f,dis_extra=dis+2f;
		Vector3 
			dir=player.transform.TransformDirection(player.LowerTorsoDir),
			start_pos=player.transform.position,
			end_pos=start_pos+dir*dis;
		
		//player.transform.position=end_pos;
		//return;
		
		//Debug.Log("COLLISION CHECK::: DIS: "+Vector3.Distance(start_pos,end_pos));
		
		var hits=Physics.RaycastAll(start_pos,dir,dis_extra).OrderBy(r=>Vector3.Distance(start_pos,end_pos)).ToArray();
		
		/*
		foreach (var h in hits){
			Debug.Log("hit dis "+h.distance);
		}*/
		
		float current_warp_dis=dis;
		Ray ray=new Ray(start_pos,dir);
		bool jump=true;
		
		for (int i=0;i<hits.Length;i++){
			var hit=hits[i];
			float
				hit_dis=(new Vector2(start_pos.x,start_pos.z)-new Vector2(hit.collider.transform.position.x,hit.collider.transform.position.z)).magnitude,
				hit_rad=hit.collider.bounds.size.x/2f,
				min_dif=0.5f+hit_rad,
				dis_dif=current_warp_dis-hit_dis;
			
			if (Mathf.Abs(dis_dif)>min_dif){
				//good pos jump
				break;
			}
			else{
				//too close change pos
				current_warp_dis=Mathf.Max (dis,hit_dis-min_dif);
				if (current_warp_dis<=0.5f){//too close no jump!
					jump=false;
					break;
				}
			}
			
			
		}
		if (jump)
			player.transform.position=ray.GetPoint(current_warp_dis);
		
		yield return null;
	}
}