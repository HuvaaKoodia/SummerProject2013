using UnityEngine;
using System.Collections;

public class AbilityContainer{
	
	public Transform projectile_prefab;
	
	Timer cooldown;
	public float _cooldown_delay;
	
	bool ability_ready=true;
	
	//DEV. stats
	public Color _color;
	public float _speed;
	public float _size;
	public float _drag;
	public float _life_time;
	
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
		
		var obj=MonoBehaviour.Instantiate(projectile_prefab,pos+direction*Mathf.Max(_size,0.5f),Quaternion.identity) as Transform;
		var pro=obj.GetComponent<ProjectileMain>();
		
		pro.life_time.Delay=_life_time;
		pro.life_time.Reset();
		pro.setDirection(direction,_speed);
		pro.changeMaterialColor(_color);
		
		obj.localScale=Vector3.one*_size;
		obj.rigidbody.mass=_size*10;
		obj.rigidbody.drag=_drag;
	
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
