using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timer{
	
	//class
	public delegate void TimerEvent();
	public static List<Timer> timers=new List<Timer>(),timers_destroyed=new List<Timer>();
	
	public static void UpdateTimers () {
		foreach (var t in timers){
			t.Update();
			if (t.Destroyed)
				timers_destroyed.Add(t);
		}
		if (timers_destroyed.Count>0){
			foreach (var t in timers_destroyed){
				timers.Remove(t);
			}
			timers_destroyed.Clear();
		}
	}
	
	public static void clearTimers(){
		timers.Clear();
	}
	
	//object
	public bool Active=false;
	public float Delay{get{return time;} set{time=value/1000;}}
	public bool Destroyed{get;private set;}
	
	float time,tick;
	public TimerEvent Timer_Event{get;set;}
	
	/// <summary>
	/// Creates an inactive timer.
	/// </param>
	public Timer(TimerEvent te){
		timers.Add(this);
		Timer_Event=te;
	}
	/// <summary>
	/// Creates an active timer with a certain delay.
	/// </param>
	public Timer(int millis,TimerEvent te):this(te){
		time=tick=millis/1000f;
		Active=true;
	}
	
	public void Update(){
		if (!Active) return;
		tick-=Time.deltaTime;
		
		if (tick<=0){
			if (Timer_Event!=null)
				Timer_Event();
			Reset();
		}
	}
	
	//subs
	
	public void Reset(){
		tick=time;
	}
	
	public void Destroy(){
		Destroyed=true;
	}
}
