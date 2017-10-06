using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float NeedUpdateTick = 10f;
	private float _currentNeedsTick = 0f;


	public Creature CreaturePrefab;
	private Creature MyCreature;

	void Start()
	{
		MyCreature = Instantiate (CreaturePrefab);
		MyCreature.gameObject.transform.position = new Vector3 (0f, 0f, 0f);
	}


	void Update(){

		UpdateCreatureNeeds ();
	}

	/// <summary>
	/// Orders Creature to check it's needs at regular interval
	/// </summary>
	void UpdateCreatureNeeds(){
		_currentNeedsTick += Time.deltaTime;
		if (_currentNeedsTick > NeedUpdateTick) {
			_currentNeedsTick = 0;
			MyCreature.UpdateNeeds ();
		}
	}
}
