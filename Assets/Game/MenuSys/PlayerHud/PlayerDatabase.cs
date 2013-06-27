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
		players.Add (new PlayerData (1));
		players.Add (new PlayerData (2));
		players.Add (new PlayerData (3));
		players.Add (new PlayerData (4));
		
		//DEV.temp
		players [0].color = Color.blue;
		players [1].color = Color.red;
		players [2].color = Color.green;
		players [3].color = Color.yellow;
	}
}

public class PlayerData
{
	public int controllerNumber;
	public PlayerState state = PlayerState.notConnected;
	public Color color;
	public int ResourceAmount = 100;
	
	public PlayerData (int controller)
	{
		controllerNumber = controller;
		color = Color.white;
	}
}
