using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInNextBoy : MonoBehaviour {
	
	public BoyMovementTarget nextTargetMover;
	public float nextTargetDelay = 3.0f;

	public void MoveInBoy()
	{
		nextTargetMover.moveIntoPosition();
	}
}
