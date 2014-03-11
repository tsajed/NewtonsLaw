using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public float timer = 200f;

	void Update(){
	  timer -= Time.deltaTime;
	  if (timer < 0) timer = 0; // clamp the timer to zero
	  
	  int seconds = (int) timer % 60; // calculate the seconds
	  int minutes = (int) timer / 60; // calculate the minutes
	  
	  guiText.text = "Timer: " + string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}
