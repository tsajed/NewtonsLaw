using UnityEngine;
using System.Collections;


public class Health : MonoBehaviour {

	public int playerHealth;
	public int maxHealth;
	public SpriteRenderer spriteRenderer;
	public Sprite[] sprites;
	public AudioClip hurt;
	public bool canDie;

	void Start () 
	{
		spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
		sprites = Resources.LoadAll<Sprite> ("NL-player");
		hurt = Resources.Load<AudioClip> ("g450_hurt");
		playerHealth = 2;
		maxHealth = 2;
	}
	
	void Update () {
		if(playerHealth < 0)
			playerHealth = 0;
		if(playerHealth > maxHealth)
			playerHealth = maxHealth;

		// Set the player's sprite dependent on health
		spriteRenderer.sprite = sprites[playerHealth];
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
		int loaded = Application.loadedLevel;
		//restart level when player dies.
		//Application.LoadLevel(loaded);
	}
}
