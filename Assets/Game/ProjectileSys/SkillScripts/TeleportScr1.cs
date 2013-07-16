using UnityEngine;
using System.Collections;
using System.Linq;

public class TeleportScr1 :MonoBehaviour, SkillScript {
	
	public void UseSkill(ProjectileStatsContainer mod_stats,PlayerMain player){
		
		float 
			dis=mod_stats.Radius,dis_extra=dis+2f;
		Vector3 
			dir=player.transform.TransformDirection(player.UpperTorsoDir),
			start_pos=player.transform.position
			;//end_pos=start_pos+dir*dis;
		
		Ray ray=new Ray(start_pos,dir);
		var hits=Physics.RaycastAll(ray,dis_extra).OrderByDescending(r=>r.distance).ToArray();
		
		float current_warp_dis=dis;
		
		bool jump=true;
		
		for (int i=0;i<hits.Length;i++){
			var hit=hits[i];
			float
				hit_dis=(new Vector2(start_pos.x,start_pos.z)-new Vector2(hit.collider.transform.position.x,hit.collider.transform.position.z)).magnitude,
				hit_rad=hit.collider.bounds.size.x/2f,
				min_dif=0.5f+hit_rad,
				dis_dif=Mathf.Abs(current_warp_dis-hit_dis),
				min_legal=0.1f;
			
			if (dis_dif<=min_legal){
				//Too close. No jump.
				jump=false;
				break;
			}else
			if (dis_dif<min_dif){
				//too close to this collider->Change current to min legal distance from collider and continue;
				current_warp_dis=hit_dis-min_dif;
			}else{
				//this pos is good use it
				break;
			}
			
			/*
			if (next_valid_jump_dis>distance){
				continue;//over legal distance.
			}else
			if (){
				continue;//under legal distance
			{
				*/
				
				
			
			/*
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
			*/
		}
		if (jump)
			player.transform.position=ray.GetPoint(current_warp_dis);
	}
}
