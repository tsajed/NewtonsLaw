using UnityEngine;
using System.Collections;

public class HighScores : MonoBehaviour {
	public Sprite hoverSprite;
	public GameObject leaderBoard;	// Made this public cause Find cannot find inactive objects

	private SpriteRenderer ren;
	private Sprite normalSprite;
	private GameObject startMenu;

	void Awake() {
		ren = gameObject.GetComponent<SpriteRenderer>();
		normalSprite = ren.sprite;

		startMenu = GameObject.Find("UI");
	}

    void OnMouseEnter() {
    	ren.sprite = hoverSprite;
    }

    void OnEnable() {
    	ren.sprite = normalSprite;
    }

    void OnMouseExit() {
    	ren.sprite = normalSprite;
    }

    void OnMouseDown() {
    	startMenu.SetActive(false);
    	leaderBoard.SetActive(true);
    	leaderBoard.transform.Find("Score Choices").gameObject.SetActive(true);
    }
}
