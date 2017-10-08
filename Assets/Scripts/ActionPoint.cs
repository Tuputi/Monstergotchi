using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPoint : MonoBehaviour {

	public enum ActionPointType{
		Food,
		Sleep
	}

	public string Name;
	public ActionPointType Type;
}
