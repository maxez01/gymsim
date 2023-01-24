using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightManager : MonoBehaviour
{
    public Light sun, lamp, spot, moon;
    bool flicker = false;
    float flickerMoment;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DayTimeManager.currentDateTime.TimeOfDay < new TimeSpan(8, 15, 0) || DayTimeManager.currentDateTime.TimeOfDay > new TimeSpan(20, 15, 0))
            lamp.gameObject.SetActive(true);
        else
            lamp.gameObject.SetActive(false);

        if (DayTimeManager.currentDateTime.TimeOfDay < new TimeSpan(8, 0, 0) || DayTimeManager.currentDateTime.TimeOfDay > new TimeSpan(20, 0, 0))
        {
            sun.intensity = 0;
            moon.intensity = 0.1f;
        }
        else
        {
            sun.intensity = 1;
            moon.intensity = 0;
        }

        if (DayTimeManager.currentDateTime.TimeOfDay < new TimeSpan(8, 0, 0) || DayTimeManager.currentDateTime.TimeOfDay > new TimeSpan(21, 0, 0))
            spot.intensity = 8;
        else
            spot.intensity = 0;

        if (DayTimeManager.currentDateTime.TimeOfDay == new TimeSpan(21, 0, 0))
        {
            flicker = true;
            flickerMoment = Time.time;
        }

        if(flicker)
            SetSpotFlicker();
    }

    private void SetSpotFlicker()
    {
        float secondsPassed = Time.time - flickerMoment;
        if (secondsPassed < 2.5)
        {
            if (secondsPassed <= 1.8)
            {
                spot.intensity = 0;
            }

            if (secondsPassed <= 1.2)
            {
                spot.intensity = 12;
            }

            if (secondsPassed <= 1)
            {
                spot.intensity = 0;
            }

            if (secondsPassed <= 0.5)
            {
                spot.intensity = 8;
            }

            if (secondsPassed >= 2.4)
            {
                spot.intensity = 8;
                flicker = false;
            }
        }
    }
}
