using UnityEngine;
using System.Collections;

public class AbilityGridSys : MonoBehaviour {

	public GameObject container;
	public UICamera _Camera;
	
	public int grid_width,grid_height;
		
	public UIButton[,] Grid{get;private set;}
	
	// Use this for initialization
	void Start () {
		Grid=new UIButton[grid_width,grid_height];
		
		for (int y=0;y<Grid.GetLength(1);y++){
			for (int x=0;x<Grid.GetLength(0);x++){
				var go=NGUITools.AddChild(gameObject,container);
				Grid[x,y]=go.GetComponent<UIButton>();
			}
		}
		_Camera.selectedObjectInput=gameObject;
		
		var g=GetComponent<UIGrid>();
		g.maxPerLine=grid_width;
		g.Reposition();
		
		var gi=GetComponent<GridInput>();
		gi.Grid=Grid;
		gi._Camera=_Camera;
		gi.SelectFirst();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
