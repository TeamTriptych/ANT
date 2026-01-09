using TMPro;
using UnityEngine;

public class TimerBehavior : MonoBehaviour
{
    //ref to the GameManager
    GameManagerBehavior gameManager;
    //variable for tracking total amount of seconds elapsed since game start. Incremented using gameManager's frameCounter
    public int secondsElapsed = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //assign ref to game Manager
        gameManager = GameManagerBehavior.singleton;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if 60 frames have passed, update secondsElapsed
        if (gameManager.frameCounter % 60 == 0)
        {
            secondsElapsed = secondsElapsed + 1;
        }
        //finally, update the timer display according to secondsElapsed
        this.gameObject.GetComponent<TextMeshProUGUI>().text = formatSecondsToTime(secondsElapsed);
        //and store that time in the gameManager
        gameManager.finalTime = formatSecondsToTime(secondsElapsed);

        // -- TOAST INTEGRITY --

        //write current toast integrity to the game manager
        gameManager.toastIntegrity = currentToastIntegrity();
    }
    string formatSecondsToTime(int totalSeconds)
    {
        //first, divide by 60 and throw away the non-integer part to get minutes
        int minutesToDisplay = Mathf.FloorToInt(totalSeconds / 60);
        //then, convert that minutes number back into seconds and subtract those seconds from totalSeconds to get the sub-minute seconds
        int subMinuteSeconds = totalSeconds - (minutesToDisplay * 60);
        //finally, format the subMinute seconds for display
        string secondsToDisplay;
        //If less than 10 subMinuteSeconds are remaining, then add a 0 to the display
        if (subMinuteSeconds < 10)
        {
            secondsToDisplay = "0" + subMinuteSeconds;
        }
        //else if there are 10 or more subMinuteSeconds, convert them directly to the display string
        else
        {
            secondsToDisplay = subMinuteSeconds.ToString();
        }
        //concatenate the info
        string timeToDisplay = minutesToDisplay + ":" + secondsToDisplay;
        //finally, return the time to display
        return timeToDisplay;
    }
    float currentToastIntegrity()
    {
        //start at 1
        float baseIntegrity = 1;
        //find the relationship of how close to 0 integrity we are (targetTimeInSeconds / secondsElapsed)
        float percentDecayed = (float)secondsElapsed / (float)gameManager.targetTimeInSeconds;
        //subtract that proportion from 1 to find our current "integrity" as a percent
        float currentIntegrity = baseIntegrity - percentDecayed;
        //multiply currentIntegrity by 100 to make it a whole number between 0-100, then discard any decimal parts left
        return Mathf.Round(currentIntegrity * 100);
    }
}
