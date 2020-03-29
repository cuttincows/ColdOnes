using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceWithShattered : MonoBehaviour {

	public GameObject shatteredPrefab;

	private void OnCollisionEnter(Collision collision)
	{
		ColdOneController coldOne = collision.gameObject.GetComponent<ColdOneController>();
		if (coldOne)
		{
			GameObject shatteredObj = Instantiate(shatteredPrefab, transform.position, transform.rotation);
			Destroy(gameObject);

			foreach(Transform shatteredPiece in shatteredObj.transform)
			{
				shatteredPiece.GetComponent<Rigidbody>().AddForce(collision.relativeVelocity, ForceMode.Impulse);
			}
		}
	}
}
