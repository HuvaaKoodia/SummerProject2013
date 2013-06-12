using UnityEngine;
using System.Collections;

public class AbilityContainer{
	
	public Transform projectile_prefab;
	
	Timer cooldown;
	public float _cooldown_delay;
	
	public Transform ability_prefab;
	
	bool ability_ready=true;
	
	public PlayerMain player;
	
	// Use this for initialization
	public AbilityContainer(){
		cooldown=new Timer(1000,OnTimer);
		cooldown.Active=false;
		
		//projectile_prefab=Resources.Load("Projectile") as GameObject;
	}
	
	// Update is called once per frame
	void Update (){
	
	}
	
	public void UseAbility(Vector3 pos,Vector3 direction){
		if (!ability_ready) return;
		
		var ProStats=ability_prefab.GetComponent<ProjectileStats>();
		
		if (player.MP<ProStats.EnergyCost){
			return;
		}
		
		
		var abl=ability_prefab.GetComponent<AbilityMain>();
		
		var dis=Mathf.Max(ProStats.Size,0.5f)+0.1f;
		var spawn_pos=pos+direction*dis;
		//check if pos free
		var ray_hits=Physics.RaycastAll(pos,direction,dis);
		
		foreach (var hit in ray_hits){
			if (hit.collider.gameObject.tag=="Ground"){
				return;
			}
			//don't spawn a projectile at all.
		}
		
		var obj=MonoBehaviour.Instantiate(projectile_prefab,spawn_pos,Quaternion.identity) as Transform;
		
		foreach (var scr in abl.Components){
			if (scr!=null){
				obj.gameObject.AddComponent(scr.name);
			}
		}
		
		//add rigid body as the last component
		obj.gameObject.AddComponent<Rigidbody>();
		obj.rigidbody.useGravity=ProStats.Gravity_on;
		obj.rigidbody.collisionDetectionMode=CollisionDetectionMode.ContinuousDynamic;

		//set stats
		var pro=obj.GetComponent<ProjectileMain>();
		
		pro.stats=ProStats;
		
		pro.life_time.Delay=ProStats.Life_time;
		pro.life_time.Reset();
		pro.setDirection(direction,ProStats.Speed);
		pro.changeMaterialColor(ProStats.Colour);
		
		obj.localScale=Vector3.one*ProStats.Size;
		obj.rigidbody.mass=ProStats.Size*10;
		obj.rigidbody.drag=ProStats.Drag;
		obj.rigidbody.angularDrag=ProStats.Drag;
		
		_cooldown_delay=ProStats.Cooldown;
		setOnCooldown();
		
		player.MP-=ProStats.EnergyCost;
	}
	
	void setOnCooldown(){
		cooldown.Active=true;
		cooldown.Delay=_cooldown_delay;
		cooldown.Reset();
		ability_ready=false;
	}
	
	public void OnTimer(){
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