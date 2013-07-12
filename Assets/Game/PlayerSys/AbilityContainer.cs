using UnityEngine;
using System.Collections;
using System.Linq;

public class AbilityContainer{
	
	public PlayerMain player;
	public AbilityItem Ability;
	
	public float _cooldown_delay;
	
	Timer cooldown;
	bool ability_ready=true;
	
	Transform ability_prefab,projectile_prefab;
	UpgradeStatContainer upgrade_stats;
	StoreSounds sfx;
	ProjectileStats ProStats;
	
	public AbilityContainer(PlayerMain player,AbilityItem ability){
		cooldown=new Timer(1000,OnTimer,true);
		cooldown.Active=false;
		
		this.player=player;
		
		Ability=ability;
		
		ability_prefab=Ability.Ability;
		upgrade_stats=Ability.Stats;
		projectile_prefab=ability_prefab.GetComponent<AbilityStats>().ProjectilePrefab;
		
		ProStats = ability_prefab.GetComponent<ProjectileStats> ();
		sfx = ability_prefab.GetComponent<StoreSounds>();
	}
	
	public bool UseAbility(Vector3 pos,Vector3 direction){
		if (!ability_ready) return false;
	
		var ProStats = ability_prefab.GetComponent<ProjectileStats> ();
		var sfx = ability_prefab.GetComponent<StoreSounds>();
		
		float cd_s=0,ec_s=0,e_cost;
		cd_s=GetUpgradeStat(upgrade_stats,UpgradeStat.Cooldown,ProStats.Cooldown_multi);
		ec_s=GetUpgradeStat(upgrade_stats,UpgradeStat.EnergyCost,ProStats.EnergyCost_multi);
		
		e_cost=Mathf.Max(1,ProStats.EnergyCost-ec_s);
		if (player.MP < e_cost) {
			return false;
		}

		if (projectile_prefab!=null){//is projectile
			PlayerMain inside_player=null;
			
			var dis = Mathf.Max (ProStats.Size, 0.2f);// + player.rigidbody.velocity.magnitude / 10
			var spawn_pos = pos + direction * dis;
			//check if pos free
			var ray_hits = Physics.RaycastAll (pos, direction, dis);
			
			foreach (var hit in ray_hits) {
				var go=hit.collider.gameObject;
				if (go.tag == "Ground"){
					return false;
					//don't spawn a projectile at all.
				}
				if (go.tag == "Player"){
					inside_player=go.GetComponent<PlayerMain>();
					break;
					//automatically destroy projectile after spawning.
				}
			}
			for (int i=0;i<ProStats.AmountOfShots;i++){
				var obj=MonoBehaviour.Instantiate(projectile_prefab,spawn_pos+(direction*dis*i),Quaternion.identity) as Transform;
				
				//copy projectile modifiers.
				foreach (ProjectileModifier scr in ability_prefab.GetComponents(typeof(ProjectileModifier))){
					var comp=obj.gameObject.AddComponent(scr.GetType());
					foreach (var f in scr.GetType().GetFields()){
						f.SetValue(comp,f.GetValue(scr));
					}
				}
				
				//add rigid body as the last component
				obj.gameObject.AddComponent<Rigidbody> ();
				obj.rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
				obj.rigidbody.useGravity=false;
		
				//calculate stats based on upgrades DEV.RELOC
				float lt_s=0,spd_s=0,pwr_s=0,kck_s=0,hp_s=0,rad_s=0,acc_s=0;
				
				lt_s=GetUpgradeStat(upgrade_stats,UpgradeStat.Lifetime,ProStats.Life_time_multi)*100;
				spd_s=GetUpgradeStat(upgrade_stats,UpgradeStat.Speed,ProStats.Speed_multi);
				pwr_s=GetUpgradeStat(upgrade_stats,UpgradeStat.Power,ProStats.Power_multi);
				kck_s=GetUpgradeStat(upgrade_stats,UpgradeStat.Knockback,ProStats.Knockback_multi);
				hp_s=GetUpgradeStat(upgrade_stats,UpgradeStat.HP,ProStats.HP_multi);
				rad_s=GetUpgradeStat(upgrade_stats,UpgradeStat.Radius,ProStats.Radius_multi);
				acc_s=GetUpgradeStat(upgrade_stats,UpgradeStat.Accuracy,ProStats.Accuracy_multi);
				
				//set stats
				var pro = obj.GetComponent<ProjectileMain> ();
				
				pro.Creator = player;
				pro.stats = ProStats;
				pro.sfx=sfx;
				pro.GravityOn=ProStats.Gravity_on;
				
				if (ProStats.Life_time < 0)
					pro.life_time.Active = false;
				else {
					pro.life_time.Delay=ProStats.Life_time+lt_s;
					pro.life_time.Reset();
				}
				
				//var q=Quaternion.Euler(new Vector3(Random.Range(-35,35),Random.Range(-35,35),Random.Range(-35,35)));
				float spread_start=0;
				/*if (ProStats.AmountOfShots>1){
					float angle_per=180f/ProStats.AmountOfShots;
					spread_start=-90+angle_per*i+angle_per*0.5f;
				}*/
				
				var spread=ProStats.Spread-ProStats.Spread*(acc_s*0.01f);
				spread*=0.5f;
				var q=Quaternion.Euler(new Vector3(0,spread_start+Random.Range(-spread,spread),0));
				
				var dir=direction;
				if (ProStats.AimDistance>0)
					dir=(player.transform.position+direction*ProStats.AimDistance)-pos;
				
				pro.setDirection(q*dir.normalized,ProStats.Speed+spd_s);
				pro.changeMaterialColor(ProStats.Colour);
				pro.Power=ProStats.Power+pwr_s;
				pro.Knockback=ProStats.Knockback+kck_s;
				pro.HP=ProStats.HP+hp_s;
				pro.Radius=ProStats.Radius+rad_s;
				
				obj.localScale=Vector3.one*ProStats.Size;
				obj.rigidbody.mass=ProStats.Size*10;
				obj.rigidbody.drag=ProStats.Drag;
				obj.rigidbody.angularDrag=ProStats.Drag;
				obj.rigidbody.mass=ProStats.Mass;
				
				if (inside_player!=null){//DEV.HAX!
					GameObject.Destroy(obj.gameObject);
					inside_player.HP-=pro.Power;
				}
			}
		}
		else{
			//use skill
			foreach (SkillScript scr in ability_prefab.GetComponents(typeof(SkillScript))){
				scr.UseSkill(player);
			}
		}
		
		_cooldown_delay = ProStats.Cooldown-cd_s;
		setOnCooldown ();
		
		player.MP -= e_cost;
		return true;
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

	public float getCooldownPercent ()
	{
		return cooldown.Tick/cooldown.Delay;
	}
}