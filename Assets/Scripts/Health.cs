using UnityEngine;
using System.Collections;


public class Health : MonoBehaviour {

	public int playerHealth;
	public int maxHealth;
	public SpriteRenderer renderer;
	public Sprite[] sprites;
	public AudioClip hurt;
	public bool canDie;
	private bool onDeath = false;
	void Start () 
	{
		renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
	}
	
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
		int loaded = Application.loadedLevel;
		//restart level when player dies.
		//Application.LoadLevel(loaded);
		Time.timeScale = 0;
		onDeath = true;
	}

	void OnGUI() {

		if(onDeath) {
			if (GUI.Button(new Rect(Screen.width/2, Screen.height/2 , 150, 50), "Restart Level")) {
				Debug.Log("Clicked the button with text");
				Time.timeScale = 1;
				Application.LoadLevel(Application.loadedLevel);
			}
			if (GUI.Button(new Rect(Screen.width/2, Screen.height/2 + 70, 150, 50), "Go To Next Level")) {
				Debug.Log("Clicked the button with text");
				Time.timeScale = 1;
				Application.LoadLevel(Application.loadedLevel + 1);
			}
		}

	}
}
