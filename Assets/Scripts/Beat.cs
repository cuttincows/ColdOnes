using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A beat is a moment in the story, with a begin and an end. 
// Implement this class with the details of the middle, and add 
// to a BeatManager in the scene to call to
public abstract class Beat : MonoBehaviour
{
    public bool IsComplete { get; private set; } = false;

    public virtual void StartBeat()
    {

    }

    public virtual void EndBeat()
    {
        IsComplete = true;
    }
}
