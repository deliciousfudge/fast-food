using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerPositions : MonoBehaviour {

public GameObject[] Burgers;
private BurgerLaps CurrentBurger;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (GameObject Burger in Burgers)
		{
			CurrentBurger = Burger.GetComponentInChildren<BurgerLaps>();
		}
	}
}
