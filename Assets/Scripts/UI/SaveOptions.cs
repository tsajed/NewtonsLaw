using UnityEngine;
using System.Collections;

public class SaveOptions : MonoBehaviour 
{

    private string playerName;

    void Awake()
    {
    	playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
    }

    void OnGUI() 
    {
    	GUI.Label(new Rect(Screen.width/2 - 200, 100, 200, 20), "Player Name:");
        playerName = GUI.TextField(new Rect(Screen.width/2 - 100, 100, 200, 20), playerName, 25);
    }

	void OnMouseDown()
	{
		// Save the Player Name
        if(playerName != "")
		  PlayerPrefs.SetString("PlayerName", playerName);
	}
}
