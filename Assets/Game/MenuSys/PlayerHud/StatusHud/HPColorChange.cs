using UnityEngine;
using System.Collections;

public class HPColorChange : MonoBehaviour {
	public Color colorStart, colorEnd;
	public UISprite hpGauge;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float fill=hpGauge.fillAmount;
		hpGauge.color=new Color(fill*colorStart.r+(1-fill)*colorEnd.r,fill*colorStart.g+(1-fill)*colorEnd.g,fill*colorStart.b+(1-fill)*colorEnd.b);
	}
}
