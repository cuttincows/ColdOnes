using System.Linq;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

[SelectionBase]
public class OvenDoorControoler : MonoBehaviour, I_Beerable
{
	private Rigidbody rBody;
    private Vector3 baseOrientation;
    public Transform forcePoint;
	public GameObject promptObj;

	public float forceScalar = 500f;
	public float inhalationForce = 5.0f;

	public Transform ovenCenterTr;

	public GameObject inhalationParticleEffect;

	private bool isInhaling = false;
	private float inhalationCounterCur = 0f;
	private float inhalationCounterMax = 5.0f;
	private List<Rigidbody> inhalationTargets = new List<Rigidbody>();


	private bool hasBeenBeered = false;
	public float waitToDisplayTextDur = 1.5f;
	private bool promptDisplaying = false;
    public float rollDropScalar = 1.0f;
    private float doorStopSwingMargin;
    public BoyMovementTarget godMover;
    private GameObject activationBeer;

    public bool HasBeenBeered
	{
		get
		{
			return hasBeenBeered;
		}
		set
		{
            if (!hasBeenBeered && value) {
                // Prevent the Cold One(tm) from jamming the oven
                Physics.IgnoreLayerCollision(9, 10);
            }
			hasBeenBeered = value;
		}
	}

	// Use this for initialization
	void Start()
	{
		rBody = GetComponent<Rigidbody>();
        baseOrientation = transform.up;
	}

	// Update is called once per frame
	void Update()
	{
		if (!isInhaling)
		{
			return;
		}

		rBody.AddForceAtPosition(
			forcePoint.forward * forceScalar,
			forcePoint.position
		);

		foreach (Rigidbody target in inhalationTargets)
		{
			if (!target)
				continue;
			//print(target);
			//print(target.transform);
			Vector3 dirToOven = (
				ovenCenterTr.position - target.transform.position)
                .normalized;

            //GameObject.Find("TestSphere").transform.position = ovenCenterTr.position;

			target.AddForce(dirToOven * inhalationForce);
		}
	}

	public void GetBeered(Collision beerCollider)
	{
		if (!promptDisplaying) return;
		HasBeenBeered = true;
		isInhaling = true;
		// Start vacuuming up all the pizza rolls

		inhalationTargets = (from roll in FindObjectsOfType<RollController>()
							 select roll.GetComponent<Rigidbody>())
								.ToList();
		inhalationParticleEffect.SetActive(true);
		foreach (Rigidbody targetRB in inhalationTargets)
		{
			targetRB.useGravity = false;
		}
        // Maybe suck up the boys' feet, and make them grab the ground???

        ovenCenterTr.SetParent(null);
        StartCoroutine(InhalationFinishedSequence(inhalationTargets, inhalationParticleEffect.GetComponent<ParticleSystem>().main.duration * rollDropScalar));
        activationBeer = beerCollider.gameObject;
	}

    private IEnumerator InhalationFinishedSequence(List<Rigidbody> inhalationTargets, float v)
    {
        yield return new WaitForSeconds(v);
        foreach (Rigidbody inhalationTarget in inhalationTargets)
        {
            if (inhalationTarget == null) continue;
            inhalationTarget.useGravity = true;
        }
        while (Vector3.Dot(transform.up, baseOrientation) < doorStopSwingMargin)
        {
            yield return null;
        }
        transform.up = baseOrientation;
        isInhaling = false;

        yield return new WaitForSeconds(1.0f);
        foreach(Rigidbody target in inhalationTargets)
        {
            Destroy(target.gameObject);
        }
        Destroy(activationBeer);
        inhalationTargets.Clear();
        SpawnTheGod();
    }

    private void SpawnTheGod()
    {
        godMover.moveIntoPosition();
    }

    public void DisplayText()
	{
		StartCoroutine(WaitToDisplayText(waitToDisplayTextDur));
	}

	private IEnumerator WaitToDisplayText(float waitDur)
	{
		yield return new WaitForSeconds(waitDur);

		promptDisplaying = true;
		promptObj.SetActive(true);

		// Allow door to swing
		rBody.isKinematic = false;
	}
}