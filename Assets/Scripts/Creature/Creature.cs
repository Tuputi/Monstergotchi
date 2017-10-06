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

	//compound value calculated based on base stats
	private int _happiness;

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
	/// Adds the specified amount of food into teh creature
	/// </summary>
	/// <param name="FoodEffect">Food effect.</param>
	public void Feed(int FoodEffect){
		_food += FoodEffect;
		Debug.Log ("Burp");
	}
}
		