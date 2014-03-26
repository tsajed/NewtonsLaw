using UnityEngine;
using System.Collections;

public class SceneBasicTexts : MonoBehaviour {

	string tutorialString = "Welcome to the tutorial screen. Use the left mouse, aim it on " +
		"an enemy and click it to push it away.";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		tutorialString = GUI.TextArea(new Rect(0, Screen.height - 50, Screen.width, 50), tutorialString);
	}
}
