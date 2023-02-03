using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(GravityBody))]
[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{

	// public vars
	//public float mouseSensitivityX = 1;
	//public float mouseSensitivityY = 1;
	//public float walkSpeed = 6;
	//public float jumpForce = 220;
	//public LayerMask groundedMask;

	// System vars
	//bool grounded;
	Vector3 moveAmount;
	//Vector3 smoothMoveVelocity;
	//float verticalLookRotation;
	//Transform cameraTransform;
	Rigidbody rigidbody;

	private Vector3 moveDirection;

	private Vector2 moveDir;
	[SerializeField] private float moveSpeed = 4f;
	[SerializeField] private GameObject globe;
	[SerializeField] private float gravity = -9.8f;

	void Awake()
	{
		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
		//cameraTransform = Camera.main.transform;
		rigidbody = GetComponent<Rigidbody>();
		//globe = null;
		globe = GameObject.Find("Globe");//GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
		//rigidbody = GetComponent<Rigidbody>();

		// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void Update()
	{
		// Look rotation:
		/*transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;*/
		//verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
		//cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;

		// Calculate movement:
		//float inputX = Input.GetAxisRaw("Horizontal");
		//float inputY = Input.GetAxisRaw("Vertical");

		//Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
		//Vector3 targetMoveAmount = moveDir * walkSpeed;
		//moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

		// Jump
		/*if (Input.GetButtonDown("Jump"))
		{
			if (grounded)
			{
				rigidbody.AddForce(transform.up * jumpForce);
			}
		}*/

		// Grounded check
		/*Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 1 + .1f, groundedMask))
		{
			grounded = true;
		}
		else
		{
			grounded = false;
		}*/

		GoBackMoving();
		LRMoving();
		//AddforceGravity();

	}

	void FixedUpdate()
	{
		// Apply movement to rigidbody
		Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
		rigidbody.MovePosition(rigidbody.position + localMove);

		Attract(rigidbody);
	}


/*	private void AddforceGravity()
	{
		if (globe != null)
		{
			var dir = this.transform.position - globe.transform.position;
			transform.GetComponent<Rigidbody>().AddForce((-1) * dir * vectorGravity);
		}
	}*/

	private void GoBackMoving()
	{
		if (moveDir.y != 0)
		{
			/*if (globe != null)
				transform.position += transform.forward * Time.deltaTime * moveSpeed * (globe.transform.lossyScale.x) * (moveDir.y);
			else */
			transform.position += transform.forward * Time.deltaTime * moveSpeed * (moveDir.y);

		}

	}

	private void LRMoving()
	{
        if (globe != null)
        {
            var dir = this.transform.position - globe.transform.position;
            transform.RotateAround(transform.position, dir, 0.5f * moveDir.x);
        }
        else if (moveDir.x != 0) StartCoroutine(Rotatetions(0.005f, moveDir.y * moveDir.x));
	}

	private void OnMove(InputValue value)
	{

		Vector2 input = value.Get<Vector2>();                 // 입력 받은 값을 가져오기

		//Debug.Log("x: " + input.x + ", y:" + input.y);
		if (input != null)
		{
			moveDir = input;

			//transform.position += transform.forward * Time.deltaTime * movementSpeed;
			//moveDirection = new Vector3(input.x, 0f, input.y);
			//Debug.Log($"SEND_MESSAGE : {input.magnitude}");
		}
	}


	private IEnumerator Rotatetions(float duration, float angle)
	{
		float startRotation = this.transform.localEulerAngles.y;
		float endRotation = startRotation + angle;

		float t = 0.0f;
		while (t < duration)
		{
			t += Time.deltaTime;

			float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;

			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRotation,
			transform.localEulerAngles.z);
			/*transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRotation,
			transform.localEulerAngles.z);*/
			//transform.Rotate(transform.forward, yRotation);


			yield return null;
		}
	}



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