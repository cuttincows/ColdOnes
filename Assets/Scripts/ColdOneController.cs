using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ColdOneController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Open()
	{
		ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
		particleSystem.Play();

		StartCoroutine("LerpCan");
	}

	IEnumerator LerpCan()
	{
		float cur = 0f, max = 0.3f, i;

		Transform tabTransform = transform.Find("Model").Find("TabPivot");

		Quaternion fromRot = tabTransform.localRotation;
		Quaternion toRot = transform.Find("Model").Find("ToLerp").transform.localRotation;

		while (cur < max)
		{
			yield return null;
			cur += Time.deltaTime;

			i = Ease.SineIn(cur / max);
			tabTransform.rotation = Quaternion.Lerp(fromRot, toRot, i);
		}
		tabTransform.rotation = toRot;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.collider)
			return;

		I_Beerable beerable = collision.collider.GetComponentInParent<I_Beerable>();
		if (beerable == null)
			return;

		if (!beerable.HasBeenBeered)
		{
			beerable.HasBeenBeered = true;
			beerable.GetBeered(collision);
		}
	}
}
