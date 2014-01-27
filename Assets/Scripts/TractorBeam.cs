using UnityEngine;
using System.Collections;

public class TractorBeam : MonoBehaviour {
	public int power;
	public bool stasisEffect;
	void FixedUpdate () {
		if (Input.GetButton("Fire2") || Input.GetButton("Fire1")) {
			Vector3 clickLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 myLoc = new Vector2(transform.position.x,transform.position.y);
			//raycast needs a vector for the direction from the player.
			Vector2 target_direction = new Vector2(clickLoc.x - myLoc.x,clickLoc.y - myLoc.y); 
			//print ("target_direction " + target_direction);
			//print ("myLoc " + myLoc);
			if( Input.GetButton("Fire2"))
				Debug.DrawRay(myLoc,target_direction, Color.blue,1);
			if(Input.GetButton ("Fire1"))
				Debug.DrawRay(myLoc,target_direction, Color.green,1);
			//send a raycast and return all intersections. magn. gives distance to cast e.g. distance clicked from player
			RaycastHit2D[] all_hit = Physics2D.RaycastAll(myLoc,target_direction,target_direction.magnitude);
			foreach(RaycastHit2D hit in all_hit){
				if(hit.collider != null){
					//print (hit.collider.gameObject.name);
					if (hit.collider.gameObject.name != "Prototypi" && hit.collider.rigidbody2D != null){
						int dir = 1;
						if (Input.GetButton("Fire1")) dir = -1;
						if(stasisEffect) hit.collider.rigidbody2D.velocity = Vector2.zero; //reset velocity for stasis effect
						hit.collider.rigidbody2D.AddForce( (myLoc - target_direction) * dir * power);
					}
				}
			}
		}
	}
}
