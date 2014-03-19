using UnityEngine;
using System.Collections;

public class StartStartScreen : MonoBehaviour {

	public Sprite hoverSprite;
	
	private SpriteRenderer renderer;
	private Sprite normalSprite;

	void Awake() {
		renderer = gameObject.GetComponent<SpriteRenderer>();
		normalSprite = renderer.sprite;

	}

    void OnMouseEnter() {
    	renderer.sprite = hoverSprite;
    }

    void OnMouseExit() {
    	renderer.sprite = normalSprite;
    }

    void OnMouseDown() {
   	Application.LoadLevel ("StartScreen");
    }

}
