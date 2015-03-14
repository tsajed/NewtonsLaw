using UnityEngine;
using System.Collections;


public class EnemyAvoidance : MonoBehaviour 
{
	public float power;

	void OnTriggerStay2D(Collider2D other)
	{
		GameObject other_gameobj = other.gameObject;
		string name = other_gameobj.name;
		//might not be the best way to check.
		if (name == "Prototypi")
			return;
		Vector2 oLoc = new Vector2(other_gameobj.transform.position.x,other_gameobj.transform.position.y);
		Vector2 myLoc = new Vector2(transform.position.x,transform.position.y);
		GetComponent<Rigidbody2D>().AddForce( (myLoc - oLoc).normalized * power);
	} 
}
