using UnityEngine;
using System.Collections;

public class SceneBasicTexts : MonoBehaviour {

	string tutorialString = "Hello, my name is Isaac Newton, welcome to the universe. I'm going to teach you how to survive and become of force of nature." +
		"Your only weapon in this world is pushing and pulling other entities using your tractor beam. Use the left mouse, aim it on " +
		"an enemy and click it to push it away. Click on the right mouse button to pull it toward" +
		" you. The objects in this area are basic enemies and they don't hurt you. You" +
		" can push or pull them to your advantage later on. Click on the Tutorial 2 button for the next lesson.";
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
		if (GUI.Button(new Rect(Screen.width - 150, Screen.height - 50, 150, 50), "Tutorial 2")) {
			Debug.Log("Clicked the button with text");
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}

}
