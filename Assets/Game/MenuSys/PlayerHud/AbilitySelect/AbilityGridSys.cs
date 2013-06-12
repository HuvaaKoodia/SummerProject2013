using UnityEngine;
using System.Collections;

public class AbilityGridSys : MonoBehaviour {

	public GameObject container;
	public PlayerHudMain playerPanel;
		
	public ItemContainerMain[,] Grid{get;private set;}
	
	// Use this for initialization
	void Start () {
		
		var database=GameObject.Find("GameController").GetComponent<AbilitiesDatabase>();
		var data=database.abilities;
		int d_i=0;
				
		foreach (Transform t in transform){
			var item_c=t.GetComponent<ItemContainerMain>();

			if (d_i<data.Length){
				item_c.Ability=data[d_i];
				d_i++;
			}
		}
		
		/*
		Grid=new ItemContainerMain[grid_width,grid_height];
		
		for (int y=0;y<Grid.GetLength(1);y++){
			for (int x=0;x<Grid.GetLength(0);x++){
				var go=NGUITools.AddChild(gameObject,container);
				Grid[x,y]=go.GetComponent<ItemContainerMain>();
				var item_c=go.GetComponent<ItemContainerMain>();
				if (d_i<data.Length){
					item_c.setAbility(data[d_i]);
					d_i++;
				}
			}
		}
		*/
		//var item_conts=
		//for (int i=0;i<max_items;i++){
			
		/*
		for (int i=0;i<max_items;i++){
				var go=NGUITools.AddChild(gameObject,container);
				var item_c=go.GetComponent<ItemContainerMain>();
	
				if (d_i<data.Length){
					item_c.setAbility(data[d_i]);
					d_i++;
				}
		}*/
		

		//playerPanel._Camera.selectedObjectInput=gameObject;
		
		//var g=GetComponent<UIGrid>();
//		g.maxPerLine=grid_width;
		//g.Reposition();

	}
}
