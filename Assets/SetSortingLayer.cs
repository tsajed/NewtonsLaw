using UnityEngine;
using System.Collections;

public class SetSortingLayer : MonoBehaviour {

	public string sortingLayerName;
	public int sortingLayerNumber;
	
	void Start ()
	{
		// Set the sorting layer
		renderer.sortingLayerName = sortingLayerName;
		renderer.sortingOrder = sortingLayerNumber;
	}
}
