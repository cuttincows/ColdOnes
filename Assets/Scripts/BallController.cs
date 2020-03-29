using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject hitPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        //GameObject particleSystem = Instantiate(hitPrefab);
        //particleSystem.transform.position = collision.contacts[0].point;
        //particleSystem.transform.forward = collision.contacts[0].normal;
        //StartCoroutine(DestroyParticle(particleSystem));
    }

    private IEnumerator DestroyParticle(GameObject particleSystem)
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(particleSystem);
    }
}
