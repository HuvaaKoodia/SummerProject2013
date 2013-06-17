using UnityEngine;
using System.Collections;

public class ProjectileStats : MonoBehaviour {
	public Color Colour=Color.red;
	public bool Gravity_on;
	
	public float 
		Speed,
		Size,
		Drag,
		Life_time,
		Cooldown,
		Damage,
		Knockback,
		EnergyCost,
		Radius;
	
	public float 
		Speed_multi,
		Life_time_multi,
		Cooldown_multi,
		Damage_multi,
		Knockback_multi,
		EnergyCost_multi,
		Radius_multi;
}
