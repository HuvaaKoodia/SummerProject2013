using UnityEngine;
using System.Collections;

public class Exploder : MonoBehaviour
{
	
	public GameObject explosion_particle;
	public int min_a = 5, max_a = 10;
	public float max_rad = 1, min_time = 0.1f, max_time = 0.3f;
	// Use this for initialization
	void Start ()
	{
		StartCoroutine (explode ());
	}
	
	IEnumerator explode ()
	{
		for (int i=0; i<Random.Range(min_a,max_a); i++) {
			{
				var p = Instantiate (explosion_particle,
					transform.position + new Vector3 (Random.Range (-max_rad, max_rad), Random.Range (-max_rad, max_rad), Random.Range (-max_rad, max_rad)),
					Quaternion.identity 
				) as GameObject;
				var ve = p.GetComponent<VolumetricExplosionAnimation> ();
				
				ve.speed_multi += Random.Range (0, 2f);
				ve.max_scale=Random.Range (1f, 2f);
				ve.color_fade_multi*=Random.Range (0.9f,1.1f);
				ve.graphic_fade_multi*=Random.Range (0.9f,1.1f);
				ve.interference_multi*=Random.Range (0.9f,1.1f);
				ve.scale_multi*=Random.Range (0.9f,1.1f);
				
				yield return new WaitForSeconds(Random.Range(min_time,max_time));
			}
		}
		Destroy(gameObject);
	}
}
