using UnityEngine;
using System.Collections;

public class Walker : MonoBehaviour {
	
    //Storing the reference to RagePixelSprite -component
    public IRagePixel ragePixel;
	 
    //enum for character state
    public enum WalkingState {Standing=0, WalkRight, WalkLeft, WalkUp,WalkDown, WalkDownRight, WalkDownLeft,WalkUpLeft,WalkUpRight};
    public enum LastState {R, L, U, D, UR, UL, DR, DL};
	public WalkingState state = WalkingState.Standing;
	public LastState lastState = LastState.D;
    //walking speed (pixels per second)
    public float walkingSpeed = 10f, r_Xaxis = 0f, r_Yaxis = 0f, castTimeMax = 0.3f, castTime = 0.3f;
	private int controllerNumber;
	public int health, maxHealth=100;
	public Fireball fireball;
	public PlayerScript player;
	public bool casting = false, castDirectionSet=false, isInSafeZone=false;
	public Transform newObject;
	public string animation="";
	Vector3 moveDirection = new Vector3();
	
	
	void Start(){
		ragePixel = GetComponent<RagePixelSprite>();
		player = GetComponent<PlayerScript>();
		controllerNumber = player.getControllerNumber();
		health=maxHealth;
	}
	
	void Update () {
		if(!isInSafeZone){
		reduceHealth(1);
		}
		//Update Right Stick
		if(!castDirectionSet){
			r_Xaxis = Input.GetAxis("R_XAxis_" + controllerNumber);
		r_Yaxis = -Input.GetAxis("R_YAxis_" + controllerNumber);
		}
		if(Input.GetButtonDown("RB_" + controllerNumber)||(Input.GetKey(KeyCode.Space) && controllerNumber==1))
		{
			
			casting = true;
		}
		
		if(!casting)
		{
        //Check the keyboard state and set the character state accordingly
        	if (Input.GetAxis("L_XAxis_"+controllerNumber) < 0)
        	{
            	state = WalkingState.WalkLeft;
        	}
       		else if (Input.GetAxis("L_XAxis_"+controllerNumber) > 0)
        	{
            state = WalkingState.WalkRight;
			}
			else
			{
            	state = WalkingState.Standing;
        	}
			if(Input.GetAxis("L_YAxis_"+controllerNumber) < 0)
			{
				if(state == WalkingState.WalkLeft)
				{
					state= WalkingState.WalkUpLeft;
				}
				else if(state == WalkingState.WalkRight)
				{
					state = WalkingState.WalkUpRight;
				}
				else
				{
            		state = WalkingState.WalkUp;
        		}
			
			}
			else if(Input.GetAxis("L_YAxis_"+controllerNumber) > 0)
        	{
				if(state == WalkingState.WalkLeft)
				{
					state= WalkingState.WalkDownLeft;
				}
				else if(state == WalkingState.WalkRight)
				{
					state=WalkingState.WalkDownRight;
				}
				else
				{
            		state = WalkingState.WalkDown;
        		}
			}
			if(state == WalkingState.Standing)
			{
				getStandAnimation(lastState);
		
        		ragePixel.PlayNamedAnimation(animation, false);
			}
			else switch (state)
    	    {   
		
			case (WalkingState.WalkLeft):
                //Flip horizontally. Our animation is drawn to walk right.
                ragePixel.SetHorizontalFlip(true);
                //PlayAnimation with forceRestart=false. If the WALK animation is already running, doesn't do anything. Otherwise restarts.
            
			ragePixel.PlayNamedAnimation("WALK R", false);
			
                //Move direction. X grows right so left is -1.
                moveDirection = new Vector3(-1f, 0f, 0f);
				lastState=LastState.L;
                break;

            case (WalkingState.WalkRight):
                //Not flipping horizontally. Our animation is drawn to walk right.
                //ragePixel.SetHorizontalFlip(false);
                //PlayAnimation with forceRestart=false. If the WALK animation is already running, doesn't do anything. Otherwise restarts.
			ragePixel.SetHorizontalFlip(false);
                ragePixel.PlayNamedAnimation("WALK R", false);
                //Move direction. X grows right so left is +1.
                moveDirection = new Vector3(1f, 0f, 0f);
				lastState=LastState.R;
                break;
			case (WalkingState.WalkUp):
			ragePixel.SetHorizontalFlip(false);
			moveDirection = new Vector3(0f,1f,0f);
			ragePixel.PlayNamedAnimation("WALK U", false);
			lastState=LastState.U;
			break;
			
			case (WalkingState.WalkDown):
			ragePixel.SetHorizontalFlip(false);
			moveDirection = new Vector3(0f,-1f,0f);
			ragePixel.PlayNamedAnimation("WALK D", false);
			lastState=LastState.D;
			break;
			
			case (WalkingState.WalkUpLeft):
			ragePixel.SetHorizontalFlip(true);
			moveDirection = new Vector3(-1f,1f,0f)/Mathf.Pow(2f,0.5f);
			ragePixel.PlayNamedAnimation("WALK UR", false);
			lastState=LastState.UL;
			break;
			
			case (WalkingState.WalkUpRight):
			ragePixel.SetHorizontalFlip(false);
			moveDirection = new Vector3(1f,1f,0f)/Mathf.Pow(2f,0.5f);
			ragePixel.PlayNamedAnimation("WALK UR", false);
			lastState=LastState.UR;
			break;
			
			case (WalkingState.WalkDownLeft):
			ragePixel.SetHorizontalFlip(true);
			moveDirection = new Vector3(-1f,-1f,0f)/Mathf.Pow(2f,0.5f);
			ragePixel.PlayNamedAnimation("WALK DR", false);
			lastState=LastState.DL;
			break;
			
			case (WalkingState.WalkDownRight):
			ragePixel.SetHorizontalFlip(false);
			moveDirection = new Vector3(1f,-1f,0f)/Mathf.Pow(2f,0.5f);
			ragePixel.PlayNamedAnimation("WALK DR", false);
			lastState=LastState.DR;
			break;
			
        	}
			
		}else if(!castDirectionSet)
		{
			if((Input.GetAxis("R_XAxis_" + controllerNumber)==0&&Input.GetAxis("R_YAxis_" + controllerNumber)==0))
			{
				//getStandAnimation(lastState);
				if(lastState == LastState.R||lastState==LastState.DR||lastState==LastState.UR)
				{
					r_Xaxis = 1f;
					
				}
				else if(lastState == LastState.L||lastState==LastState.DL||lastState==LastState.UL)
				{
					r_Xaxis = -1f;
					
				}
				if(lastState == LastState.U||lastState==LastState.UR||lastState==LastState.UL)
				{
					r_Yaxis= 1f;
				}else if(lastState == LastState.D||lastState==LastState.DR||lastState==LastState.DL)
				{
					r_Yaxis= -1f;
				}
				
				
			}
			else
			{
				lastState = getCastAngle();
			}
				getStandAnimation(lastState);
				ragePixel.PlayNamedAnimation(animation, false);
				castDirectionSet = true;
		}
		

        //Move the sprite into moveDirection at walkingSpeed pixels/sec
        //transform.Translate(moveDirection * Time.deltaTime * walkingSpeed);
		reset();
	}
	public void reduceHealth(int amount){
		health-=amount;
		if(health<=0){
			Destroy(this.gameObject);
		}
	}
	public void isSafe(bool truth){
		isInSafeZone=truth;
	}
	public void getStandAnimation(LastState last)
	{
		animation="";
		switch(lastState)
			{
				case (LastState.D):
				ragePixel.SetHorizontalFlip(false);
				animation="STAND D";
				break;
				
				case (LastState.DL):
				animation="STAND DR";
				ragePixel.SetHorizontalFlip(true);
				break;
				
				case (LastState.DR):
				ragePixel.SetHorizontalFlip(false);
				animation="STAND DR";
				break;
				
				case (LastState.L):
				ragePixel.SetHorizontalFlip(true);
				animation="STAND R";
				break;
				
				case (LastState.R):
				ragePixel.SetHorizontalFlip(false);
				animation="STAND R";
				break;
				
				case (LastState.U):
				ragePixel.SetHorizontalFlip(false);
				animation="STAND U";
				break;
				
				case (LastState.UL):
				ragePixel.SetHorizontalFlip(true);
				animation="STAND UR";
				break;
				
				case (LastState.UR):
				ragePixel.SetHorizontalFlip(false);
				animation="STAND UR";
				break;
				
				default:
				break;
			}
	}

