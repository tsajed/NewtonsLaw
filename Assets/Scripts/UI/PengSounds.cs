using UnityEngine;
using System.Collections;

public class PengSounds : MonoBehaviour {

        public AudioClip penguin;
	public AudioClip explosion;
	public AudioClip fire;
	
	// Use this for initialization
	IEnumerator Start () {
	     	   yield return new WaitForSeconds (0.4f);
		   AudioSource.PlayClipAtPoint(penguin, GameObject.Find("Main Camera").transform.position);
	     	   yield return new WaitForSeconds (2.5f);
	     	   AudioSource.PlayClipAtPoint(explosion, GameObject.Find("Main Camera").transform.position);
	     	   yield return new WaitForSeconds (0.5f);
	 	   AudioSource.PlayClipAtPoint(fire, GameObject.Find("Main Camera").transform.position);
		   yield return new WaitForSeconds (2);
		   Application.LoadLevel ("SceneBasic");

	}

}
