using UnityEngine;
using System.Collections;


public class EnemyScore : MonoBehaviour
{
	public GameObject scorePointsUI; // A prefab that appears when the enemy dies.
	public GenericEnemy self;

	private PlayerScore scoreBoard;	// Reference to the Score Script

	void Start () // Use this for initialization
	{
		scoreBoard = GameObject.Find("Score").GetComponent<PlayerScore>();
	}

	public void createScore () // Create the Score above the game object
	{
		// Increase the score by so and so points
		scoreBoard.score += self.score;

		// Instantiate the score points prefab at this point.
		GameObject scorePoints = (GameObject) Instantiate(scorePointsUI, Vector3.zero, Quaternion.identity);
		// Change the text to the enemy's score value
		scorePoints.GetComponent<TextMesh>().text = self.score.ToString();

		scorePoints.transform.parent = gameObject.transform;
		scorePoints.transform.localPosition = new Vector3(0, 1.5f, 0);
	}
}
