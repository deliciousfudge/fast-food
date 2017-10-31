// Contents of this script adapted from that found at:
// http://answers.unity3d.com/questions/546233/simple-checpoint-system-in-3d-racing-game-some-iss.html

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BurgerCheckpoint : MonoBehaviour
{
    public Text LapLabel;
    public Text PositionLabel;
    private BurgerLaps Burger;
    private int ProgressValue = 0;
    private static int LAP_VALUE = 1000;
    private static int CHECKPOINT_VALUE = 100;

    void Start()
    {
        Burger = gameObject.GetComponent<BurgerLaps>();
    }

    void Update()
    {
        LapLabel.text = "Lap " + (Burger.currentLap) + " Checkpoint " + (Burger.currentCheckpoint);
        PositionLabel.text = "1st";
    }

    void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Checkpoint" + (Burger.currentCheckpoint + 1)) )
        {
            //Check so we dont exceed our checkpoint quantity
            if (Burger.currentCheckpoint + 1 < Burger.checkpointA.Length)
            {
                Burger.currentCheckpoint++;
                ProgressValue += CHECKPOINT_VALUE;
            }
            else
            {
                //If we dont have any Checkpoints left, go back to 0
                Burger.currentCheckpoint = 0;
                
                // Since we've exhausted all available checkpoints, we also want to increase the lap count 
                Burger.currentLap++;
                ProgressValue += LAP_VALUE;
            }
        }
    }

    public int GetProgressValue()
    {
        return ProgressValue;
    }
}