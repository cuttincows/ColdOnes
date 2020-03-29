using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static void MoveTowards(this Transform fromTr, Transform toTr, float factor = 5f)
    {
        MoveTowards(fromTr, toTr.position, factor);
    }

    public static void MoveTowards(this Transform fromTr, Vector3 toPos, float factor = 5f)
    {
        fromTr.position = Vector3.Lerp(fromTr.position, toPos, 1 - Mathf.Exp(-factor * Time.deltaTime));
    }
}
