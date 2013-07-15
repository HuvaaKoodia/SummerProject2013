using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public enum UpgradeStat{
	Speed,
	Accuracy,
	Power,
	Knockback,
	Cooldown,
	Radius,
	Lifetime,
	EnergyCost,
	HP
}
public class UpgradeStats : MonoBehaviour {

	public UpgradeStat[] AvailableUpgrades;
	
	public static string getUpgradeName(UpgradeStat stat){
		if (stat==UpgradeStat.Cooldown)
			return "Firerate";
		if (stat==UpgradeStat.EnergyCost)
			return "Cooling";
		
		return stat.ToString();
	}
}

public class UpgradeStatContainer{
	public Dictionary<UpgradeStat,int> Data;
	
	public UpgradeStatContainer(){
		 Clear();
	}

	public void Clear()
	{
		Data=new Dictionary<UpgradeStat, int>();
		foreach(UpgradeStat s in System.Enum.GetValues(typeof(UpgradeStat))){
			Data.Add(s,0);
		}
	}
}
/*
public class UpgradeStatData{
	public UpgradeStat stat;
	int points;
	public int Points{get{return points;} set{points=Mathf.Max(value,0,10);}}
}*/
