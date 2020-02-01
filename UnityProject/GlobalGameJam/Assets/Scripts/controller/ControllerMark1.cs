using System.Linq;
using UnityEngine;

public class ControllerMark1 : MonoBehaviour
{
	private const string HorizontalAxis = "Horizontal";
	private const string VerticalAxis = "Vertical";
	private const string ActionButton = "Fire1";
	private const string PickUpButton = "Fire2";

	private int turnSpeedHash = Animator.StringToHash("turnSpeed");
	private int forwardSpeedHash = Animator.StringToHash("forwardSpeed");
	private int pickUpHash = Animator.StringToHash("pickUp");
	private int action1Hash = Animator.StringToHash("action1");
	private int interactibleLayerMask;

	private float turnSpeed;
	private float forwardSpeed;
	private bool pickUp;
	private bool action1;

	public float maxSpeed;
	public float decelationSpeed;
	public float accelationSpeed;
	public float rotateSpeed;
	public float pickUpRange;
	public int nbMaxPickUp;
	private float life;

	private Rigidbody rb;
	private Animator anim;
	private Transform playerTransform;

	private void Awake()
	{
		interactibleLayerMask = LayerMask.GetMask("Interactible");
		rb = GetComponentInChildren<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
		playerTransform = transform.GetChild(0).transform;
	}
	void Start()
	{

	}

	void Update()
	{
		if (Input.GetButton(PickUpButton))
		{
			Collider[] hits = Physics.OverlapSphere(transform.position, pickUpRange, interactibleLayerMask);
			Collider[] orderedHits = hits.OrderBy(c => Vector3.Distance(transform.position, c.transform.position)).ToArray();

			for (int i = 0; i < Mathf.Min(nbMaxPickUp, orderedHits.Length); i++)
			{
				//orderedHits[i].attachedRigidbody.GetComponent<IInteractible>();
			}
		}
	}

	private void FixedUpdate()
	{
		Vector3 direction = new Vector3(Input.GetAxisRaw(HorizontalAxis), 0, Input.GetAxisRaw(VerticalAxis));
		rb.velocity.Set(rb.velocity.x * decelationSpeed, rb.velocity.y, rb.velocity.z * decelationSpeed);

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
		}
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);

		forwardSpeed = rb.velocity.magnitude;

		anim.SetFloat(turnSpeedHash, turnSpeed);
		anim.SetFloat(forwardSpeedHash, forwardSpeed);
		anim.SetBool(pickUpHash, pickUp);
		anim.SetBool(action1Hash, action1);
	}
}
