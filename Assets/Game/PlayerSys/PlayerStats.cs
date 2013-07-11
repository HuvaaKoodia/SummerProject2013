using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
	
	public float 
		HP=100,
		MP=100,
		Acceleration = 10000,
		Jump_speed = 7,
		Move_speed = 1f,
	    Upper_torso_rotation_multi=2f,
		Lower_torso_rotation_multi=2f,
	    Lower_torso_rotation_deadzone=0.1f,
		MP_regen_multi=5f,
		MP_regen_add=5.5f
	;
	
	public int
		MP_regen_delay=1000
	;
	
	
	
}
