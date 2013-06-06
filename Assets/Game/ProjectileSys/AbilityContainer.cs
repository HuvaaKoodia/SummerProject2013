using UnityEngine;
using System.Collections;

public class AbilityContainer{
	
	public Transform projectile_prefab;
	
	Timer cooldown;
	public float _cooldown_delay;
	
	public Transform ability_prefab;
	
	bool ability_ready=true;
	
	//DEV. stats
	/*public Color _color;
	public float _speed;
	public float _size;
	public float _drag;
	public float _life_time;*/
	
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
		
		var ProStats=ability_prefab.GetComponent<IsProjectile>();
		var abl=ability_prefab.GetComponent<AbilityMain>();
		
		var spawn_pos=pos+direction*(Mathf.Max(ProStats._size,0.5f)+0.1f);
		//check if pos free
		//if (Physics.RaycastAll(pos,)
		
		var obj=MonoBehaviour.Instantiate(projectile_prefab,spawn_pos,Quaternion.identity) as Transform;
		
		foreach (var scr in abl.Components){
			if (scr!=null)
				obj.gameObject.AddComponent(scr.name);
			
			/*
			foreach (var f in old_c.GetType().GetFields()){
				f.SetValue(new_c,f.GetValue(old_c));
			}*/
		}
		
		//add rigid body as the last component
		obj.gameObject.AddComponent<Rigidbody>();
		obj.rigidbody.useGravity=false;
		
		/*foreach (var old_c in ability_prefab.GetComponents<Component>()){
			var old_t =old_c.GetType();
			var new_c=obj.gameObject.AddComponent(old_t);
			foreach (var f in old_t.GetFields()){
				f.SetValue(new_c,f.GetValue(old_c));
			}
		}*/
		
		//set stats
		var pro=obj.GetComponent<ProjectileMain>();
		
		pro.life_time.Delay=ProStats._life_time;
		pro.life_time.Reset();
		pro.setDirection(direction,ProStats._speed);
		pro.changeMaterialColor(ProStats._color);
		
		obj.localScale=Vector3.one*ProStats._size;
		obj.rigidbody.mass=ProStats._size*10;
		obj.rigidbody.drag=ProStats._drag;
		obj.rigidbody.angularDrag=ProStats._drag;
		
		_cooldown_delay=ProStats._cooldown;
		setOnCooldown();
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
