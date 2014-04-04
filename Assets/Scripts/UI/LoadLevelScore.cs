using UnityEngine;
using System.Collections;

// Load the Scores of the Selected Level
public class LoadLevelScore : MonoBehaviour 
{

	public Transform scoreBoardPrefab;
	
	private Transform scoreBoard;	// The individual Score Board of a Level
	private GameObject backButton;	// The Back Button of the Score Levels List
	private string[] levelScores;	// Contains the scores of all the individual levels
	void OnMouseDown()
	{

		// Turn off the Back Button of the Score Levels List
		backButton = GameObject.Find("Top Back Text");
		backButton.SetActive(false);

		// Get the parent of Score List
		Transform parent = transform.parent.transform.parent;
		scoreBoard = Instantiate(scoreBoardPrefab, new Vector3(0, 0, -0.1f), Quaternion.identity) as Transform;
		scoreBoard.transform.parent = parent;
		scoreBoard.localPosition = new Vector3(0, 0, -0.1f);


		// Load the Scores
		loadScores();

		// Fill each rank
		int counter = 0;
		foreach(Transform child in scoreBoard) 
		{
			GUIText scoreText = child.GetComponent("GUIText") as GUIText;

			if(scoreText.name != "Back Text") 
			{
				scoreText.text += levelScores[counter];
				++counter;
			}
		}

		// Must keep a reference so that we can turn these game objects back on
		scoreBoard.Find("Back Text").GetComponent<BackLevelScoreBoard>().SetScoreList(transform.parent.gameObject, backButton);
		// Turn off the Score List Game Object
		transform.parent.gameObject.SetActive(false);
	}

	// Load the scores from PlayerPrefs!
	void loadScores() 
	{
		levelScores = new string[10];
		for(int i = 0; i < 10; i++)
		{
			// Get the number; The object name should be "Scene #"
			string[] name = transform.name.Split(char.Parse(" "));
			levelScores[i] = PlayerPrefs.GetString("Scene " + name[1] + "Score" + i, "Sean Gouglas:1234");
		}
	}
}
