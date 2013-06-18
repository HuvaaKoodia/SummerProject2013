using UnityEngine;
using System.Collections;

public class ProjectileStats : MonoBehaviour {
	public Color Colour=Color.red;
	public bool Gravity_on;
	
	public float 
		Speed=1,
		Size=1,
		Drag=0,
		Life_time=1000,
		Cooldown=1000,
		Damage=1,
		Knockback=1,
		EnergyCost=1,
		Radius=1;
	
	public float 
		Speed_multi=1,
		Life_time_multi=1,
		Cooldown_multi=1,
		Damage_multi=1,
		Knockback_multi=1,
		EnergyCost_multi=1,
		Radius_multi=1;
}
