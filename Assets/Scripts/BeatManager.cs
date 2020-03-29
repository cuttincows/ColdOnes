using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeatManager : MonoBehaviour
{
    public int curBeatIndex = 0;
    public List<Beat> beats;

    public void Update()
    {
        if (beats[curBeatIndex].IsComplete)
        {
            beats[curBeatIndex].EndBeat();
            curBeatIndex++;
            if (curBeatIndex < beats.Count)
            {
                beats[curBeatIndex].StartBeat();
            }
        }
    }
}
