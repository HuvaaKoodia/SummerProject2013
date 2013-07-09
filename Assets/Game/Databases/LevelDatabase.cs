using UnityEngine;
using System.Collections;

public class LevelDatabase : MonoBehaviour {

	public Transform[] levels;

	public LevelStats getLevel(string prefab_name){
		foreach (var l in levels){
			if (l.name==prefab_name){
				return l.gameObject.GetComponent<LevelStats>();
			}
		}
		return null;
	}
}
