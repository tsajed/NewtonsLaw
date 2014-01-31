using UnityEngine;
using System.Collections;


public class Health : MonoBehaviour {

	public int playerHealth = 2;
	public int maxHealth = 2;
	public SpriteRenderer renderer;
	public Sprite[] sprites;
	public AudioClip hurt;

	// Use this for initialization
	void Start () {
		renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerHealth < 0)
			playerHealth = 0;
		if(playerHealth > 2)
			playerHealth = 2;
		
		// Set the player's sprite dependent on health
		renderer.sprite = sprites[playerHealth];
	}

	public void increasePlayerHP(int amount) { playerHealth += amount; }
	public void decreasePlayerHP(int amount) 
	{ 
		playerHealth -= amount; 
		AudioSource.PlayClipAtPoint(hurt, GameObject.Find("Main Camera").transform.position);
	}
}
