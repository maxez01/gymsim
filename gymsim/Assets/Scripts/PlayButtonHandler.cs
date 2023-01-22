using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonHandler : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public void HandlePlayButtonClick()
    {
        if (!gameIsPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        gameIsPaused = !gameIsPaused;
    }
}
