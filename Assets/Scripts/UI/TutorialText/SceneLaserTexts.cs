using UnityEngine;
using System.Collections;

public class SceneLaserTexts : MonoBehaviour {
	
	string tutorialString = "Welcome to the LaserEnemy Tutorial. Laser enemies " +
		"emit laser in 4 directions. Make sure you are not near the laser" +
			" at all costs. If other enemies hit the laser, they will be flung away. Click on the Tutorial 5 button to go to next tutorial";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		GUI.skin.textArea.fontSize = 18;
		tutorialString = GUI.TextArea(new Rect(200, Screen.height - 150, Screen.width - 350, 200), tutorialString);
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Tutorial 5")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
	
}
