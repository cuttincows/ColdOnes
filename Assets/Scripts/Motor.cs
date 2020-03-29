using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(CharacterJoint))]
public class Motor : MonoBehaviour
{
    [HideInInspector] public Quaternion targetRotation;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.up);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Motor))]
public class MotorEditor : Editor
{
    private void OnSceneGUI()
    {
        Motor motor = target as Motor;
        CharacterJoint joint = motor.GetComponent<CharacterJoint>();

        Vector3 handlePos = motor.transform.position + joint.anchor;
        motor.targetRotation = Handles.RotationHandle(motor.targetRotation, handlePos);
        
    }
}
#endif