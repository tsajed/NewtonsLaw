using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
	public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can show.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can show.
	
	private Transform player;		// Reference to the player's transform.


	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}


	bool CheckXMargin()
	{
		// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}


	bool CheckYMargin()
	{
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}


	void FixedUpdate ()
	{

		TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{

		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		// If the player has moved beyond the x margin...
		if(CheckXMargin())
			// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
			// (Lerp to the edge of the margin, not the centre!)
			targetX = Mathf.Lerp(transform.position.x, player.position.x+(xMargin*Mathf.Sign(transform.position.x-player.position.x)), xSmooth * Time.deltaTime);

		// If the player has moved beyond the y margin...
		if(CheckYMargin())
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
			targetY = Mathf.Lerp(transform.position.y, player.position.y+(yMargin*Mathf.Sign(transform.position.y-player.position.y)), ySmooth * Time.deltaTime);

		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		//targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		//targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);


		//Do not display anything beyond the minimum and maximum.
		float xRight = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 0, 0)).x;
		float xLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0, 0)).x;
		float yBottom = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 0, 0)).y;
		float yTop = Camera.main.ViewportToWorldPoint (new Vector3 (0f, 1f, 0)).y;
		if ((xRight > maxXAndY.x && targetX >= transform.position.x) | 
		    (xLeft < minXAndY.x && targetX <= transform.position.x)) {

			targetX = transform.position.x;
		}
		if ((yTop > maxXAndY.y && targetY >= transform.position.y) | 
		    (yBottom < minXAndY.y && targetY <= transform.position.y)) {
			targetY = transform.position.y;
		}

		transform.position = new Vector3(targetX, targetY, transform.position.z);


	}
}
