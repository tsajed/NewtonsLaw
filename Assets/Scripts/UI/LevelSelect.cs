using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour 
{
	public Sprite hoverSprite;
	public GameObject levelSelect;	// Made this public cause Find cannot find inactive objects

	private SpriteRenderer ren;
	private Sprite normalSprite;
	private GameObject startMenu;

	void Awake() 
	{
		ren = gameObject.GetComponent<SpriteRenderer>();
		normalSprite = ren.sprite;

		startMenu = GameObject.Find("UI");
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
    	startMenu.SetActive(false);
    	levelSelect.SetActive(true);
    }
}
