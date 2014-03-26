using UnityEngine;
using System.Collections;

public class SceneBasicTexts : MonoBehaviour {

	string tutorialString = "Welcome to the tutorial screen. Use the left mouse, aim it on " +
		"an enemy and click it to push it away. Click on the right mouse button to pull it toward" +
		" the player. The objects that are spawning are basic enemies and they don't hurt you. You" +
		" can push or pull them to your advantage. Click on the Tutorial 2 button for next tutorial";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		tutorialString = GUI.TextArea(new Rect(0, Screen.height - 50, Screen.width - 150, 50), tutorialString);
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Tutorial 2")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}

}
