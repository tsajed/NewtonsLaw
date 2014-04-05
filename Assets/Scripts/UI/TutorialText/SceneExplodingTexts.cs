using UnityEngine;
using System.Collections;

public class SceneExplodingTexts : MonoBehaviour {

	string tutorialString = "Exploding enemies will mostly mind their own business... " +
		"Until another entity collides with them when they blow up and damage everything around them. " +
		"These volatile beings can be a useful asset for clearing out groups of enemies.";
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