	public float getAxisName(string input)
	{
		System.Console.WriteLine(Input.GetAxis((input + controllerNumber).ToString()));
	return	Input.GetAxis((input + controllerNumber.ToString()));
		
	}
	
	public void reset()
	{
		if(casting)
		{
		castTime -= Time.deltaTime;
		}
		if(castTime <=  0 && casting)
		{
			castDirectionSet = false;
			Vector3 newVector = new Vector3(r_Xaxis, r_Yaxis, 0f);
			if(newVector!=Vector3.zero)
			{
				newVector.Normalize();
			}
			
			var fb = Instantiate(fireball, new Vector3(transform.position.x	, transform.position.y+16, transform.position.z), Quaternion.identity)as Fireball;
			fb.setDirection(newVector);
			fb.GetComponent<DumbCollision>().setControllerNumber(controllerNumber);
			fb.setPlayer(player);
			
			castTime=castTimeMax;
			casting = false;
		}
		
		moveDirection=new Vector3();
	}
	
	
	
	public LastState getCastAngle()
	{
		
		Vector2 angleVector = new Vector2(Input.GetAxis("R_XAxis_" + controllerNumber), Input.GetAxis("R_YAxis_" + controllerNumber));
		if(Mathf.Abs(angleVector.y) <= 0.3826)
		{
			if(angleVector.x>0)
			{
			return LastState.R;
			}
			else {return LastState.L;
			}
		}
		else if(0.3826 <= Mathf.Abs(angleVector.y) && Mathf.Abs(angleVector.y) < 0.9238)
		{
			if(angleVector.y<0){
				if(angleVector.x>0)
				{
					return LastState.UR;
				}
				else 
				{
					return LastState.UL;
				}
			}else
			{
				if(angleVector.x>0)
				{
					return LastState.DR;
				}else
				{
					return LastState.DL;
				}
			}
			
		}
		else if (0.9238 <= Mathf.Abs(angleVector.y) && Mathf.Abs(angleVector.y) <= 1)
		{
			if(angleVector.y>0)
			{
			return LastState.D;
			}
			else
			{
				return LastState.U;
			}
		}
		return LastState.DR;
		
	}
	public bool isCasting(){
		return casting;
	}
}
