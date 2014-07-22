﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	
	public float stunCooldown = 0f;

	#if UNITY_IPHONE || UNITY_ANDROID
		private Joystick moveJoystick;
	#endif

	void Start ()
	{
		#if UNITY_IPHONE || UNITY_ANDROID
			moveJoystick = GameObject.Find("MobileControls").GetComponentInChildren<Joystick>();
		#endif
	}

	void FixedUpdate () 
	{
		TryMove ();
	}

	private void TryMove ()
	{
		if (stunCooldown > 0)
			stunCooldown -= Time.deltaTime;
		else
			Move ();
	}

	private void Move ()
	{
		if (!Input.anyKey)
			return;

		// Cache the horizontal input.
		#if UNITY_IPHONE || UNITY_ANDROID
			float h = moveJoystick.position.x;
			float v = moveJoystick.position.y;
		#else
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");
		#endif

		// Set a maximum velocity, don't stop add force when you're over the max velocity!
		rigidbody2D.AddForce (new Vector2 (h * moveForce, v * moveForce));
		if (rigidbody2D.velocity.x > maxSpeed)
		{
			rigidbody2D.velocity = new Vector2 (maxSpeed, rigidbody2D.velocity.y);
		}
		else if (rigidbody2D.velocity.x < -maxSpeed)
		{
			rigidbody2D.velocity = new Vector2 (-maxSpeed, rigidbody2D.velocity.y);
		}
		if (rigidbody2D.velocity.y > maxSpeed)
		{
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, maxSpeed);
		}
		else if (rigidbody2D.velocity.y < -maxSpeed)
		{
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, -maxSpeed);
		}



		//Invisible walls
		if (rigidbody2D.transform.position.x < -78.5)
		{	
			// player can't move
			stunCooldown = 0.3f;
			rigidbody2D.velocity = new Vector2 (maxSpeed+30, rigidbody2D.velocity.y);
		}
		else if (rigidbody2D.transform.position.x > 78.5)
		{
			stunCooldown = 0.3f;
			rigidbody2D.velocity = new Vector2 (-maxSpeed-30, rigidbody2D.velocity.y);
		}
		if (rigidbody2D.transform.position.y < -78.5)
		{
			stunCooldown = 0.3f;
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, maxSpeed+30);
		}
		else if (rigidbody2D.transform.position.y > 78.5)
		{
			stunCooldown = 0.3f;
			rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.y, -maxSpeed-30);

		}

		// Wraparound, unused.
		/**
		if (rigidbody2D.transform.position.x < -76)
		{
			Vector3 tr = rigidbody2D.transform.position;
			tr.x += 150;
			rigidbody2D.transform.position = tr;
		}
		else if (rigidbody2D.transform.position.x > 76)
		{
			Vector3 tr = rigidbody2D.transform.position;
			tr.x -= 150;
			rigidbody2D.transform.position = tr;
		}
		if (rigidbody2D.transform.position.y < -76)
		{
			Vector3 tr = rigidbody2D.transform.position;
			tr.y += 150;
			rigidbody2D.transform.position = tr;
		}
		else if (rigidbody2D.transform.position.y > 76)
		{
			Vector3 tr = rigidbody2D.transform.position;
			tr.y -= 150;
			rigidbody2D.transform.position = tr;
		}
		*/
	}
}
