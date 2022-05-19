using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianPoints : MonoBehaviour
{
    public Transform[] wayPoints1;
    public Transform[] wayPoints2;
    public Transform[] wayPoints3;
    public Transform[] wayPoints4;
    public Transform[] wayPoints5;
    public Transform[] wayPoints6;

    public PedestrianScript[] pedestrians;
    private void Awake()
    {
        for (int i = 0; i < pedestrians.Length; i++)
        {
            int number = Random.Range(1, 3);
            Debug.Log("para " + pedestrians[i] + " tocó el camino " + number);
            if (number == 1) pedestrians[i].wayPoints = wayPoints1;
            else if (number == 2) pedestrians[i].wayPoints = wayPoints2;
            else if (number == 3) pedestrians[i].wayPoints = wayPoints3;
            else if (number == 4) pedestrians[i].wayPoints = wayPoints4;
            else if (number == 5) pedestrians[i].wayPoints = wayPoints5;
            else if (number == 6) pedestrians[i].wayPoints = wayPoints6;
        }
    }
}
