using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public string name;
	public bool active;
	public int effectValue = 10;
	public 

	Ray ray;
	RaycastHit hit;

	//what happens when the creature is given this
	public void Interact(){
		Debug.Log ("Omnomnom");
		GameManager.instance.MyCreature.Feed (effectValue);
	}

	void Update(){
		if (active) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast(ray, out hit)){
				if(hit.collider.name.Equals("Creature(Clone)")){
					SetActiveStatus (false);
					Interact ();
					SetActiveStatus (false);
					transform.position = startPosition;
				}
			}
		}
	}

	public void SetActiveStatus(bool status){
		if (status) {
			GetComponent<UnityEngine.UI.Image> ().raycastTarget = false;
			active = true;
		} else {
			GetComponent<UnityEngine.UI.Image> ().raycastTarget = true;
			active = false;
		}
	}

	public static GameObject objectBeingDragged;
	Vector3 startPosition;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		startPosition = transform.position;
		SetActiveStatus (true);
	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if (active) {
			transform.position = Input.mousePosition;
		}
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		SetActiveStatus (false);
		transform.position = startPosition;
	}

	#endregion
}
