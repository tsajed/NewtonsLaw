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
        // Not using Regex, due to performance reasons (OnGUI called several times)
        // More info: http://answers.unity3d.com/questions/18736/restrict-characters-in-guitextfield.html
        char chr = Event.current.character;
        if ( (chr < 'a' || chr > 'z') && (chr < 'A' || chr > 'Z') && (chr < '0' || chr > '9') ) {
            Event.current.character = '\0';
        }

    	GUI.Label(new Rect(Screen.width/2 - 200, 100, 200, 20), "Player Name:");
        playerName = GUI.TextField(new Rect(Screen.width/2 - 100, 100, 200, 20), playerName, 10);
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
