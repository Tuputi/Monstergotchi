using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimationEvents : MonoBehaviour {

	public Creature creature;

	public void EatFood(){
		if (creature.activeFood != null) {
			Destroy (creature.activeFood);
		}
	}

}
