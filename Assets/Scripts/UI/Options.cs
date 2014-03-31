using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {
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
    	Debug.Log("OPTIONS");
    }
}
