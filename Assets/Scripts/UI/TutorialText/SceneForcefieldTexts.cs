using UnityEngine;
using System.Collections;

public class SceneForcefieldTexts : MonoBehaviour {

	string tutorialString = "Forcefield enemies will try to ram you like spike enemies. However," +
		"forcefield enemies also have a shield that alternates on and off that will protect them from damage." +
		"Try to get them damaged while the shield is off!";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		GUI.skin.textArea.fontSize = 18;
		tutorialString = GUI.TextArea(new Rect(200, Screen.height - 150, Screen.width - 350, 200), tutorialString);
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Tutorial 8")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
}
