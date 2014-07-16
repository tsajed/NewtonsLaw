using UnityEngine;
using System.Collections;

public class Glow : MonoBehaviour {
	public float variance;

	private SpriteRenderer spriteRenderer;
	private float offset;
	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		System.Random rnd = new System.Random ();
		offset = (float)rnd.Next (100)/100f;
		print (offset);
	}
	
	// Update is called once per frame
	void Update () {
		float alpha = Mathf.Abs(Mathf.Sin (Time.time + offset));
		alpha = alpha * variance;
		Color oldColor = spriteRenderer.color;
		Color newColor = new Color (oldColor.r, oldColor.g, oldColor.b, alpha + (1 - variance));
		spriteRenderer.color = newColor;
	}
}
