using UnityEngine;
using System.Collections;

public class SaveOptions : MonoBehaviour 
{

    private string playerName;
    private string oldName;

    void Awake()
    {
    	playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
    }

    // Save the old settings, if the inputted text was invalid
    void OnEnable()
    {
        oldName = playerName;
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
        {
		  PlayerPrefs.SetString("PlayerName", playerName);
        }
        else
        {
            playerName = oldName;
        }
	}
}
