using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DayMonthHandler : MonoBehaviour
{
    public SunMovement sunMovement;

    // Start is called before the first frame update
    public void Start()
    {
        TMP_InputField[] inputs = this.GetComponentsInChildren<TMP_InputField>();
        foreach (TMP_InputField input in inputs)
        {
            if (input.name == "TagInput")
                input.text = DateTime.Now.Day.ToString();
            if (input.name == "MonatInput")
                input.text = DateTime.Now.Month.ToString();
        }
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
