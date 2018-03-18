using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable2D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject objectBeingDragged;
	Vector3 startPosition;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		objectBeingDragged = gameObject;
		startPosition = transform.position;
		objectBeingDragged.GetComponent<InteractionObject> ().SetActiveStatus (true);
	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		objectBeingDragged.GetComponent<InteractionObject> ().SetActiveStatus (false);
		objectBeingDragged = null;
		transform.position = startPosition;
	}

	#endregion
}
