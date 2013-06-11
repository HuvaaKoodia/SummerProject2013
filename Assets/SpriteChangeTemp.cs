using UnityEngine;
using System.Collections;

public class SpriteChangeTemp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var spr=GetComponent<UISprite>();
		spr.spriteName=spr.atlas.spriteList[Random.Range(0,spr.atlas.spriteList.Count)].name;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
