using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	public void LoadLevel(string levelName) {
		Time.timeScale = 1;
		Application.LoadLevel(levelName);
	}

	public void NextLevel() {
		int index = Application.loadedLevel + 1;
		if(index < Application.levelCount) 
			Application.LoadLevel(index);
	 	else
	 		Application.LoadLevel("Credits");
	}
}
