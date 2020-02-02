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
	private int waterLayerMask;

	private float feetDistance = 1f;

	public float ForwardSpeed { get; private set; }
	public bool IsInWater { get; private set; }
	public float RefMaxSpeed { get; private set; }

	private bool pickUp;
	private bool action1;
	private float life;

	public float maxSpeed;
	public float decelationSpeed;
	public float accelationSpeed;
	public float rotateSpeed;
	public float pickUpRange;
	public int nbMaxPickUp;

	private Rigidbody rb;
	private Collider collider;
	private Animator anim;
	private Transform playerTransform;

	private void Awake()
	{
		RefMaxSpeed = maxSpeed;
		interactibleLayerMask = LayerMask.GetMask("Interactible");
		waterLayerMask = LayerMask.GetMask("Water");

		rb = GetComponentInChildren<Rigidbody>();
		collider = GetComponentInChildren<Collider>();
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
		float fallingSpeed = rb.velocity.y;
		Vector3 direction = new Vector3(Input.GetAxisRaw(HorizontalAxis), 0, Input.GetAxisRaw(VerticalAxis));

		Vector3 velocity = new Vector3(rb.velocity.x * decelationSpeed, fallingSpeed, rb.velocity.z * decelationSpeed);

		if (direction != Vector3.zero)
		{
			velocity += direction * accelationSpeed;
			velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

			Quaternion playerRotation = playerTransform.rotation;
			Quaternion directionRotation = Quaternion.LookRotation(direction);
			Quaternion targetRotation = Quaternion.Slerp(playerRotation, directionRotation, Time.deltaTime * rotateSpeed);
			playerTransform.rotation = targetRotation;
		}

		rb.velocity = new Vector3(velocity.x, fallingSpeed, velocity.z);

		ForwardSpeed = new Vector3(velocity.x, 0, velocity.z).magnitude;

		anim.SetFloat(forwardSpeedHash, ForwardSpeed);
		anim.SetBool(pickUpHash, pickUp);
        //anim.SetBool(action1Hash, action1);
		//IsInWater = Physics.Raycast(transform.position, Vector3.down * feetDistance, waterLayerMask);
	}
}
