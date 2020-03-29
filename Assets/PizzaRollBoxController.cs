using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaRollBoxController : MonoBehaviour {

	public GameObject rollPrefab;
	public int numRolls = 90;

	public void Drop()
	{
		GetComponent<Rigidbody>().isKinematic = false;
		transform.SetParent(null);

		for(int i = 0; i < numRolls; ++i)
		{
			Instantiate(rollPrefab);
			rollPrefab.transform.position = transform.position;
		}
	}
}
