using UnityEngine;
using System.Collections;

public class Options : MenuButton 
{
    public GameObject optionsMenu;

	override protected void OnButtonClick()    {
        optionsMenu.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
