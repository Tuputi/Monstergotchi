using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

	public ActionPoint[] ActionPoints;
	public static RoomManager Instance;

	void Awake(){
		Instance = this;
	}

	public ActionPoint GetActionPoint(string name){
		for (int i = 0; i < ActionPoints.Length; i++) {
			if (ActionPoints [i].Name.Equals(name)) {
				return ActionPoints [i];
			}
		}
		Debug.Log ("ActionPoint of the type " + name + " not found!");
		return null;
	}


}
