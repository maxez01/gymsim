using UnityEngine;
using TMPro;
using System;

public class DayTimeDisplay : MonoBehaviour
{
    public SunMovement sunMovement;
    public static DateTime currentDateTime;

    // Update is called once per frame
    void Update()
    {
        // 0-based, as sun angle starts at 0 (= sunrise), needs to be offset by sunriseHour
        int currentHour = (int)Math.Floor(sunMovement.angle / 360 * 24);
        int currentMinute = (int)Math.Floor(sunMovement.angle / 360 * 24 * 60 % 60);

        int sunriseHour = 8;

        currentHour = (currentHour + sunriseHour) % 24;

        currentDateTime = new DateTime(
            2022, 
            sunMovement.month, 
            sunMovement.day, 
            currentHour, 
            currentMinute, 
            0 /* seconds */
            );

        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.SetText(currentDateTime.Hour.ToString("00") + ":" + currentDateTime.Minute.ToString("00"));
    }
}
