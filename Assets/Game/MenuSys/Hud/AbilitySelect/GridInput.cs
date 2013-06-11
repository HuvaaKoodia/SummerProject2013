using UnityEngine;
using System.Collections.Generic;

public class GridInput : MonoBehaviour
{
	public UICamera _Camera;
	public UIButton[,] Grid;
	int s_x,s_y;
	
	void Start ()
	{
		
	}
	 
	void OnKey (KeyCode key)
	{
		if (enabled)
		{
			switch (key)
			{
			case KeyCode.LeftArrow:
					s_x=Subs.Wrap(s_x-1,0,Grid.GetLength(0));
				break;
			case KeyCode.RightArrow:
				s_x=Subs.Wrap(s_x+1,0,Grid.GetLength(0));
				break;
			case KeyCode.UpArrow:
				s_y=Subs.Wrap(s_y-1,0,Grid.GetLength(1));
				break;
			case KeyCode.DownArrow:
				s_y=Subs.Wrap(s_y+1,0,Grid.GetLength(1));
				break;
			case KeyCode.A:
				//A OK; select grid item
				break;
			case KeyCode.B:
				//NOT OK; cancel what ever ur doing
				break;
			}
		}
		
		_Camera.selectedObjectHighlight=Grid[s_x,s_y].gameObject;
	}
	
	public void SelectFirst(){
		OnKey(KeyCode.None);
	}
}
