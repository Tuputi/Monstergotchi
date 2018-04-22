using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public string name;
	public bool active;
	public int effectValue = 10;
	public bool used = false;

	protected Ray ray;
	protected RaycastHit hit;

	//what happens when the creature is given this
	public virtual void Interact(){
		GameManager.instance.MyCreature.Feed (effectValue, this.gameObject);
		used = true;
	}

	public virtual void EndInteraction(){
	}

	void Update(){
		if (active) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast(ray, out hit) && !used){
				if(hit.collider.name.Equals("Creature(Clone)")){
					Interact ();
				}
			}
		}
	}

	void OnDestroy(){
		var parent = GetComponentInParent<IteractionObjectSource> ();
		if(parent != null)
			parent.CreateNewObject ();
	}

	public void SetActiveStatus(bool status){
		if (status) {
			GetComponent<UnityEngine.UI.Image> ().raycastTarget = false;
			active = true;
		} else {
			GetComponent<UnityEngine.UI.Image> ().raycastTarget = true;
			active = false;
			used = false;
			EndInteraction ();

		}
	}

	public static GameObject objectBeingDragged;
	Vector3 startPosition;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		if (used)
			return;
		
		startPosition = transform.position;
		SetActiveStatus (true);
	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if (active && !used) {
			transform.position = Input.mousePosition;
		}
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		if (used)
			return;
		
		SetActiveStatus (false);
		transform.position = startPosition;
	}

	#endregion
}
