using UnityEngine;
using System.Collections;
using System.Linq;

public class TeleportScr1 :MonoBehaviour, SkillScript {
	
	public float distance=3f;
	
	public void UseSkill(PlayerMain player){
		float 
			dis=distance,dis_extra=dis+2f;
		Vector3 
			dir=player.transform.TransformDirection(player.UpperTorsoDir),
			start_pos=player.transform.position,
			end_pos=start_pos+dir*dis;
		
		var hits=Physics.RaycastAll(start_pos,dir,dis_extra).OrderBy(r=>Vector3.Distance(start_pos,end_pos)).ToArray();

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
				current_warp_dis=dis_dif;
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
	}
}
