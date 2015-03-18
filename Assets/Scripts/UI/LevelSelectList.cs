using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class LevelSelectList : MonoBehaviour {

	public Transform levelPrefab;	// Prefab for the level icons
	public int numExclude;	// Number of Scenes to not include

	public bool updateLevelList = false;	// Update the list in the Editor

	private int sceneCounter;	// Number of scenes with level scores
	
	void Update() {
		if(updateLevelList) {
			updateLevelList = false;

			// Get and delete the old children
			List<GameObject> oldChildren = new List<GameObject>();
			foreach(Transform child in transform) {
				oldChildren.Add(child.gameObject);
			}
			oldChildren.ForEach(child => DestroyImmediate(child));

			// Minus 12 to take account of Tutorials, Credits, and Start Screens
			sceneCounter = Application.levelCount - numExclude;
			int index = 0;
			while(index < sceneCounter) {
				Transform level = Instantiate(levelPrefab, Vector3.zero, Quaternion.identity) as Transform;
				level.name = "Level " + (index+1);
				level.SetParent(gameObject.transform);
				level.localScale = new Vector3(1, 1, 1);
				level.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("LevelIcons/" + "Scene" + (index + 1) + "Icon") as Sprite;
				++index;
			}
		}
	}
}
