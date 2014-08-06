using UnityEngine;
using System.Collections;

public class RestartButton : MenuButton {
	override protected void OnButtonClick()
	{
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevel);
	}

}
