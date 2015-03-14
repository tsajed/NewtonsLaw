using UnityEngine;
using System.Collections;

public class TractorBeam : MonoBehaviour
{
		public float power;
		public bool stasisEffect;
		public AudioClip pull;
		public AudioClip push;
		public Material[] lineMaterials;
		private LineRenderer line;
		private ParticleSystem particles;
		private float distance;
		private GameObject grabbed;
		private AudioSource audioSound;

		#if UNITY_IPHONE || UNITY_ANDROID
			private int fingerBeamId = -1;
			private bool mobilePull = false;
			private Joystick moveJoystick;
		#endif

		void Awake() {
			audioSound = GetComponent<AudioSource>();
		}

		void Start ()
		{
				line = GetComponentInChildren<LineRenderer> ();
				SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer> ();
				line.GetComponent<Renderer>().sortingLayerID = sprite.GetComponent<Renderer>().sortingLayerID;
				line.GetComponent<Renderer>().sortingOrder = sprite.GetComponent<Renderer>().sortingOrder;

				particles = GetComponentInChildren<ParticleSystem> ();
				
				#if UNITY_IPHONE || UNITY_ANDROID
					moveJoystick = GameObject.Find("MobileControls").GetComponentInChildren<Joystick>();
				#endif
		}

		void Update ()
		{
			#if UNITY_IPHONE || UNITY_ANDROID


				Vector3 mobileInput = Vector3.zero;
				if(Input.touchCount > 0) {
					for(int i = 0; i < Input.touchCount; ++i) {
						Touch input = Input.GetTouch(i);
						if(input.fingerId != moveJoystick.getFingerDown) {
							fingerBeamId = input.fingerId;
							mobileInput = input.position;
							if(input.tapCount == 2) {
								mobilePull = true;
							} else {
								mobilePull = false;
							}
							TractorBeamAction(mobileInput);
							break;
						}
					}
				}
				
				// Play the grab sound when grabbing.
				if (fingerBeamId != -1 && mobilePull) {
					audioSound.clip = pull;
					if (!audioSound.isPlaying)
						audioSound.Play ();
				} else {
					audioSound.Stop ();
				}

				// Turn off the line renderer once the player lets go of the button
				if(fingerBeamId != -1 && Input.touchCount > 0) {
					for(int j = 0; j < Input.touchCount; ++j) {
					 	if(Input.GetTouch(j).fingerId == fingerBeamId) {
					 		if(Input.GetTouch(j).phase == TouchPhase.Ended) {
								fingerBeamId = -1;
								mobilePull = false;
								line.gameObject.SetActive (false);
					 		}
					 	}
					}
				}

				if(fingerBeamId == moveJoystick.getFingerDown) {
					fingerBeamId = -1;
					mobilePull = false;
					line.gameObject.SetActive (false);
				}

				// If grab released, free the object.
				if(fingerBeamId != -1 && !mobilePull)
					grabbed = null;
			#else
				// Turn off the line renderer once the player lets go of the button
				if (Input.GetButtonUp ("Fire2") || Input.GetButtonUp ("Fire1")) {
					line.gameObject.SetActive (false);
				}
				
				// If grab released, free the object.
				if (Input.GetButtonUp ("Fire2")) {
					grabbed = null;
				}

				if (Input.GetButton ("Fire2") || Input.GetButton ("Fire1")) {
					TractorBeamAction (Input.mousePosition);
				}

				// Play the grab sound when grabbing.
				if (Input.GetButton ("Fire2")) {
					audioSound.clip = pull;
					if (!audioSound.isPlaying)
						audioSound.Play ();
				} else {
					audioSound.Stop ();
				}
			#endif

		}

