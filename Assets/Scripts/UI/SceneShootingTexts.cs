using UnityEngine;
using System.Collections;

public class SceneShootingTexts : MonoBehaviour {
	
	string tutorialString = "Welcome to the Shooting Enemy Tutorial. Shooting enemies " +
		"shoot bubble like bullets at you which reduce your health. So make sure you evade the bullets" +
			" at all costs. You can use other enemies as shields. Shooting enemies are killed by spike" +
			" enemies when they come in contact. Click on the Tuturial 3 button to go to spike tutorial";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		tutorialString = GUI.TextArea(new Rect(0, Screen.height - 50, Screen.width - 150, 50), tutorialString);
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Tutorial 3")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
	
}
