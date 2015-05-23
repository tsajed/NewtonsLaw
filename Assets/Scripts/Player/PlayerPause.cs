using UnityEngine;
using System.Collections;

public class PlayerPause : MonoBehaviour {
	private bool paused = false;
	private GameObject pausebg;
	private Health healthScript;
	
	void Start()
	{
		//Since you can't directly Find() inactive game objects, we 
		//get the UI gameobject and then get the transform from it.
		//Unity is weird sometimes.
		pausebg = GameObject.Find("UI").transform.FindChild("Pause Menu").gameObject;
		healthScript = GameObject.FindWithTag("Player").GetComponent<Health>();

	}
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Pause"))
		{
			if(healthScript && healthScript.playerHealth > 0) 
			{
				if(!paused)
				{
					paused = true;
					Time.timeScale = 0;
					pausebg.SetActive(true);
				}
				else
				{
					pausebg.SetActive(false);
					paused = false;
					Time.timeScale = 1;
				}
			}
		}
	}
}