		void TractorBeamAction (Vector3 inputClickLoc) {
			//Get click location.
			Vector3 clickLoc = Camera.main.ScreenToWorldPoint (inputClickLoc);
			Vector2 clickLoc2d = new Vector2 (clickLoc.x, clickLoc.y);

			var myLoc = new Vector2 (transform.position.x, transform.position.y);
			//raycast needs a vector for the direction from the player.
			var target_direction = new Vector2 (clickLoc.x - myLoc.x, clickLoc.y - myLoc.y);
			target_direction.Normalize ();
			target_direction.Scale (new Vector2 (50, 50));
			int materialIndex;

			//Move grabbed enemy if applicable.
			#if UNITY_IPHONE || UNITY_ANDROID
				if (fingerBeamId != -1 && mobilePull && grabbed != null) {
					orient (clickLoc);
				}
				if(fingerBeamId != -1 && mobilePull)
					materialIndex = 0;
				else
					materialIndex = 1;
			#else
				if (Input.GetButton ("Fire2") && grabbed != null) {
						orient (clickLoc);
				}
				if (Input.GetButton ("Fire2"))
						materialIndex = 0;
				else
						materialIndex = 1;
			#endif

			//Set the line renderer.
			line.gameObject.SetActive (true);
			renderLine (myLoc, target_direction, materialIndex);

			//send a raycast and return all intersections. magn. gives distance to cast e.g. distance clicked from player
			RaycastHit2D[] all_hit = Physics2D.RaycastAll (myLoc, target_direction, target_direction.magnitude);
			foreach (RaycastHit2D hit in all_hit) {
				if (hit.collider == null)
					continue;
				Rigidbody2D hitRigidBody = hit.collider.GetComponent<Rigidbody2D>();
				if (hit.collider.gameObject.name != "Prototypi" && hitRigidBody != null
					&& !hit.collider.gameObject.name.Contains ("Projectile")) {


					if (stasisEffect)
							hitRigidBody.velocity = Vector2.zero; //reset velocity for stasis effect

					#if UNITY_ANDROID || UNITY_IPHONE
						if(fingerBeamId != -1 && mobilePull && grabbed == null) {
							Vector2 hitPos2d = new Vector2 (hit.transform.position.x, hit.transform.position.y);
							distance = (hitPos2d - myLoc).magnitude;
							grabbed = hit.collider.gameObject;		
						} else if(fingerBeamId != -1 && !mobilePull) {
							// Play push sound when found a hit when pushing
							AudioSource.PlayClipAtPoint (push, hit.collider.transform.position);
							hitRigidBody.AddForce ((clickLoc2d - myLoc).normalized * power);

						}
					#else
						if (Input.GetButton ("Fire2") && grabbed == null) {
							Vector2 hitPos2d = new Vector2 (hit.transform.position.x, hit.transform.position.y);
							distance = (hitPos2d - myLoc).magnitude;
							grabbed = hit.collider.gameObject;
						} else if (Input.GetButton ("Fire1")) {
							// Play push sound when found a hit when pushing
							AudioSource.PlayClipAtPoint (push, hit.collider.transform.position);
							hitRigidBody.AddForce ((clickLoc2d - myLoc).normalized * power);

						}
					#endif
					break; // Only affect the first object hit.
				}
			}
		}

		void renderLine (Vector2 start, Vector2 end, int materialIndex)
		{
			line.material = lineMaterials [materialIndex];
			line.SetPosition (0, start);
			line.SetPosition (1, start + end);

			if (materialIndex == 0) {
					particles.startColor = Color.cyan;
			} else {
					particles.startColor = Color.green;
			}
			particles.transform.rotation = Quaternion.LookRotation (end);
			particles.Play ();
		}

		void orient (Vector3 clickLoc)
		{
			Rigidbody2D grabbedRigid = grabbed.GetComponent<Rigidbody2D>();
			if (stasisEffect)
				grabbedRigid.velocity = Vector2.zero; //reset velocity for stasis effect
	
			Vector3 target = (clickLoc - transform.position).normalized;
			target.Scale(new Vector3(distance,distance,0));
			//target = target * distance;
			Vector3 newDir = Vector3.RotateTowards(grabbed.transform.position-transform.position, target, 1F, 0.0F);
			newDir.Normalize();
			newDir.Scale (new Vector3(distance,distance,0));
			Debug.DrawRay(transform.position, newDir, Color.red, 1);
			Vector3 dirGlobal = newDir + transform.position;
			Debug.DrawRay(Vector3.zero, dirGlobal, Color.green,2);
			Vector3 dirTarget = dirGlobal - grabbed.transform.position;
			//Vector2 dirTarget2D = new Vector2 (dirTarget.x, dirTarget.y);
			Debug.DrawRay(grabbed.transform.position, dirTarget, Color.yellow,2);
			grabbedRigid.AddForce (dirTarget * power);

				
				
		}

}

