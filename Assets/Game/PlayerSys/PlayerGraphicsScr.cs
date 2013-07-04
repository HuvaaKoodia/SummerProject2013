using UnityEngine;
using System.Collections;

public class PlayerGraphicsScr : MonoBehaviour {
	
	public Transform Mecha,LowerTorso,UpperTorso,LowerPelvis,ExplosionDummy,Fullbody;
	
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
		foreach (Renderer t in Mecha.GetComponentsInChildren(typeof(Renderer))){
			t.material.color=color;
		}
		
		ExplosionDummy.gameObject.SetActive(true);
		foreach (Renderer t in ExplosionDummy.GetComponentsInChildren(typeof(Renderer))){
			t.material.color=color;
		}
		ExplosionDummy.gameObject.SetActive(false);
		
		Fullbody.gameObject.SetActive(true);
		foreach (Renderer t in Fullbody.GetComponentsInChildren(typeof(Renderer))){
			t.material.color=color;
		}
		Fullbody.gameObject.SetActive(false);
	}
	
	public void DisengageParts(){
		ExplosionDummy.gameObject.SetActive(true);
		
		foreach (Rigidbody r in ExplosionDummy.GetComponentsInChildren(typeof(Rigidbody))){
			r.transform.parent=null;
		}
	}
	
	public void setFullbody(bool on){
		Mecha.gameObject.SetActive(!on);
		Fullbody.gameObject.SetActive(on);
	}
	
	public void toggleFullbody(){
		setFullbody(!Fullbody.gameObject.activeSelf);
	}
	
	public void changeFullAnimation(string animation){
		Fullbody.animation.Play(animation);
	}

	public bool isFullbody ()
	{
		return Fullbody.gameObject.activeSelf;
	}
}
