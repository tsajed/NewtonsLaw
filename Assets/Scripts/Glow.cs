using UnityEngine;
using System.Collections;

public class Glow : MonoBehaviour {
	public float variance;
	public float speed = 1f;
	public Color secondaryColor = Color.white;

	private Color primaryColor;

	private SpriteRenderer spriteRenderer;
	private float offset;
	// Use this for initialization
	void Start () {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		System.Random rnd = new System.Random ();
		offset = (float)rnd.Next (100)/100f;
		primaryColor = spriteRenderer.color;
	}
	
	// Update is called once per frame
	void Update () {
		//Interpolate transparency.
		float alpha = Mathf.Abs(Mathf.Sin (speed * (Time.time + offset)));
		alpha = alpha * variance;
		//Interpolate color.
		Color intrpColor = (primaryColor * alpha) + (secondaryColor * (1-alpha));
		Color newColor = new Color (intrpColor.r, intrpColor.g, intrpColor.b, alpha + (1 - variance));
		spriteRenderer.color = newColor;
	}
}
