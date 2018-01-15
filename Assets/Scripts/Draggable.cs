using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {
	private Vector3 screenPoint; 
	private Vector3 offset;
	public bool BeingDragged = false;

	private float OrigY;

	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		OrigY = Input.mousePosition.y;
		//offset =  transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,screenPoint.z));
		BeingDragged = true;
	}

	void OnMouseDrag()
	{
		float cahngeY = (OrigY - Input.mousePosition.y)/20f;
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint);// + offset;
		Vector3 locedYPosition = new Vector3 (curPosition.x, 0.1f, curPosition.z-cahngeY);
		transform.position = locedYPosition;
	}

	void OnMouseUp(){
		BeingDragged = false;
		Debug.Log ("Let go");
	}
}
