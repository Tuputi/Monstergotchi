using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveFile {

	public long LastTimeStampUTC; //the last recorded timestamp to reference to when restarting & calculating passed time

}

[Serializable]
public class CreatureSave{

	public long LastTimeStampUTC;
	public string Name;

	public int food; 
	public int energy;
	public int motivation;
}
