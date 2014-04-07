using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour 
{
	public Sprite hoverSprite;
    public GameObject optionsMenu;
	
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
        optionsMenu.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }
}
