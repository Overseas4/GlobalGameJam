using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footStepController : MonoBehaviour
{
	[SerializeField] Transform leftFoot;
	[SerializeField] Transform rightFoot;
	[SerializeField] Transform hips;

	private ControllerMark1 controller;

	private bool readyToPlayLeft;
	private bool readyToPlayRight;

	public float footStepThreshold;
	public float minRunSpeedForSound;

	private void Awake()
	{
		controller = GetComponentInParent<ControllerMark1>();
	}
	private void Update()
	{
		Vector3 leftFootPosition = leftFoot.position;
		Vector3 rightFootPosition = rightFoot.position;
		Vector3 hipsPosition = hips.position;

		bool playSound = false;
		if (readyToPlayLeft && (hipsPosition.y - leftFootPosition.y) < footStepThreshold)
		{
			readyToPlayLeft = false;
			playSound = true;
		}
		else if (!readyToPlayLeft && (hipsPosition.y - leftFootPosition.y) > footStepThreshold)
		{
			readyToPlayLeft = true;
		}

		if (readyToPlayRight && (hipsPosition.y - rightFootPosition.y) < footStepThreshold)
		{
			readyToPlayRight = false;
			playSound = true;
		}
		else if (!readyToPlayRight && (hipsPosition.y - rightFootPosition.y) > footStepThreshold)
		{
			readyToPlayRight = true;
		}

		if (playSound)
		{
			if (controller.IsInWater)
			{
				Debug.Log("splouch");
				eventController.Instance.FS_eau.Post(controller.gameObject);
			}
			else
			{
				if (controller.ForwardSpeed > minRunSpeedForSound)
				{
					Debug.Log("cour");
					eventController.Instance.FS_sable_course.Post(controller.gameObject);
				}
				else
				{
					Debug.Log("marche");
					eventController.Instance.FS_sable_marche.Post(controller.gameObject);
				}
			}
		}
	}
}
