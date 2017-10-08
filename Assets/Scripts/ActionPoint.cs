using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoint : MonoBehaviour {

	public enum ActionPointType{
		MovementTarget
	}

	public string Name;
	public ActionPointType Type;
}
