using UnityEngine;
using System.Collections;

public class SceneMitoTexts : MonoBehaviour {
	
	string tutorialString = "Mitosis enemies " +
		"create smaller versions of themselves over time and will try to ram you, just like spike enemies. So make sure you evade the mitotis enemies" +
			" at all costs. Try to take them out fast, before they get a chance to multiply! Click on the Tutorial 6 button to go to next tutorial";
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
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Tutorial 6")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
	
}
