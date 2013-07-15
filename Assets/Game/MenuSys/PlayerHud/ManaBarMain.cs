using UnityEngine;
using System.Collections;

public class ManaBarMain : MonoBehaviour {
	
	public PlayerHudMain hud;
	public UILabel overheat;
	
	// Use this for initialization
	void Start () {
	
	}
	
	bool check_timer=true;
	
	// Update is called once per frame
	void Update () {
		if (check_timer){
			

			if (hud.playerData.Player.OVERHEAT)	{
				StartCoroutine("Blink",0.1f);
				check_timer=false;
			}
		}
		else{
			if (!hud.playerData.Player.OVERHEAT)	{
				StopCoroutine("Blink");
				overheat.gameObject.SetActive(false);
				check_timer=true;
			}
		}
	}
	
	IEnumerator Blink(float time){
		
		while(true){
			overheat.gameObject.SetActive(!overheat.gameObject.activeSelf);
			yield return new WaitForSeconds(time);
		}
	}
}
