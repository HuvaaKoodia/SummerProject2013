using UnityEngine;
using System.Collections;

public class PlayerGraphicsScr : MonoBehaviour {
	
	public Transform Mecha,LowerTorso,UpperTorso,LowerPelvis,ExplosionDummy;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (LowerPelvis!=null){
			var pos=LowerPelvis.position;
			pos.y-=0.5f;
			UpperTorso.position=pos;
		}
	}
	
	//DEV.TEMP
	public void setColor(Color color){
		foreach (Renderer t in Mecha.GetComponentsInChildren(renderer.GetType())){
			t.material.color=color;
		}
		
		ExplosionDummy.gameObject.SetActive(true);
		foreach (Renderer t in ExplosionDummy.GetComponentsInChildren(renderer.GetType())){
			t.material.color=color;
		}
		ExplosionDummy.gameObject.SetActive(false);
	}
	
	public void DisengageParts(){
		ExplosionDummy.gameObject.SetActive(true);
		
		foreach (Rigidbody r in ExplosionDummy.GetComponentsInChildren(rigidbody.GetType())){
			r.transform.parent=null;
		}
	}
}
