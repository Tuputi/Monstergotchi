using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {
	private Vector3 screenPoint; 
	private Vector3 offset;
	private float OrigY;

	public bool BeingDragged = false;

	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset =  transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,screenPoint.z));
		BeingDragged = true;
		OrigY = Input.mousePosition.y;
	}

	void OnMouseDrag()
	{
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		float changedY = (OrigY - Input.mousePosition.y)/10f;
		Debug.Log ("Change is: " + changedY);
		Vector3 curPosChangeY = new Vector3 (curPosition.x, curPosition.y, curPosition.z + changedY);
		transform.position = curPosChangeY;
	}

	void OnMouseUp(){
		BeingDragged = false;
		Debug.Log ("Let go");
	}
}
