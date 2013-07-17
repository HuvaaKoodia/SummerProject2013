using UnityEngine;
using System.Collections;

public class DetachOnDestroy : MonoBehaviour {
	
	public Transform obj;
	
	void OnDestroy(){
		obj.transform.parent=null;
		obj.SendMessage("Detached");
	}
}
