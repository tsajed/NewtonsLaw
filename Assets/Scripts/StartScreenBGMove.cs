using UnityEngine;
using System.Collections;

public class StartScreenBGMove : MonoBehaviour 
{

	public Vector3 start;
	public Vector3 end;
	public float speed = 10.0f;

	private float journeyLength;
	private float startTime;
	private bool finished;

	// Use this for initialization
	void Start () 
	{		
		startTime = Time.time;
		journeyLength = Vector3.Distance(start, end);
		//InvokeRepeating("MovePlayer", 2, 15.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	   	float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        if(transform.position != end) {
			transform.position = Vector3.Lerp(start, end, fracJourney);
		} else if(transform.position == end) {
			transform.position = start;
			startTime = Time.time;
		}

	}
}
