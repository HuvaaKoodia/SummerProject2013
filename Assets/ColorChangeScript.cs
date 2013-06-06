using UnityEngine;
using System.Collections;

public class ColorChangeScript : MonoBehaviour {
	public UILabel label;
	float timer=0;
	int controller=0;
	public PlayerManager manager;
	public UIButton uibutton;
	// Use this for initialization
	void Start () {
	label = GetComponent<UILabel>();
	manager=GameObject.Find("PLAYERDATAS!").GetComponent<PlayerManager>();
	uibutton = GetComponent<UIButton> ();
		int.TryParse (uibutton.name [3].ToString (), out controller);
	}
	
	// Update is called once per frame
	void Update () {	
		
		timer+=Time.deltaTime;
		Debug.Log(Mathf.Sin(timer));
		label.color = new Color(0.2f,0.2f,Mathf.Abs(Mathf.Sin(timer)));
	}
}
