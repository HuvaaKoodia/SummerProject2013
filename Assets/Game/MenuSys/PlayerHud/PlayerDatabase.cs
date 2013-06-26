using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum playerState
{
	notConnected,
	connected,
	ready
}

public class PlayerDatabase : MonoBehaviour
{
	public List<playerData> players = new List<playerData> ();
	
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
				players.Add (new playerData (1));
		players.Add (new playerData (2));
		players.Add (new playerData (3));
		players.Add (new playerData (4));
		
		//DEV.temp
		players [0].color = Color.blue;
		players [1].color = Color.red;
		players [2].color = Color.green;
		players [3].color = Color.yellow;
	}
}

public class playerData
{
	public int controllerNumber;
	public playerState state = playerState.ready;
	public Color color;
	public int ResourceAmount = 100;
	
	public playerData (int controller)
	{
		controllerNumber = controller;
		color = Color.white;
	}
}
