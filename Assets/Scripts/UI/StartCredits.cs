using UnityEngine;
using System.Collections;

public class StartCredits : MenuButton {
	override protected void OnButtonClick()
	{
		Debug.Log("CREDITS");
		Application.LoadLevel ("Credits");
	}
}
