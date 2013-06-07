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
	uibutton = gameObject.transform.parent.GetComponent<UIButton> ();
		int.TryParse (uibutton.name [3].ToString (), out controller);
		NGUITools.SetActive(gameObject, false);	
	}
	
	// Update is called once per frame
	void Update () {
		
		//Debug.Log("State: " + manager.players[controller-1].state + " " + PlayerManager.playerState.ready);
		if(manager.players[controller-1].state == PlayerManager.playerState.ready)
		{
		timer+=Time.deltaTime;
		//Debug.Log(Mathf.Sin(timer));
			float temp = Mathf.Abs(Mathf.Sin(timer));
			
		label.color = new Color(uibutton.pressed.r/256*temp,uibutton.pressed.g/256*temp,uibutton.pressed.b/256*temp);
		}
	}
}
