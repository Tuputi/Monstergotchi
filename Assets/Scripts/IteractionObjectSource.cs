using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IteractionObjectSource : MonoBehaviour {

	public InteractionObject itemPrefab;

	private GameObject activeObject;


	void Start(){
		CreateNewObject ();
	}

	public void CreateNewObject(){
			activeObject = GameObject.Instantiate (itemPrefab, transform).gameObject;
	}


}
