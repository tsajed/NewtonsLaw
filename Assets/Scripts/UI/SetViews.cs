using UnityEngine;
using System.Collections;

public class SetViews : MonoBehaviour {

	public GameObject[] oldViews;
	public GameObject[] newViews;

	public void ChangeView() {
		for(int i = 0; i < oldViews.Length; i++) {
				oldViews[i].SetActive(false);
		}
		for(int j = 0; j < newViews.Length; j++) {
			newViews[j].SetActive(true);
		}
	}
}
