using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoint : MonoBehaviour {

	public enum ActionPointType{
		Food,
		Sleep,
		Play
	}

	public string Name;
	public ActionPointType Type;

	public void ActivateActionPoint(){
		GameManager.instance.MyCreature.MoveToActionPoint (Name);
	}
}
