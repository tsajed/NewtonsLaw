using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {
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
    	Debug.Log("EXIT");
    }
}
