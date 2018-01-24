using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public float NeedUpdateTick = 10f;
	private float _currentNeedsTick = 0f;


	public Creature CreaturePrefab;
	private Creature myCreature;

	public static GameManager instance;

	public Creature MyCreature {
		get {
			return myCreature;
		}
	}

	void Start()
	{
		instance = this;
		myCreature = Instantiate (CreaturePrefab);
		myCreature.gameObject.transform.position = new Vector3 (0f, 0f, 0f);
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
			myCreature.UpdateNeeds ();
		}
	}

	private void UpdateCreatureNeedsForATimespan(float timespan){
		float currentTime = 0;
		while (currentTime < timespan) {
			currentTime += NeedUpdateTick;
			myCreature.UpdateNeeds ();
		}
	}

	#region saving
	public void Save(){
	}

	public void Load(){
	}
	#endregion
}
