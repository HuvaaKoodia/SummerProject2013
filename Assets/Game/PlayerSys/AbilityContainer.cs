using UnityEngine;
using System.Collections;
using System.Linq;

public class AbilityContainer{
	
	public PlayerMain player;
	public AbilityItem Ability;
	
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
		
		ProStats = ability_prefab.GetComponent<ProjectileStats>();
		sfx = ability_prefab.GetComponent<StoreSounds>();
	}
	
	public bool UseAbility(Vector3 pos,Vector3 direction){
		if (
			!ability_ready|| 
			player.MP >= player.stats.MP
		)return false;
		
		//DEV.DEBUG!
		ProStats = ability_prefab.GetComponent<ProjectileStats>();;
		sfx = ability_prefab.GetComponent<StoreSounds>();
		//DEV.END
		
		
		//set stats
		ProjectileStatsContainer mod_stats=new ProjectileStatsContainer();
		
		mod_stats.EnergyCost=ProStats.EnergyCost*(1-GetUpgradeStat(UpgradeStat.EnergyCost,ProStats.EnergyCost_multi));
		mod_stats.Cooldown=ProStats.Cooldown*(1-GetUpgradeStat(UpgradeStat.Cooldown,ProStats.Cooldown_multi));
		
		mod_stats.Life_time=ProStats.Life_time*(1+GetUpgradeStat(UpgradeStat.Lifetime,ProStats.Life_time_multi));
		mod_stats.Speed=ProStats.Speed*(1+GetUpgradeStat(UpgradeStat.Speed,ProStats.Speed_multi));
		mod_stats.Power=ProStats.Power*(1+GetUpgradeStat(UpgradeStat.Power,ProStats.Power_multi));
		mod_stats.Knockback=ProStats.Knockback*(1+GetUpgradeStat(UpgradeStat.Knockback,ProStats.Knockback_multi));
		mod_stats.HP=ProStats.HP*(1+GetUpgradeStat(UpgradeStat.HP,ProStats.HP_multi));
		mod_stats.Radius=ProStats.Radius*(1+GetUpgradeStat(UpgradeStat.Radius,ProStats.Radius_multi));
		
		mod_stats.Accuracy_multi=GetUpgradeStat(UpgradeStat.Accuracy,ProStats.Accuracy_multi);
		mod_stats.Spread=ProStats.Spread*mod_stats.Accuracy_multi;
		
		
		//is projectile
		if (projectile_prefab!=null){
			PlayerMain inside_player=null;
			
			var dis = Mathf.Max (ProStats.Size, 0.2f);//+player.rigidbody.velocity.magnitude/10
			var spawn_pos = pos + direction * dis;
			//check if pos free
			if (ProStats.Check_start_collisions){
				var ray_hits = Physics.RaycastAll (pos-direction*1,direction, dis+1);
				
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
				obj.rigidbody.useGravity=ProStats.Gravity_on;
				
				//set stats
				var pro = obj.GetComponent<ProjectileMain> ();
				
				pro.Creator = player;
				pro.original_stats = ProStats;
				pro.mod_stats = mod_stats;
				pro.sfx=sfx;
				
				if (ProStats.Life_time < 0)
					pro.life_time.Active = false;
				else {
					pro.life_time.Delay=mod_stats.Life_time;
					pro.life_time.Reset();
				}
				
				float spread_start=0;
				/*if (ProStats.AmountOfShots>1){
					float angle_per=180f/ProStats.AmountOfShots;
					spread_start=-90+angle_per*i+angle_per*0.5f;
				}*/
				
				mod_stats.Spread=(ProStats.Spread-ProStats.Spread*(mod_stats.Accuracy_multi))*0.5f;
				var q=Quaternion.Euler(new Vector3(0,spread_start+Random.Range(-mod_stats.Spread,mod_stats.Spread),0));
				
				var dir=direction;
				if (ProStats.AimDistance>0){
					dir=(player.transform.position+direction*ProStats.AimDistance)-pos;
					dir.y=0;
				}
				
				pro.setDirection(q*dir.normalized,mod_stats.Speed);
				pro.changeMaterialColor(ProStats.Colour);
	
				obj.localScale=Vector3.one*ProStats.Size;
				obj.rigidbody.mass=ProStats.Size*10;
				obj.rigidbody.drag=ProStats.Drag;
				obj.rigidbody.angularDrag=ProStats.Drag;
				obj.rigidbody.mass=ProStats.Mass;
				
				if (inside_player!=null){//DEV.HAX! NOT COOL!
					GameObject.Destroy(obj.gameObject);
					inside_player.HP-=pro.mod_stats.Power;
				}
			}
		}
		else{
			//use skill
			foreach (SkillScript scr in ability_prefab.GetComponents(typeof(SkillScript))){
				scr.UseSkill(mod_stats,player);
			}
		}
		
		setOnCooldown (mod_stats.Cooldown);
		
		player.MP += mod_stats.EnergyCost;
		return true;
	}
	
	float GetUpgradeStat(UpgradeStat stat,float multi){
		int temp=0;
		upgrade_stats.Data.TryGetValue(stat,out temp);
		return temp*multi*0.1f;
	}
	
	void setOnCooldown (float time)
	{
		cooldown.Active = true;
		cooldown.Delay = time;
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