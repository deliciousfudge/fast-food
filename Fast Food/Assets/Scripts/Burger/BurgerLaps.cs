// Contents of this script sourced from:
// http://answers.unity3d.com/questions/546233/simple-checpoint-system-in-3d-racing-game-some-iss.html

using UnityEngine;
using System.Collections;

public class BurgerLaps : MonoBehaviour
{
    // These Static Variables are accessed in "checkpoint" Script
    public Transform[] checkpointArray;
    public Transform[] checkpointA;
    public int currentCheckpoint = 0;
    public int currentLap = 0;
    public Vector3 startPos;
    public int Lap;

    void Start()
    {
        startPos = transform.position;
        currentCheckpoint = 0;
        currentLap = 1;
    }

    void Update()
    {
        Lap = currentLap;
        checkpointA = checkpointArray;
    }
}