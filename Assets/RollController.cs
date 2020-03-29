using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollController : MonoBehaviour
{
	private Rigidbody rBody;

	public void Start()
	{
		rBody = GetComponent<Rigidbody>();
	}
}
