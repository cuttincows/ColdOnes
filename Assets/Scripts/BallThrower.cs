using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class BallThrower : MonoBehaviour
{
    public int killY = -100;

    public GameObject ballPrefab = null;
    public float ballThrowSpeed = 1.0f;

    // Only public for now

    public GameObject heldColdOne;
    public Rigidbody heldRBody;

    public float holdDistance = 0.2f;
    private Camera mainCamera;
    public float minHoldDist = 0.3f;

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        bool clicked = Input.GetMouseButtonDown(0);

        if (clicked && !heldColdOne)
        {
            GameObject clickedCan;
            if (ClickedPickup(out clickedCan))
            {
                // On click, disable physics, and set to current held object
                heldColdOne = clickedCan;
				ColdOneController coldOne = clickedCan.GetComponentInParent<ColdOneController>();
				coldOne.Open();
                heldRBody = clickedCan.GetComponentInParent<Rigidbody>();

                heldRBody.isKinematic = true;
            }
        }

        if (heldColdOne)
        {
            // Move towards hold position

            Ray toRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Vector3 toPos = toRay.origin + toRay.direction * holdDistance;
            Quaternion toRot = Quaternion.identity;

            heldColdOne.transform.MoveTowards(toPos);


            if (clicked && Vector3.Distance(heldColdOne.transform.position, toPos) < minHoldDist)
                ThrowBall();
        }
        

        CheckKillY();
    }

    private bool ClickedPickup(out GameObject clickedCan)
    {
        clickedCan = null;

        Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit clickHit;

        if (Physics.Raycast(clickRay, out clickHit) && clickHit.collider.gameObject.CompareTag("Pickup"))
        {
            clickedCan = clickHit.collider.gameObject;
            return true;
        }

        return false;
    }

    private void CheckKillY()
    {
        List<GameObject> goList =
            (from tr
            in FindObjectsOfType<Transform>()
             where tr.position.y < killY
             select tr.gameObject)
        .ToList();

        foreach (GameObject go in goList)
            Destroy(go);
    }

    private void ThrowBall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // GameObject newBall = Instantiate(ballPrefab);
        //newBall.transform.position = Camera.main.transform.position;
        heldRBody.isKinematic = false;
        heldRBody.velocity = ray.direction * ballThrowSpeed;
        heldColdOne = null;
    }
}
