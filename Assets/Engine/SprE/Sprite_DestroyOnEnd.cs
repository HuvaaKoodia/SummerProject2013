using UnityEngine;
using System.Collections;
 
class Sprite_DestroyOnEnd : MonoBehaviour
{   
	public Sprite_ spr;
	
    void Start(){
		spr.on_last=Destroy;
	}
	
	void Destroy(){
		Destroy(gameObject);
	}
}