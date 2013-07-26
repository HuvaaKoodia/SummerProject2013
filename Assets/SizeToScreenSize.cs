using UnityEngine;
using System.Collections;

public class SizeToScreenSize : MonoBehaviour {

	void Start () {
		transform.localScale=new Vector3(Screen.width,Screen.height,0);
	}

}
