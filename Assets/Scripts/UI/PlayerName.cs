using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class PlayerName : MonoBehaviour {

    private string playerName;
    private InputField inputField;

    void Awake() {
    	inputField = GetComponent<InputField>();
    	playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
    	inputField.text = playerName;
    }

    void OnEnable() {
    	inputField.text = playerName;
    }

    public void ParseName() {
    	inputField.text = Regex.Replace(inputField.text, "[^0-9a-zA-Z\\(\\)\\s-]+", "");
    	playerName = inputField.text;
    }

    public void SaveName() {
    	if(playerName != "") {
		  PlayerPrefs.SetString("PlayerName", playerName);
        }
    }
}
