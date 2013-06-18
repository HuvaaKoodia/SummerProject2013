using UnityEngine;
using System.Collections;
using System.Linq;

public class AbilityContainer{
	
	public PlayerMain player;
	public AbilityItem Ability;
	
	public float _cooldown_delay;
	
	Timer cooldown;
	bool ability_ready=true;
	
	public AbilityContainer(){
		cooldown=new Timer(1000,OnTimer);
		cooldown.Active=false;
		
		Ability=new AbilityItem();
	}
	
	public void UseAbility(Vector3 pos,Vector3 direction){
		if (!ability_ready) return;
		
		var ability_prefab=Ability.Ability;
		var upgrade_stats=Ability.Stats;
		
		var ProStats = ability_prefab.GetComponent<ProjectileStats> ();
		
		if (player.MP < ProStats.EnergyCost) {
			return;
		}
		
		var projectile_prefab=ability_prefab.GetComponent<AbilityStats>().ProjectilePrefab;
		var modifiers=ability_prefab.GetComponent<AbilityModifiers>();
		
		if (projectile_prefab!=null){//is projectile
			
			var dis = Mathf.Max (ProStats.Size, 0.5f) + 0.2f + player.rigidbody.velocity.magnitude / 10;
			var spawn_pos = pos + direction * dis;
			//check if pos free
			var ray_hits = Physics.RaycastAll (pos, direction, dis);
			
			foreach (var hit in ray_hits) {
				if (hit.collider.gameObject.tag == "Ground") {
					return;
					//don't spawn a projectile at all.
				}
			}

			var obj=MonoBehaviour.Instantiate(projectile_prefab,spawn_pos,Quaternion.identity) as Transform;
			foreach (var scr in modifiers.Components){
				if (scr!=null){
					obj.gameObject.AddComponent(scr.name);

				}
			}
			
			//add rigid body as the last component
			obj.gameObject.AddComponent<Rigidbody> ();
			obj.rigidbody.useGravity = ProStats.Gravity_on;
			obj.rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
	
			//calculate stats based on upgrades DEV.RELOC
			
			float lt_s=0,spd_s=0,pwr_s=0,kck_s=0;
			
			lt_s=GetUpgradeStat(upgrade_stats,UpgradeStat.Lifetime,ProStats.Life_time_multi);
			spd_s=GetUpgradeStat(upgrade_stats,UpgradeStat.Speed,ProStats.Speed_multi);
			pwr_s=GetUpgradeStat(upgrade_stats,UpgradeStat.Power,ProStats.Power_multi);
			kck_s=GetUpgradeStat(upgrade_stats,UpgradeStat.Knockback,ProStats.Knockback_multi);
			
			//set stats
			var pro = obj.GetComponent<ProjectileMain> ();
			
			pro.Creator = player;
			pro.stats = ProStats;
			
			if (ProStats.Life_time < 0)
				pro.life_time.Active = false;
			else {
				pro.life_time.Delay=ProStats.Life_time+lt_s;
				pro.life_time.Reset();
			}
			
			pro.setDirection(direction,ProStats.Speed+spd_s);
			pro.changeMaterialColor(ProStats.Colour);
			pro.Power=ProStats.Power+pwr_s;
			pro.Knockback=ProStats.Knockback+pwr_s;
			
			obj.localScale=Vector3.one*ProStats.Size;
			obj.rigidbody.mass=ProStats.Size*10;
			obj.rigidbody.drag=ProStats.Drag;
			obj.rigidbody.angularDrag=ProStats.Drag;
		}
		else{
			//use skill
			foreach (SkillScript scr in ability_prefab.GetComponents(typeof(SkillScript))){
				scr.UseSkill(player);
			}
		}
		
		float cd_s=0,ec_s=0;
		cd_s=GetUpgradeStat(upgrade_stats,UpgradeStat.Cooldown,ProStats.Cooldown_multi);
		ec_s=GetUpgradeStat(upgrade_stats,UpgradeStat.EnergyCost,ProStats.EnergyCost_multi);
		
		_cooldown_delay = ProStats.Cooldown-cd_s;
		setOnCooldown ();
		
		player.MP -= Mathf.Max(1,ProStats.EnergyCost-ec_s);
	}
	
	float GetUpgradeStat(UpgradeStatContainer stats,UpgradeStat stat,float multi){
		int temp=0;
		stats.Data.TryGetValue(stat,out temp);
		return temp*multi;
	}
	
	void setOnCooldown ()
	{
		cooldown.Active = true;
		cooldown.Delay = _cooldown_delay;
		cooldown.Reset ();
		ability_ready = false;
	}
	
	void OnTimer(){
		cooldown.Active=false;
		ability_ready=true;
	}
}

/*foreach (var old_c in ability_prefab.GetComponents<Component>()){
			var old_t =old_c.GetType();
			var new_c=obj.gameObject.AddComponent(old_t);
			foreach (var f in old_t.GetFields()){
				f.SetValue(new_c,f.GetValue(old_c));
			}
		}*/
		

/*
			foreach (var f in old_c.GetType().GetFields()){
				f.SetValue(new_c,f.GetValue(old_c));
			}*/