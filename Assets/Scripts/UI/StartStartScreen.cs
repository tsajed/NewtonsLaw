using UnityEngine;
using System.Collections;

public class StartStartScreen : MonoBehaviour {

	public Sprite hoverSprite;
	
	private SpriteRenderer ren;
	private Sprite normalSprite;

	void Awake() {
		ren = gameObject.GetComponent<SpriteRenderer>();
		normalSprite = ren.sprite;

	}

    void OnMouseEnter() {
    	ren.sprite = hoverSprite;
    }

    void OnMouseExit() {
    	ren.sprite = normalSprite;
    }

    void OnMouseDown() {
   	Application.LoadLevel ("StartScreen");
    }

}
