using UnityEngine;
using System.Collections;

public class FollowThatHP : MonoBehaviour
{
	public UISprite hpToFollow;
	public float fill, delay, catchUpSpeed;
	public	float timer, decayTimer = 0f;
	public bool delayActive = false, decayActive = false;
	UISprite sprite;
	// Use this for initialization
	void Start ()
	{
		sprite = GetComponent<UISprite> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if(hpToFollow.fillAmount>sprite.fillAmount){
			sprite.fillAmount=hpToFollow.fillAmount;
			timer=delay;
			delayActive=true;
			decayActive=false;
			decayTimer=0f;
			return ;
		}
		if (timer <= 0) {
			if (!decayActive) {
				decayActive = true;
				decayTimer = 0f;
			}
			delayActive = false;
		}
		
		if (delayActive) {
			timer -= Time.deltaTime;
		} else {
			if (decayActive){
				decayTimer += Time.deltaTime;
				sprite.fillAmount -= (catchUpSpeed * (1 + decayTimer) * Time.deltaTime);
				
				if (hpToFollow.fillAmount==0){
					if (sprite.fillAmount < 0)
						sprite.fillAmount = 0;
				} else if (sprite.fillAmount < hpToFollow.fillAmount + fill) {
					
					sprite.fillAmount = hpToFollow.fillAmount + fill;
				}
			}
		}
	}
}
