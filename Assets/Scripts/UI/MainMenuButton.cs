using UnityEngine;
using System.Collections;

public class MainMenuButton : MenuButton {
	override protected void OnButtonClick()
	{
		Time.timeScale = 1;
		Application.LoadLevel("StartScreen");
	}

}
