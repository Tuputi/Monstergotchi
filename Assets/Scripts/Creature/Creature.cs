using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatureStates;
using DigitalRuby.Tween;

public class Creature : MonoBehaviour {


	//base needs
	public int _food = 100; // hunger meter
	public int _energy = 100; // sleepyness
	public int _motivation = 50; //0-100


	private EnergyState _energyState = EnergyState.Awake;
	private MotivationState _motivationState = MotivationState.Normal;


	[SerializeField]
	private int EnergySpeed = 3;
	[SerializeField]
	private int HungerSpeed = 3; // how fast the creature loses food
	[SerializeField]
	private int GettingBoredSpeed = 2; //how fast the creature moves toward bored

	public float MovementSpeed = 0.1f;

	private Animator animator;
	public GameObject PoopPrefab;

	/// <summary>
	/// Goes through the different needs and increases/decreases them as appropriate
	/// </summary>
	public void UpdateNeeds(){
		HandleHunger ();
		HandleEnergy ();
		HandleMotivation ();
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

	private void HandleMotivation(){
		switch (_motivationState) {
		case MotivationState.Normal:
			_motivation -= GettingBoredSpeed;
			if (_motivation < 20) {
				_motivationState = MotivationState.Bored;
			}
			break;
		case MotivationState.Bored:
			_motivation -= GettingBoredSpeed;
			if (_motivation < 0) {
				_motivation = 0;
			} else if (_motivation > 50) {
				_motivationState = MotivationState.Normal;
			}
			break;
		case MotivationState.Overexcited:
			_motivation -= GettingBoredSpeed;
			if (_motivation < 75) {
				_motivationState = MotivationState.Normal;
			}
			break;
		default:
			_motivation -= GettingBoredSpeed;
			if (_motivation < 0) {
				_motivation = 0;
			} 
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
		GameManager.instance.UpdateSliders ();
	}

	public void Sleep(){
		_energyState = EnergyState.Asleep;
		Debug.Log ("ZzzZZz");
		GameManager.instance.UpdateSliders ();
	}

	public void Play(int playEffect){
		_motivation += playEffect;
		Debug.Log ("Having fun");
		GameManager.instance.UpdateSliders ();
	}



	public void InitFromSave(CreatureSave save){
		_food = save.food;
		_energy = save.energy;
		_motivation = save.motivation;
	}

	public void ResetStats(){
		_food = 100;
		_energy = 100;
		_motivation = 100;
		UpdateNeeds ();
		GameManager.instance.UpdateSliders ();
	}



	//MOVEMENT/LOCATION LOGIC

	public void MoveToActionPoint(string myTargetType){
		ActionPoint targetPoint = RoomManager.Instance.GetActionPoint (myTargetType);

		if (animator == null) {
			animator = GetComponentInChildren<Animator> ();
		}
		//animator.SetBool ("Bounce", true);

		if (MovementIEnumerator != null) {
			StopCoroutine (MovementIEnumerator);
		}
		MovementIEnumerator = MoveIEnumerator (targetPoint);
		StartCoroutine(MovementIEnumerator);
	}

	IEnumerator MovementIEnumerator;



	IEnumerator MoveIEnumerator(ActionPoint actionPoint){
		bool TargetReached = false;
		bool RotationComplete = false;
		//values that will be set in the Inspector
		Transform Target = actionPoint.transform;
		float RotationSpeed = 5f;

		//values for internal use
		Quaternion _lookRotation;
		Vector3 _direction;

		Vector3 targetPos = new Vector3(actionPoint.gameObject.transform.position.x, 0f, actionPoint.transform.position.z);
		//gameObject.transform.LookAt (targetPos);

		while (!TargetReached) {
			_direction = (targetPos - transform.position).normalized;

			//create the rotation we need to be in to look at the target
			_lookRotation = Quaternion.LookRotation(_direction);

			//rotate us over time according to speed until we are in the required rotation
			transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

			Vector3 sourcePos = gameObject.transform.position;
			transform.position = Vector3.MoveTowards (sourcePos, targetPos, Mathf.SmoothStep (0, 1f, MovementSpeed));
			if (Mathf.Approximately (transform.position.x, targetPos.x) && Mathf.Approximately (transform.position.y, targetPos.y) && Mathf.Approximately (transform.position.z, targetPos.z)) {     
				TargetReached = true;
			}
			yield return 0;
		}
		//animator.SetBool ("Bounce", false);
		DoLocationAction (actionPoint);
		Debug.Log ("Target Reached");
	}
		
	private void DoLocationAction(ActionPoint actionPoint){
		switch (actionPoint.Type) {
		case ActionPoint.ActionPointType.Food:
			Feed (35);
			break;
		case ActionPoint.ActionPointType.Sleep:
			if (_energyState == EnergyState.Sleepy) {
				Sleep ();
			}
			break;
		case ActionPoint.ActionPointType.Play:
			Play (15);
			break;
		default:
			break;
		}
	}
}
		