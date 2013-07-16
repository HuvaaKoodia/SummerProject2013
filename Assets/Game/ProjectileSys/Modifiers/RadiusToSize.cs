using UnityEngine;
using System.Collections;

public class RadiusToSize : MonoBehaviour, ProjectileModifier {
	void Start () {
		var pro_main=GetComponent<ProjectileMain>();
		transform.localScale*=pro_main.mod_stats.Radius;
	}
}
