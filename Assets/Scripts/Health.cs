using UnityEngine;
using System.Collections;


public class Health : MonoBehaviour {

	public int playerHealth = 2;
	public int maxHealth = 2;
	public SpriteRenderer renderer;
	public Sprite[] sprites;
	public AudioClip hurt;
	public bool canDie;

	// Use this for initialization
	void Start () {
		renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerHealth < 0)
			playerHealth = 0;
		if(playerHealth > maxHealth)
			playerHealth = maxHealth;

		// Set the player's sprite dependent on health
		renderer.sprite = sprites[playerHealth];
	}

	public void increasePlayerHP(int amount) { playerHealth += amount; }
	public void decreasePlayerHP(int amount) 
	{ 
		playerHealth -= amount; 
		AudioSource.PlayClipAtPoint(hurt, GameObject.Find("Main Camera").transform.position);
		//flag for debug purposes
		if (canDie) {
			if(playerHealth <= 0)
			{
				die();
			}
		}
	}
	private void die()
	{
		//TODO menus/GUI etc.
		print("u ded");
		Time.timeScale = 0;
		//Destroy (this.gameObject); //note that this causes errors currently as other scripts still try to reference the object.
	}
}
