using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SaveScoreLevel))]
public class Health : MonoBehaviour 
{
	public int playerHealth;
	public int maxHealth;
	public SpriteRenderer ren;
	public Sprite[] sprites;
	public AudioClip hurt;
	public AudioClip death;
	public bool canDie;
	
	private bool onDeath = false;
	private SaveScoreLevel saveScore;

	void Start () 
	{
		saveScore = this.GetComponent<SaveScoreLevel>();
		ren = gameObject.GetComponentInChildren<SpriteRenderer>();
	}
	
	void Update () 
	{
		if(playerHealth < 0)
			playerHealth = 0;
		if(playerHealth > maxHealth)
			playerHealth = maxHealth;

		// Set the player's sprite dependent on health
		ren.sprite = sprites[playerHealth];
	}

	public void increasePlayerHP(int amount) { playerHealth += amount; }

	public void decreasePlayerHP(int amount) 
	{ 
		playerHealth -= amount; 
		AudioSource.PlayClipAtPoint(hurt, GameObject.Find("Main Camera").transform.position);
		//flag for debug purposes
		if (canDie && playerHealth <= 0)
			die();
	}

	private void die()
	{
		AudioSource.PlayClipAtPoint(death, GameObject.Find("Main Camera").transform.position);
		Time.timeScale = 0;
		GameObject.Find("UI").transform.Find("Pause Menu").gameObject.SetActive(true);
	}
}
