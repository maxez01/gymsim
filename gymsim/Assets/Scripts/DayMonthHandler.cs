using System;
using TMPro;
using UnityEngine;

public class DayMonthHandler : MonoBehaviour
{
    public SunMovement sunMovement;
    public TMP_InputField dayInput;
    public TMP_InputField monthInput;

    // Start is called before the first frame update
    void Start()
    {
        dayInput.text = DateTime.Now.Day.ToString();
        monthInput.text = DateTime.Now.Month.ToString();
    }

    public void Reset()
    {
        dayInput.text = sunMovement.day.ToString();
        monthInput.text = sunMovement.month.ToString();
    }

    public void HandleDayUpdate(string text)
    {
        sunMovement.day = int.Parse(text);
    }

    public void HandleMonthUpdate(string text)
    {
        sunMovement.month = int.Parse(text);
    }
}
