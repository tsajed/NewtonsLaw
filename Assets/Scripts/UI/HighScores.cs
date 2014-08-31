using UnityEngine;
using System.Collections;

public class HighScores : MenuButton {
	public GameObject leaderBoard;	// Made this public cause Find cannot find inactive objects

	override protected void OnButtonClick()
	{
        transform.parent.gameObject.SetActive(false);
    	leaderBoard.SetActive(true);
    	leaderBoard.transform.Find("Score Choices").gameObject.SetActive(true);
    }
}
