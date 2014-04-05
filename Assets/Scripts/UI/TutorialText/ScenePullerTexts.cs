using UnityEngine;
using System.Collections;

public class ScenePullerTexts : MonoBehaviour {
	
	string tutorialString = "Puller enemies are the opposite of laser enemies. They will pull you into" +
		"their central vortex and damage you. They will still pull other enemies, but won't harm them, watch out!" +
		"Press the Start Game when you feel you are ready to face the horde.";
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
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Start Game")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
	
}
