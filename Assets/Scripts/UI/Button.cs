using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public GameObject showUI;
	public GameObject disableUI;

	// Maybe just use this script for everything
	void OnMouseDown()
	{
		showUI.SetActive(true);
		disableUI.SetActive(false);
	}
}
