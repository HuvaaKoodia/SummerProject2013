using UnityEngine;
using System.Collections.Generic;

public delegate void ButtonAction();

public class GridInput : MonoBehaviour
{
	public UICamera _Camera;
	public Transform[,] Grid;
	public int grid_width,grid_height;
	public bool ForwardToItem,WrapToNextLineHorizontal;
	int s_x,s_y;
	public SoundPlayer sfxr;
	
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
				sfxr.playMove();
					addX(-1);
					if (WrapToNextLineHorizontal&&s_x==Grid.GetLength(0)-1){
						addY(-1);
					}
				break;
			case KeyCode.RightArrow:
				sfxr.playMove();
				addX(1);
				if (WrapToNextLineHorizontal&&s_x==0){
					addY(1);
				}
				break;
			case KeyCode.UpArrow:
				sfxr.playMove();
				addY(-1);
				break;
			case KeyCode.DownArrow:
				sfxr.playMove();
				addY(1);
				break;
			case KeyCode.A:
				sfxr.playSelect();
				if (AcceptEvent!=null)
					AcceptEvent();
				break;
			case KeyCode.B:
				sfxr.playCancel();
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
	
	void addX(int a){
		s_x=Subs.Wrap(s_x+a,0,Grid.GetLength(0));
		
	}
	
	void addY(int a){
		s_y=Subs.Wrap(s_y+a,0,Grid.GetLength(1));
		
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
