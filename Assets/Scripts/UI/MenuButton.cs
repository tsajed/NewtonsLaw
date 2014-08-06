/*
 * Base class for menu buttons.
 *
 */
using UnityEngine;
using System.Collections;

public abstract class MenuButton : MonoBehaviour {

	public Sprite hoverSprite;


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
		OnButtonClick ();
	}
	abstract protected void OnButtonClick();

}
