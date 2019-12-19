using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobocheController : MonoBehaviour
{
	[SerializeField] float rotationSpeed;
	[SerializeField] float movingSpeed;
	void Update()
	{
		float axis = rotationSpeed * Input.GetAxis("Horizontal");
		float forward = movingSpeed * Input.GetAxis("Vertical");
		transform.Rotate(0f, axis, 0f);
		transform.position += transform.forward * forward;
	}
}
