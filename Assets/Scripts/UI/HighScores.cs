using UnityEngine;
using System.Collections;

public class HighScores : MonoBehaviour {
	public Sprite hoverSprite;
	public GameObject leaderBoard;	// Made this public cause Find cannot find inactive objects

	private SpriteRenderer renderer;
	private Sprite normalSprite;
	private GameObject startMenu;

	void Awake() {
		renderer = gameObject.GetComponent<SpriteRenderer>();
		normalSprite = renderer.sprite;

		startMenu = GameObject.Find("UI");
	}

    void OnMouseEnter() {
    	renderer.sprite = hoverSprite;
    }

    void OnMouseExit() {
    	renderer.sprite = normalSprite;
    }

    void OnMouseDown() {
    	startMenu.SetActive(false);
    	leaderBoard.SetActive(true);
    }
}
