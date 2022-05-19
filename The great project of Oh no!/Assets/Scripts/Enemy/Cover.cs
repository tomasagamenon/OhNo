using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    //Esto lo tienen las coberturas para determinar en que lugares se puede colocar un enemigo y que tanto cubre
    public enum CoverQuality { Low, Medium, High }
    public CoverQuality coverQuality;
    public List<Transform> positions;
    public bool[] isTaken;
    public bool isFull;

    public void CoverTaken(Transform position, bool taken)
    {
        int i = positions.IndexOf(position);
        isTaken[i] = taken;
    }
}
