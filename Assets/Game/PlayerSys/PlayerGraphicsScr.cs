using UnityEngine;
using System.Collections;

public class PlayerGraphicsScr : MonoBehaviour {
	
	public Transform Mecha,LowerTorso,UpperTorso,LowerPelvis;
	
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
	
	public void setColor(Color color){
		foreach (Renderer t in Mecha.GetComponentsInChildren(renderer.GetType())){
			t.material.color=color;
		}
	}
	
	public void DisengageParts(){
		foreach (Rigidbody r in Mecha.GetComponentsInChildren(rigidbody.GetType())){
			r.transform.parent=null;
			r.isKinematic=false;
			foreach (Transform t in r.transform){
				var col=t.collider;
				if (col!=null)
					col.collider.enabled=true;
				}
		}
	}
}
