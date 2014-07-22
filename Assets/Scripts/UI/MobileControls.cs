using UnityEngine;
using System.Collections;

public class MobileControls : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		#if !(UNITY_IPHONE || UNITY_ANDROID)
			Destroy(gameObject);
		#endif
	}
}
