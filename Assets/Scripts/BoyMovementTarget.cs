using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyMovementTarget : MonoBehaviour
{
	public BoyController boy;

	public Ease.DelegateType delType;
	public Ease.EaseType easeType;

	public float moveForce = 5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, 0.25f);

		if (boy)
		{
			Gizmos.DrawLine(transform.position, boy.transform.position);
		}
	}

	public void moveIntoPosition()
	{
		StartCoroutine(HandleMove());
	}

	private IEnumerator HandleMove()
	{
		float cur = 0f, max = 3f, i;

		Vector3 boyStartPos = boy.transform.position;
		Vector3 boyMoveDir = (boy.transform.position - transform.position).normalized;

		while (cur < max)
		{
			cur += Time.deltaTime;
			yield return null;

			i = Ease.GetDelegate(delType, easeType)(cur / max);
			boy.transform.position = Vector3.Lerp(boyStartPos, transform.position, i);

			foreach(Rigidbody rbody in boy.transform.GetComponentsInChildren<Rigidbody>())
			{
				rbody.AddForce(boyMoveDir * moveForce);
			}
		}
	}
}
