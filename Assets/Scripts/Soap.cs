using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class Soap : InteractionObject {

	public bool EffectActive = false;


	public override void Interact ()
	{
		EffectActive = true;
		GetComponentInChildren<UIParticleSystem> ().enabled = true;
		StartCoroutine (CleanEffect());
	}

	public override void EndInteraction(){
		Debug.Log ("Ending interaction");
		GetComponentInChildren<ParticleSystem> ().Stop (false, ParticleSystemStopBehavior.StopEmitting);
		GetComponentInChildren<UIParticleSystem> ().enabled = false;
		EffectActive = false;
		used = true;
		Destroy (this.gameObject);
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

	IEnumerator CleanEffect(){
		while (EffectActive) {
			//if (GameManager.instance.MyCreature.cleanliness < 100) {
				GameManager.instance.MyCreature.Clean (effectValue);
			//} else {
			//	EndInteraction ();
			//}
			yield return 0;
		}
		yield return 0;
	}

}
