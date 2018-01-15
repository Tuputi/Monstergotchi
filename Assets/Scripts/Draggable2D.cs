using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable2D : MonoBehaviour {

	bool Drag;

	void Update () {
		if (Drag)
		{
			var screenPoint = Input.mousePosition;
			screenPoint.z = 10.0f; 
			transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
		}
	}

	void OnMouseDown()
	{
		Drag = true;
	}

	void OnMouseUp()
	{
		Drag = false;
	}
}
