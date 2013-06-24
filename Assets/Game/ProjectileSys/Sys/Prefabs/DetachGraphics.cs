using UnityEngine;
using System.Collections;

public class DetachGraphics : MonoBehaviour {
	
	public Transform graphics;
	
	void OnDestroy(){
		graphics.transform.parent=null;
		graphics.SendMessage("StartEffect");
	}
}
