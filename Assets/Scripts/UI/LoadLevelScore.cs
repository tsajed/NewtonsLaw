using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Load the Scores of the Selected Level
public class LoadLevelScore : MonoBehaviour 
{
	public Transform scoreBoardPrefab;	// The score holder
	public Transform scorePrefab;	// The individual score
	public Transform mainUI;	// The Main UI to parent to
	public GameObject levelSelectUI;	// The Level Select UI
	
	private string[] levelScores;	// Contains the scores of all the individual levels
	private string[] levelPlayerNames;	// Contains the names of all the players
	void Awake()
	{
		mainUI = GameObject.Find("UI 3.0").transform;
		levelSelectUI = GameObject.Find("Level Scores Menu");

		// Load the Scores!
		levelScores = new string[10];
		levelPlayerNames = new string[10];
		for(int i = 0; i < 10; i++)
		{
			// Get the number; The object name should be "Scene #"
			string[] name = transform.name.Split(char.Parse(" "));
			string[] data = PlayerPrefs.GetString("Scene " + name[1] + "Score" + i, "").Split(char.Parse(":"));
			if(data.Length > 1)
			{
				levelPlayerNames[i] = data[0];
				levelScores[i] = data[1];
			}
		}
	}

	public void CreateScoreBoard() {
		levelSelectUI.SetActive(false);

		RectTransform scoreBoard = Instantiate(scoreBoardPrefab, Vector3.zero, Quaternion.identity) as RectTransform;
		scoreBoard.SetParent(mainUI);
		scoreBoard.anchoredPosition = new Vector2(0,0);
		scoreBoard.sizeDelta = new Vector2(0,0);
		scoreBoard.localScale = new Vector3(1, 1, 1);
		scoreBoard.Find("Level Name").GetComponent<Text>().text = transform.name;
		scoreBoard.Find("Back Button").GetComponent<SetViews>().newViews = new GameObject[1] { levelSelectUI };
		
		Transform scoreList = scoreBoard.Find("Score List");
		for(int i = 0; i < levelScores.Length; i++) {
			Transform score = Instantiate(scorePrefab, Vector3.zero, Quaternion.identity) as Transform;
			score.SetParent(scoreList);
			score.localScale = new Vector2(1,1);
			score.GetComponent<Text>().text = (i+1) + ". " + levelPlayerNames[i];
			//score.localScale = new Vector3(1, 1, 1);
			score.GetChild(0).GetComponent<Text>().text = levelScores[i]; 
		}
	}
}
