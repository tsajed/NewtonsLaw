using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	public void LoadLevel(string levelName) {
		Time.timeScale = 1;
		Application.LoadLevel(levelName);
	}
}
