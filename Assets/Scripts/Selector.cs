using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selector : MonoBehaviour {

	public UnityEvent OnClickEvent;

	void OnMouseDown(){
		if (OnClickEvent != null) {
			OnClickEvent.Invoke ();
		}
	}
}
