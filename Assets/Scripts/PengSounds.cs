﻿using UnityEngine;
using System.Collections;

public class PengSounds : MonoBehaviour {

        public AudioClip penguin;
	public AudioClip explosion;
	public AudioClip fire;
	
	// Use this for initialization
	void Start () {
	     	   StartCoroutine(wait());
		   AudioSource.PlayClipAtPoint(penguin, GameObject.Find("Main Camera").transform.position);
	     	   AudioSource.PlayClipAtPoint(explosion, GameObject.Find("Main Camera").transform.position);
	 	   AudioSource.PlayClipAtPoint(fire, GameObject.Find("Main Camera").transform.position);

	}

	public IEnumerator wait()
	{
		   yield return new WaitForSeconds (10);
	}

}