using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupBaseScript : MonoBehaviour
{
    /*
    //Hidden Letter pickup - method for to see what letters the player has picked up.
    //Takes the player's score as a parameter to add reward if all letters are found.
    void hiddenLetterPickup(int playerScore)
    {
        playerScore = playerScore * 1.5f;
        return playerScore;
    }

    //Bonus Point Pickup - method to add bonus points to the player's current combo.
    //Parameters - currentCombo (current skill streak the player is in), bonusPoints (The amount of points needed to be added to the current combo).
    void bonusPointPickup(int currentCombo, int bonusPoints)
    {
        currentCombo = currentCombo + bonusPoints;
        return currentCombo;
    }

    //Score Multiplier Pickup - method to increase the player's score by the multiplier specified in the parameter.
    //Takes the player's current combo as a parameter so the current run is multiplied rather than the player's total score.
    void scoreMultiplierPickup(int currentCombo, int multiplier)
    {
        currentCombo = currentCombo * multiplier;
        return currentCombo;
    }

    //Speed Increase Pickup - method to increase the players current speed.
    //Parameters unknown at the moment.
    void speedIncreasePickup(int runSpeed_f)
    {
        runSpeed_f = runSpeed_f * 2;
        return runSpeed_f;
    }

    //Contract Pickup - method to trigger the contract challenges.
    //Parameters unknown at the moment.
    void contractPickup(int currentCombo, int multiplier, int bonusPoints)
    {
        if (OnTriggerEnter.collider.tag == "player")
        {
            currentCombo = (currentCombo + bonusPoints) * multiplier;
            return currentCombo;
            
        }
    }
    //When the trigger starts
    void OnTriggerEnter(Collider player)
    {
        print("OnTriggerEnter");
    }

    //When it is always triggered, it is always in the trigger area
    void OnTriggerStay(Collider player)
    {
        print("OnTriggerStay");
    }

    //When the trigger ends
    void OnTriggerExit(Collider player)
    {
        print("OnTriggerExit");
    }

    
    //Extra Time Pickup - method to give the player extra time during the contract.
    //Parameter unknown at the moment.
    void extraTime(int Time, int time, int extraTime)
    {
        Time = time + extraTime;
        extraTime = 30;
        time = Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        hiddenLetterPickup(int playerScore);
        bonusPointPickup(int currentCombo, int bonusPoints);
        scoreMultiplierPickup(int currentCombo, int multiplier);
        contractPickup(int currentCombo, int multiplier, int bonusPoints);
        speedIncreasePickup(int runSpeed_f);
        
    }

    //Basic pickup script, this will destroy the pickup once the player comes into contact with the pickup.
    void OnCollisionEnter(Collision other)
    {
        //Check if the pickup is for a score multiplier. If so the player's current combo and multiplier will be used as a parameter for the method 'scoreMultiplierPickup'.
        if (other.gameObject.CompareTag("Score Multiplier Pickup")) {
            Destroy(other.gameObject);
        }
    }*/
}
