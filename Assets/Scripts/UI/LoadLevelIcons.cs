using UnityEngine;
using System.Collections;

// Load the Individual Score Icons
public class LoadLevelIcons : MonoBehaviour 
{

	public Transform levelPrefab;	// Prefab for the level icons
	private int sceneCounter;	// Number of scenes with level scores

	void Awake () 
	{
		// Minus 12 to take account of Tutorials, Credits, and Start Screens
		sceneCounter = Application.levelCount - 12;

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
					SpriteRenderer ren = level.GetComponent<SpriteRenderer>();
					ren.sprite = Resources.Load<Sprite>("LevelIcons/"+ "Scene1Icon") as Sprite;
					level.GetComponent<BoxCollider2D>().size = ren.bounds.size;
					++index;
				}
				else break;
			}
			yPos -= 15.0f;
		}
		
	}
	
}
