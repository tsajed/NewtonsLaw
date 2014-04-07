using UnityEngine;
using System.Collections;

public class SceneShootingTexts : MonoBehaviour {
	
	string tutorialString = " Shooting enemies " +
		"shoot bubble-like bullets at you which reduce your health. You can be damaged during these tutorials, but you can't die." +
		"So out there in the real world, make sure you evade the bullets" +
			" at all costs. Touching a shooting enemy directly will harm you, but they'll try to maintain" +
			" a safe distance from you and shoot at you. You can use other enemies as shields to block projectiles. " +
			"Click on the Tutorial 3 button to go to the spike enemy tutorial";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		GUI.skin.textArea.fontSize = 18;
		GUI.TextArea(new Rect(200, Screen.height - 150, Screen.width - 350, 200), tutorialString);
		GUI.FocusControl("");
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Tutorial 3")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
	
}
