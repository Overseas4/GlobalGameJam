using UnityEngine;

public class ControllerMark1 : MonoBehaviour
{
	private const string HorizontalAxis = "Horizontal";
	private const string VerticalAxis = "Vertical";
	private const string ActionButton = "Fire1";
	private const string PickUpButton = "Fire2";

	private int turnSpeedHash = Animator.StringToHash("turnSpeedHash");
	private int forwardSpeedHash = Animator.StringToHash("forwardSpeedHash");
	private int pickUpHash = Animator.StringToHash("pickUp");
	private int action1Hash = Animator.StringToHash("action1");

	private float turnSpeed;
	private float forwardSpeed;
	private bool pickUp;
	private bool action1;

	public float maxSpeed;
	public float decelationSpeed;
	public float accelationSpeed;
	public float rotateSpeed;
	private float life;

	private Rigidbody rb;
	private Animator anim;
	private Transform playerTransform;

	private void Awake()
	{
		rb = GetComponentInChildren<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
		playerTransform = transform.GetChild(0).transform;
	}
	void Start()
	{

	}

	void Update()
	{

	}

	private void FixedUpdate()
	{
		Vector3 direction = new Vector3(Input.GetAxisRaw(HorizontalAxis), 0, Input.GetAxisRaw(VerticalAxis));

		if (direction != Vector3.zero)
		{
			rb.velocity += direction * accelationSpeed;
			Quaternion playerRotation = playerTransform.rotation;
			Quaternion directionRotation = Quaternion.LookRotation(direction);
			Quaternion targetRotation = Quaternion.Slerp(playerRotation, directionRotation, Time.deltaTime * rotateSpeed);
			playerTransform.rotation = targetRotation;
		}
		else
		{
			rb.velocity *= decelationSpeed;
		}
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

		forwardSpeed = rb.velocity.magnitude;

		anim.SetFloat(turnSpeedHash, turnSpeed);
		anim.SetFloat(forwardSpeedHash, forwardSpeed);
		anim.SetBool(pickUpHash, pickUp);
		anim.SetBool(action1Hash, action1);
	}
}
