using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureStates;

public class Creature : MonoBehaviour {


	//base needs
	public int _food = 100; // hunger meter
	[SerializeField]
	private int HungerSpeed = 3; // how fast the creature loses food

	public int _energy = 100; // sleepyness
	private EnergyState _energyState = EnergyState.Awake;
	[SerializeField]
	private int EnergySpeed = 3;

	public float MovementSpeed = 0.1f;

	private Animator animator;

	public GameObject PoopPrefab;

	/// <summary>
	/// Goes through the different needs and increases/decreases them as appropriate
	/// </summary>
	public void UpdateNeeds(){
		HandleHunger ();
		HandleEnergy ();
	}


	private void HandleHunger(){
		if (_food > 100) {
			Debug.Log ("Poop");
			GameObject poop = Instantiate (PoopPrefab);
			poop.transform.position = this.transform.position;
			_food = 100;
		}
		_food -= HungerSpeed;
	}

	private void HandleEnergy(){
		switch (_energyState) {
		case EnergyState.Awake:
			_energy -= EnergySpeed;
			if (_energy < 25) {
				_energyState = EnergyState.Sleepy;
			}
			break;
		case EnergyState.Sleepy:
			_energy -= EnergySpeed;
			if (_energy < 10) {
				_energyState = EnergyState.Asleep;
			}
			break;
		case EnergyState.Asleep:
			_energy += EnergySpeed;
			if (_energy >= 100) {
				_energyState = EnergyState.Awake;
			}
			break;
		case EnergyState.Hyper:
			break;
		default:
			_energy -= EnergySpeed;
			break;
		}
	}

	/// <summary>
	/// Adds the specified amount of food into the creature
	/// </summary>
	/// <param name="FoodEffect">Food effect.</param>
	public void Feed(int FoodEffect){
		_food += FoodEffect;
		Debug.Log ("Burp");
	}





	//MOVEMENT/LOCATION LOGIC

	public void MoveToActionPoint(string myTargetType){
		ActionPoint targetPoint = RoomManager.Instance.GetActionPoint (myTargetType);

		if (animator == null) {
			animator = GetComponentInChildren<Animator> ();
		}
		animator.SetBool ("Bounce", true);

		if (MovementIEnumerator != null) {
			StopCoroutine (MovementIEnumerator);
		}
		MovementIEnumerator = MoveIEnumerator (targetPoint);
		StartCoroutine(MovementIEnumerator);
	}

	IEnumerator MovementIEnumerator;

	IEnumerator MoveIEnumerator(ActionPoint actionPoint){
		bool TargetReached = false;
		Vector3 targetPos = actionPoint.gameObject.transform.position;
		while (!TargetReached) {
			Vector3 sourcePos = gameObject.transform.position;
			transform.position = Vector3.MoveTowards (sourcePos, targetPos, Mathf.SmoothStep (0, 1f, MovementSpeed));
			if (Mathf.Approximately (transform.position.x, targetPos.x) && Mathf.Approximately (transform.position.y, targetPos.y) && Mathf.Approximately (transform.position.z, targetPos.z)) {     
				TargetReached = true;
			}
			yield return 0;
		}
		animator.SetBool ("Bounce", false);
		DoLocationAction (actionPoint);
		Debug.Log ("Target Reached");
	}

	private void DoLocationAction(ActionPoint actionPoint){
		switch (actionPoint.Type) {
		case ActionPoint.ActionPointType.Food:
			Feed (35);
			Debug.Log ("Omnomonm...Burp");
			break;
		case ActionPoint.ActionPointType.Sleep:
			if (_energyState == EnergyState.Sleepy) {
				_energyState = EnergyState.Asleep;
				Debug.Log ("ZzzZZzz....");
			}
			break;
		default:
			break;
		}
	}
}
		