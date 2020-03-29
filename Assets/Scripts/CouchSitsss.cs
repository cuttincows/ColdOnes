using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchSitsss : MonoBehaviour {

	private bool hasBeenBoyed = false;
	public float launchForce = 100f;

	public BoyMovementTarget nextLad;

	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.collider)
			return;

		BoyController boy = collision.collider.GetComponentInParent<BoyController>();
		if (!boy)
			return;

		if (!hasBeenBoyed)
			GetBoyedOn();
	}

	private void GetBoyedOn()
	{
		hasBeenBoyed = true;
		StartCoroutine("TheBoyCommences");
	}

	private IEnumerator TheBoyCommences()
	{
		yield return new WaitForSeconds(2.0f);
		GetComponent<Rigidbody>().AddForce(Vector3.down * launchForce, ForceMode.VelocityChange);

		GetComponentInChildren<ParticleSystem>().Play();

		yield return new WaitForSeconds(3.0f);

	}
}
