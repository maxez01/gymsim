using System;
using TMPro;
using UnityEngine;

public class DayMonthHandler : MonoBehaviour
{
    public SunMovement sunMovement;

    // Start is called before the first frame update
    void Start()
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

    public void Reset()
    {
        TMP_InputField[] inputs = this.GetComponentsInChildren<TMP_InputField>();
        foreach (TMP_InputField input in inputs)
        {
            if (input.name == "TagInput")
                input.text = sunMovement.day.ToString();
            if (input.name == "MonatInput")
                input.text = sunMovement.month.ToString();
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
