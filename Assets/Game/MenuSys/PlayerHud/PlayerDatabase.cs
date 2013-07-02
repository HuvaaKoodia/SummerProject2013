using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerState
{
	notConnected,
	connected,
	ready
}

public class PlayerDatabase : MonoBehaviour
{
	public List<PlayerData> players = new List<PlayerData> ();
	public AbilitiesDatabase abilitiesDB;
	
	// Use this for initialization
	void Awake ()
	{
		if (players.Count==0)//DEV.temp if
			CreatePlayers ();
	}
	/// <summary>
	/// DEV. TEMP not public please
	/// </summary>
	public void CreatePlayers ()
	{
		players.Add(new PlayerData (abilitiesDB,1,Color.blue));
		players.Add(new PlayerData (abilitiesDB,2,Color.red));
		players.Add(new PlayerData (abilitiesDB,3,Color.green));
		players.Add(new PlayerData (abilitiesDB,4,Color.yellow));
		
		//DEV.temp
		/*players [0].color = Color.blue;
		players [1].color = Color.red;
		players [2].color = Color.green;
		players [3].color = Color.yellow;
		*/
	}
}

public class PlayerData
{
	public PlayerMain Player;
	public int controllerNumber;
	public PlayerState state = PlayerState.notConnected;
	public Color color;
	public int ResourceAmount = 100;
	public List<AbilityItem> Abilities = new List<AbilityItem>();
	
	public PlayerData (AbilitiesDatabase abilitiesDB,int controller,Color color)
	{
		controllerNumber = controller;
		this.color=color;
	
		for (int i=0;i<Mathf.Min(4,abilitiesDB.abilities.Length);i++){
			Abilities.Add(new AbilityItem(abilitiesDB.abilities[i]));
		}
	}
}
