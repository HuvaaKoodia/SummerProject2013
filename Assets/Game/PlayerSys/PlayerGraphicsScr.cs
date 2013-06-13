using UnityEngine;
using System.Collections;

public class PlayerGraphicsScr : MonoBehaviour {
	
	public Transform Mecha,LowerTorso,UpperTorso;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void setColor(Color color){
		foreach (Renderer t in Mecha.GetComponentsInChildren(renderer.GetType())){
			t.material.color=color;
		}
	}
	
	public void DisengageParts(){
		foreach (Rigidbody t in Mecha.GetComponentsInChildren(rigidbody.GetType())){
			t.transform.parent=null;
			t.isKinematic=false;
			var col=t.transform.Find("Collider");
			if (col!=null)
				col.collider.enabled=true;
		}
	}
}
