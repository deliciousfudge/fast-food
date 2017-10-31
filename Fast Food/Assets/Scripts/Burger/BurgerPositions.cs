using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerPositions : MonoBehaviour {

public GameObject[] Burgers;
private BurgerLaps Lap;
private BurgerCheckpoint Checkpoint;
private List<int> ProgressValues;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (GameObject Burger in Burgers)
		{
			Checkpoint = Burger.GetComponentInChildren<BurgerCheckpoint>();
            ProgressValues.Add(Checkpoint.GetProgressValue());
		}
	}
}
