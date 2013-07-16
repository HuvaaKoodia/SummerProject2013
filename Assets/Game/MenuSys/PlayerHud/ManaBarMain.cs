using UnityEngine;
using System.Collections;

public class ManaBarMain : MonoBehaviour {
	
	public PlayerHudMain hud;
	public UILabel overheat;
	public UISprite overHeatMeter;
	public Color startColor, endColor, overHeatColor;
	
	// Use this for initialization
	void Start () {
	
	}
	
	bool check_timer=true;
	
	// Update is called once per frame
	void Update () {
		float fill = overHeatMeter.fillAmount;
		
		overHeatMeter.color = new Color(fill*startColor.r+(1-fill)*endColor.r,fill*startColor.g+(1-fill)*endColor.g,fill*startColor.b+(1-fill)*endColor.b);

		if (hud.playerData.Player){
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
´
			}
		}
		else{
			overheat.gameObject.SetActive(false);
		}
	}
	
	IEnumerator Blink(float time){
		
		while(true){
			overheat.gameObject.SetActive(!overheat.gameObject.activeSelf);
			yield return new WaitForSeconds(time);
		}
	}
}
