using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {
	
	private GravityAttractor globe1;
	private GameObject globe;
	private Rigidbody rigidbody;
	
	void Awake () {
		globe = GameObject.Find("Globe");//GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
		rigidbody = GetComponent<Rigidbody> ();

		// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	void FixedUpdate () {
		// Allow this body to be influenced by planet's gravity
		Attract(rigidbody);
	}


	public float gravity = -9.8f;


	public void Attract(Rigidbody body)
	{
		Vector3 gravityUp = (body.position - globe.transform.position).normalized;
		Vector3 localUp = body.transform.up;

		// Apply downwards gravity to body
		body.AddForce(gravityUp * gravity);
		// Allign bodies up axis with the centre of planet
		body.rotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
	}
}