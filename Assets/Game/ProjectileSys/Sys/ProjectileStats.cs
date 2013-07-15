using UnityEngine;
using System.Collections;

public class ProjectileStats : MonoBehaviour {
	public Color Colour=Color.red;
	
	public int AmountOfShots=1;
	
	public bool Gravity_on=false,DoubleBarreledFunTime=true;
	
	public float 
		Size=1,
		Mass=1,
		Drag=0,
		AimDistance=0,
		Spread=0,
			
		Speed=1,
		Life_time=1000,
		Cooldown=1000,
		Power=1,
		Knockback=1,
		EnergyCost=1,
		Radius=1,
		HP=-1
		;
	
	public float 
		Accuracy_multi=1,
		Speed_multi=1,
		Life_time_multi=1,
		Cooldown_multi=1,
		Power_multi=1,
		Knockback_multi=1,
		EnergyCost_multi=1,
		Radius_multi=1,
		HP_multi
		;
}
