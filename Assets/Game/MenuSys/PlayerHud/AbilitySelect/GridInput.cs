using UnityEngine;
using System.Collections.Generic;

public delegate void ButtonAction();

public class GridInput : MonoBehaviour
{
	public UICamera _Camera;
	public Transform[,] Grid;
	public int grid_width,grid_height;
	public bool ForwardToItem;
	
	int s_x,s_y;
	public ButtonAction AcceptEvent,CancelEvent;
	
	void Start ()
	{
		UpdateGrid();
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
				if (AcceptEvent!=null)
					AcceptEvent();
				break;
			case KeyCode.B:
				if (CancelEvent!=null)
					CancelEvent();
				break;
			}
		}
		
		_Camera.selectedObjectHighlight=Grid[s_x,s_y].gameObject;
		if (ForwardToItem){
			Grid[s_x,s_y].SendMessage("OnKey",key,SendMessageOptions.DontRequireReceiver);
		}
	}
	
	public void HighlightCurrent(){
		OnKey(KeyCode.None);
	}
	
	public Transform SelectedItem(){
		return Grid[s_x,s_y];
	}

	public void UpdateGrid()
	{
		Grid=new Transform[grid_width,grid_height];
		List<Transform> list = new List<Transform>();

		foreach (Transform t in transform)
		{
			list.Add(t);
		}
		
		int i=0;
		for (int y=0;y<grid_height;y++){
			for (int x=0;x<grid_width;x++){
				if (i<list.Count){
					Grid[x,y]=list[i];
					i++;
				}
				else
					Grid[x,y]=null;
			}
		}
	}
	
	public void ClearGrid(){
		foreach (var i in Grid){
			if (i!=null){
				DestroyImmediate(i.gameObject);
			}
		}
		s_x=s_y=0;
		Grid=new Transform[grid_width,grid_height];
	}

	public void ChangeSelectedItem (Transform swap_item)
	{
		Grid[s_x,s_y]=swap_item;
	}
}
