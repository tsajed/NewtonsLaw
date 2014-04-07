using UnityEngine;
using System.Collections;

public class HighScores : MonoBehaviour 
{
	public Sprite hoverSprite;
	public GameObject leaderBoard;	// Made this public cause Find cannot find inactive objects

	private SpriteRenderer ren;
	private Sprite normalSprite;

	void Awake() 
    {
		ren = gameObject.GetComponent<SpriteRenderer>();
		normalSprite = ren.sprite;
	}

    void OnMouseEnter() 
    {
    	ren.sprite = hoverSprite;
    }

    void OnEnable() 
    {
    	ren.sprite = normalSprite;
    }

    void OnMouseExit() 
    {
    	ren.sprite = normalSprite;
    }

    void OnMouseDown() 
    {
        transform.parent.gameObject.SetActive(false);
    	leaderBoard.SetActive(true);
    	leaderBoard.transform.Find("Score Choices").gameObject.SetActive(true);
    }
}
