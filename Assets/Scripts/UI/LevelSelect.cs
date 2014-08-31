using UnityEngine;
using System.Collections;

public class LevelSelect : MenuButton 
{
	public GameObject levelSelect;	// Made this public cause Find cannot find inactive objects

	override protected void OnButtonClick()
    {
        transform.parent.gameObject.SetActive(false);
    	levelSelect.SetActive(true);
    }
}
