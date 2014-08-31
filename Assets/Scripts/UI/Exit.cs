using UnityEngine;
using System.Collections;

public class Exit : MenuButton {
	override protected void OnButtonClick()
	{
		Debug.Log("EXIT");
		Application.Quit();
	}
}
