using System.Collections;
using System.Linq;
using UnityEngine;
public class ControllerMark1 : MonoBehaviour
{
	private const string HorizontalAxis = "Horizontal";
	private const string VerticalAxis = "Vertical";
	private const string ActionButton = "Fire1";
	private const string PickUpButton = "Fire1";

	private int turnSpeedHash = Animator.StringToHash("turnSpeed");
	private int forwardSpeedHash = Animator.StringToHash("forwardSpeed");
	private int pickUpHash = Animator.StringToHash("pickUp");
	private int action1Hash = Animator.StringToHash("action1");
	private int interactibleLayerMask;
	private int waterLayerMask = 4;

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
	public float timeOfWaitForPickUp;
	public float timeToWaitBeforeNextPickUp;

	private Rigidbody rb;
	private Collider collider;
	private Animator anim;
	private Transform playerTransform;
	private Transform objectToInteractTransform;
	private bool isPickUping;
	private bool onPickUpCoolDown;

	private void Awake()
	{
		RefMaxSpeed = maxSpeed;
		interactibleLayerMask = LayerMask.GetMask("Interactible");

		rb = GetComponentInChildren<Rigidbody>();
		collider = GetComponentInChildren<Collider>();
		anim = GetComponentInChildren<Animator>();
		playerTransform = transform.GetChild(0).transform;
	}

	private void FixedUpdate()
	{
		float fallingSpeed = rb.velocity.y;
		Vector3 velocity = new Vector3(rb.velocity.x * decelationSpeed, fallingSpeed, rb.velocity.z * decelationSpeed);

		if (!isPickUping)
		{
			if (Input.GetButton(ActionButton))
			{
				Collider[] hits = Physics.OverlapSphere(transform.position, pickUpRange, interactibleLayerMask);
				Collider[] orderedHits = hits.OrderBy(c => Vector3.Distance(transform.position, c.transform.position)).ToArray();

				bool actionAllreadyDone = false;
				bool willPickSand = false;
				bool willPickWetSand = false;
				IInteractible selectedWater = null;
				IInteractible selectedSand = null;

				if (orderedHits.Length > 0)
				{
					StartCoroutine(WaitForPickUp());
				}

				for (int i = 0; i < orderedHits.Length; i++)
				{
					var interactible = orderedHits[i].GetComponent<IInteractible>();
					if (interactible != null)
					{
						var destructible = orderedHits[i].GetComponent<IDestructible>();
						if (destructible != null)
						{
							var currentSelected = UIInventory.Instance.GetCurrentSelected();
							if (currentSelected != null)
							{
								objectToInteractTransform = null;
								StartCoroutine(WaitForRepair());

								destructible.RepairDamage(UIInventory.Instance.GetCurrentSelected().repairValue);
								UIInventory.Instance.RemoveInstance(currentSelected);
								actionAllreadyDone = true;
								break;
							}
						}

						var iventoryItem = orderedHits[i].GetComponent<IIventoryItem>();
						if (iventoryItem != null)
						{
							switch (iventoryItem.Type)
							{
								case ItemType.Wood:
									objectToInteractTransform = orderedHits[i].transform;
									StartCoroutine(WaitForPickUp());
									orderedHits[i].GetComponent<IInteractible>().Interact();
									actionAllreadyDone = true;
									break;

								case ItemType.Water:
									selectedWater = orderedHits[i].GetComponent<IInteractible>();
									willPickWetSand = true;
									break;

								case ItemType.Sand:
									selectedSand = orderedHits[i].GetComponent<IInteractible>();
									willPickSand = true;

									break;

								case ItemType.SuperWood:
									objectToInteractTransform = orderedHits[i].transform;
									StartCoroutine(WaitForPickUp());
									orderedHits[i].GetComponent<IInteractible>().Interact();
									actionAllreadyDone = true;
									break;
								default:
									break;
							}

							if (actionAllreadyDone)
							{
								break;
							}
						}
					}
				}

				if (!actionAllreadyDone)
				{
					bool hasSand = false;
					for (int i = 0; i < Inventory.Instance.Items.Count; i++)
					{
						if (Inventory.Instance.Items[i].type == ItemType.Sand)
						{
							hasSand = true;
							break;
						}
					}
					if (hasSand)
					{
						if (willPickWetSand)
						{
							if (selectedWater != null)
							{
								selectedWater.Interact();
								StartCoroutine(WaitForPickUp());
							}
						}
					}
					else
					{
						if (willPickSand)
						{
							if (selectedSand != null)
							{
								selectedSand.Interact();
								StartCoroutine(WaitForPickUp());
							}
						}
					}
				}
			}

			Vector3 direction = new Vector3(Input.GetAxisRaw(HorizontalAxis), 0, Input.GetAxisRaw(VerticalAxis));

			if (direction != Vector3.zero)
			{
				velocity += direction * accelationSpeed;
				velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

				Quaternion playerRotation = playerTransform.rotation;
				Quaternion directionRotation = Quaternion.LookRotation(direction);
				Quaternion targetRotation = Quaternion.Slerp(playerRotation, directionRotation, Time.deltaTime * rotateSpeed);
				playerTransform.rotation = targetRotation;
			}
		}

		rb.velocity = new Vector3(velocity.x, fallingSpeed, velocity.z);

		ForwardSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
		anim.SetFloat(forwardSpeedHash, ForwardSpeed);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == waterLayerMask)
		{
			IsInWater = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == waterLayerMask)
		{
			IsInWater = false;
		}
	}

	IEnumerator WaitForPickUp()
	{
		float timer = 0f;
		anim.SetTrigger(pickUpHash);
		isPickUping = true;
		onPickUpCoolDown = true;
		while (timer < timeOfWaitForPickUp)
		{
			timer += Time.deltaTime;

			if (objectToInteractTransform != null)
			{
				Quaternion playerRotation = playerTransform.rotation;
				Vector3 targetDirection = objectToInteractTransform.position - playerTransform.position;
				Quaternion directionRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
				Quaternion targetRotation = Quaternion.Slerp(playerRotation, directionRotation, Time.deltaTime * rotateSpeed);
				playerTransform.rotation = targetRotation;
			}

			yield return null;
		}
		isPickUping = false;
		timer = 0f;
		while (timer < timeToWaitBeforeNextPickUp)
		{
			timer += Time.deltaTime;
		}
		onPickUpCoolDown = true;
	}

	IEnumerator WaitForRepair()
	{
		float timer = 0f;
		anim.SetTrigger(pickUpHash);
		isPickUping = true;
		onPickUpCoolDown = true;
		while (timer < timeOfWaitForPickUp)
		{
			timer += Time.deltaTime;

			if (objectToInteractTransform != null)
			{
				Quaternion playerRotation = playerTransform.rotation;
				Vector3 targetDirection = objectToInteractTransform.position - playerTransform.position;
				Quaternion directionRotation = Quaternion.LookRotation(new Vector3(targetDirection.x, 0, targetDirection.z));
				Quaternion targetRotation = Quaternion.Slerp(playerRotation, directionRotation, Time.deltaTime * rotateSpeed);
				playerTransform.rotation = targetRotation;
			}

			yield return null;
		}
		isPickUping = false;
		timer = 0f;
		while (timer < timeToWaitBeforeNextPickUp)
		{
			timer += Time.deltaTime;
		}
	}
}
