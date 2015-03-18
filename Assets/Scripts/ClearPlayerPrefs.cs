using UnityEngine;
using System.Collections;

public class ClearPlayerPrefs : MonoBehaviour {

	void Start() {
		PlayerPrefs.DeleteAll();
	}
}
