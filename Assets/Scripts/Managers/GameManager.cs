using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class GameManager : MonoBehaviour {

	public Creature CreaturePrefab;
	private Creature myCreature;

	public static GameManager instance;

	public Creature MyCreature {
		get {
			return myCreature;
		}
	}

	public Image FoodSlide;
	public Image EnergySlider;
	public Image MotivationSlider;
	public Image CleanSlider;


	void Start()
	{
		instance = this;
		if (myCreature == null) {
			myCreature = Instantiate (CreaturePrefab);
			myCreature.gameObject.transform.position = new Vector3 (0f, 0f, 0f);
		}
		UpdateSliders ();
		InvokeRepeating ("UpdateCreatureNeeds", 1f, 1f); //update needs once per second
	}

	/// <summary>
	/// Orders Creature to check it's needs at regular interval
	/// </summary>
	void UpdateCreatureNeeds(){
		myCreature.UpdateNeeds ();
		UpdateSliders ();
	}

	public void UpdateSliders(){
		FoodSlide.fillAmount = myCreature._food/100f;
		EnergySlider.fillAmount = myCreature._energy/100f;
		MotivationSlider.fillAmount = myCreature._motivation/100f;
		CleanSlider.fillAmount = myCreature.cleanliness / 100f;
	}

	private void UpdateCreatureNeedsForATimespan(int seconds){
		int currentTime = 0;
		while (currentTime < seconds/2) {
			currentTime++;
			myCreature.UpdateNeeds ();
		}
		Debug.Log ("Updated: "+currentTime);
	}

	#region saving
	public void Save(){
		CreatureSave save = new CreatureSave ();
		save.LastTimeStampUTC = System.DateTime.Now.ToFileTimeUtc ();
		save.food = myCreature._food;
		save.energy = myCreature._energy;
		save.motivation = myCreature._motivation;
		save.cleanliness = myCreature.cleanliness;

		Debug.Log (JsonUtility.ToJson (save));
		File.WriteAllText (Application.persistentDataPath + "/save.txt",JsonUtility.ToJson(save));
	}

	public void Load(){
		if(File.Exists(Application.persistentDataPath + "/save.txt")){
			string content = File.ReadAllText (Application.persistentDataPath + "/save.txt");

			CreatureSave save = JsonUtility.FromJson <CreatureSave>(content);
			if (myCreature == null) {
				myCreature = Instantiate (CreaturePrefab);
				myCreature.gameObject.transform.position = new Vector3 (0f, 0f, 0f);
			}
			myCreature.InitFromSave (save);
			DateTime lastSaveTime = DateTime.FromFileTimeUtc (save.LastTimeStampUTC);
			TimeSpan passedTime = DateTime.Now - lastSaveTime;
			Debug.Log (passedTime);
			UpdateCreatureNeedsForATimespan (passedTime.Seconds);
			UpdateSliders ();
		}
	}

	public void ResetCreatureStats(){
		myCreature.ResetStats ();
	}


	void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			Debug.Log("Paused");
			Save ();
		}
		else
		{
			Debug.Log("resumed");
			Load ();
		}
	}

	void OnApplicationQuit(){
		Debug.Log("Quitting");
		Save ();
	}
	#endregion
}
