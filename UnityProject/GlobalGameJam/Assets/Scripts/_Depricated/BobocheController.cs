using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobocheController : MonoBehaviour
{
	[SerializeField] float rotationSpeed = 1.5f;
	[SerializeField] float movingSpeed = 0.2f;
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    void Update()
	{
		float axis = rotationSpeed * Input.GetAxis(Horizontal);
		float forward = movingSpeed * Input.GetAxis(Vertical);
		transform.Rotate(0f, axis, 0f);
		transform.position += transform.forward * forward;
	}
}
