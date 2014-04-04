using UnityEngine;
using System.Collections;

// Load the High Scores when Loading the Start Menu
public class LoadLevelScores : MonoBehaviour 
{

	public Transform levelPrefab;	// Prefab for the level icons

	private string[,] levelScores;	// Contains the scores of all the individual levels
	private int sceneCounter;	// Number of scenes with level scores

	void Awake () 
	{
		// Minus 8 to take account of Tutorials, Credits, and Start Screens
		sceneCounter = Application.levelCount - 8;

		levelScores = new string[sceneCounter, 10];
		for(int k = 0; k < sceneCounter; k++)
		{
			for(int j = 0; j < 10; j++)
			{
				// Score saved in variables such as 'Scene 1Score1', 'Scene 1Score2', etc
				levelScores[k, j] = PlayerPrefs.GetString("Scene " + (k+1) + "Score" + j, "Sean Gouglas:1234");
				
				//Debug.Log("Scene " + (k+1) + "Score" + j + ": " + levelScores[k,j]);
			}
		}

		// Build the UI!
		int index = 0;
		float[] xPos = {-30.0f, -15.0f, 0.0f, 15.0f, 30.0f};
		float yPos = 15.0f;
		Vector3 position = transform.parent.transform.position;

		while(index < sceneCounter)
		{
			for(int i = 0; i < 5; i++)
			{
				if(index < sceneCounter) {
					Transform level = Instantiate(levelPrefab, new Vector3(position.x + xPos[i], position.y + yPos, -0.1f), Quaternion.identity) as Transform;
					level.name = "Level " + (index+1);
					level.transform.parent = gameObject.transform;
					level.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("LevelIcons/"+ "Scene1Icon") as Sprite;
					++index;
				}
				else break;
			}
			yPos -= 15.0f;
		}
		
	}
	
}
