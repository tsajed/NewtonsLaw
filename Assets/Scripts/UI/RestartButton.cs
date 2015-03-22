using UnityEngine;
using System.Collections;

public class RestartButton : MonoBehaviour {
	public void RestartLevel()
	{
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevel);
	}

}
