using UnityEngine;
using System.Collections;

public class Start : MenuButton {
	override protected void OnButtonClick() {
    	Debug.Log("START");
		Application.LoadLevel(Application.loadedLevel + 1);
    }
}
