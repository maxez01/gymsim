using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SunMovement : MonoBehaviour
{
    public float speed = 0.01f; // 0.01
    private float xRot, yRot;
    public float baseRotation = 0.15f;
    public float solsticeTolerance = 0.10f;
    public int day;
    public int month;
    public float angle = 0.0f;
    public Vector3 axis;
    public TMP_InputField dayInput;
    public TMP_InputField monthInput;
    public Light moon;

    void Start() {
        transform.rotation = Quaternion.AngleAxis(0, Vector3.zero);
        day = DateTime.Now.Day;
        month = DateTime.Now.Month;
        SetRot();
    }

    void Update()
    {
        SetRot();

        transform.Rotate(new Vector3(xRot * Time.deltaTime, -yRot * Time.deltaTime ,0), speed);
        transform.rotation.ToAngleAxis(out angle, out axis);
        moon.transform.rotation = Quaternion.AngleAxis(-angle, axis);

        // kleiner Hack um den Slider wieder von vorne anfangen zu lassen
        if (angle == 360)
        {
            angle = 0;
            transform.rotation = Quaternion.AngleAxis(angle, axis);
        }
    }
    public void Reset()
    {
        transform.rotation = Quaternion.AngleAxis(0, Vector3.zero);
        SetRot();
    }

    /*
    *   Zur Wintersonnenwende (21.12) muss die Neigung der Sonne am größten sein
    *   Zur Sommersonnenwende (21.06) muss die Neigung der Sonne am geringsten sein 
    *   Zum Äquinoktium (20.03 und 22.09) muss x und y Rotation gleich sein, da Tagundnachtgleiche herrscht
    */
    private float CalculateSunRotation() {
        DateTime dt = new DateTime(2022, month, day);

        // 01.01 - 21.06
        if (dt.DayOfYear + 10 >= 0 && dt.DayOfYear <= 172) {
            return (baseRotation - solsticeTolerance) + ((172 - dt.DayOfYear) / 172f) * 0.2f;
        // 22.06 - 21.12
        } else if (dt.DayOfYear >= 173 && dt.DayOfYear <= 355) {
            return (baseRotation - solsticeTolerance) + ((dt.DayOfYear - 173) / 182f) * 0.2f;
        // 22.12 - 31.12
        } else {
            for(int i = 1, j = 356; i <= 10; i++, j++) {
                if (j == dt.DayOfYear) {
                    return (baseRotation + solsticeTolerance) - ((i / 177f) * 0.02f);
                }
            }
        }
        return baseRotation;
    }

    public bool CheckDayAndMonthValue() {
        int[] monthsWith31Days = { 1, 3, 5, 7, 8, 10, 12 };
        int[] monthsWith30Days = { 4, 6, 9, 11 };
        int monthWith28Days = 2;

        if(Array.Find(monthsWith31Days, month31Days => month31Days == month) > 0 && day >= 1 && day <= 31
        || Array.Find(monthsWith30Days, month30Days => month30Days == month) > 0 && day >= 1 && day <= 30
        || month == monthWith28Days && day >= 1 && day <= 28
        ) {
            return true;
        } else if((month < 0 || month > 12)) {
            Debug.Log("Enter valid month value (1-12)");
            return false;
        } else if (day < 0 || day > 31) {
            Debug.Log("Enter valid day value (1-31)");
            return false;
        } else {
            Debug.Log("Month " + month + " of the year does not have " + day + " days");
            return false;
        }
    }

    private void SetRot()
    {
        if (CheckDayAndMonthValue())
        {
            xRot = baseRotation;
            yRot = CalculateSunRotation();
        }
        else
        {
            xRot = 0;
            yRot = 0;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;

            dayInput.text = DateTime.Now.Day.ToString();
            monthInput.text = DateTime.Now.Month.ToString();
        }
    }
}
