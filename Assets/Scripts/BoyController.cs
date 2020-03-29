using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoyController : MonoBehaviour, I_Beerable {

	[HideInInspector] private bool hasBeenBeered = false;
	public string successText = "Thanks bro";
	Transform prompt;
	Text promptText;
	private Rigidbody rBody;

	public float forceScalar = 5.0f;


	public UnityEvent onBeered;

	public bool HasBeenBeered
	{
		get
		{
			return hasBeenBeered;
		}

		set
		{
			hasBeenBeered = value;
		}
	}

	public void GetBeered(Collision beerCollider)
	{
		promptText.text = successText;

        if (rBody != null) {
            rBody.isKinematic = false;
            rBody.AddForceAtPosition(beerCollider.relativeVelocity * forceScalar, beerCollider.transform.position);
        }

		onBeered.Invoke();
	}

	// Use this for initialization
	void Start () {
		prompt = transform.Find("ColdOnePrompt");
		promptText = GetComponentInChildren<Text>();
		rBody = GetComponent<Rigidbody>();
	}
}
