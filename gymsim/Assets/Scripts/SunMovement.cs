using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SunMovement : MonoBehaviour
{
    public float speed = 0.01f;
    private float xRot = 0.15f;
    private float yRot = 0.05f;
    public GameObject timeText;
    public int day = 1; 
    public int month = 1;

    void Start() {
        yRot = CalculateSunRotation();
    }
    void Update()
    {
        this.transform.Rotate(new Vector3(xRot * Time.deltaTime, -yRot * Time.deltaTime ,0), speed);
        
        TextMeshProUGUI text = timeText.GetComponent<TextMeshProUGUI>();
        float angle = 0.0f;
        Vector3 axis = Vector3.zero;
        this.transform.rotation.ToAngleAxis(out angle, out axis);
        text.text = "Sonnenwinkel: " + angle + "�";
    }

    /*
    *   Zur Wintersonnenwende (21.12) muss die Neigung der Sonne am größten sein
    *   Zur Sommensonnenwende (21.06) muss die Neigung der Sonne am geringsten sein 
    *   Zum Äquinoktium (20.03 und 22.09) muss x und y Rotation gleich sein, da Tagundnachtgleiche herrscht
    */
    private float CalculateSunRotation() {
        if (month >= 0 && month <= 12 && day >= 1 && day <= 31) {
            DateTime dt = new DateTime(2022, month, day);
            
            // 01.01 - 21.06
            if (dt.DayOfYear + 10 >= 0 && dt.DayOfYear <= 172) {
                return 0.05f + ((172 - dt.DayOfYear) / 172f) * 0.2f;
            // 22.06 - 21.12
            } else if (dt.DayOfYear >= 173 && dt.DayOfYear <= 355) {
                return 0.05f + ((dt.DayOfYear - 173) / 182f) * 0.2f;
            // 22.12 - 31.12
            } else {
                for(int i = 1, j = 356; i <= 10; i++, j++) {
                    if (j == dt.DayOfYear) {
                        return 0.25f - ((i / 177f) * 0.02f);
                    }
                }
            }
            return 0.5f;
        } else {
            Debug.Log("Enter valid values");
            return 0.5f;
        }
    }
}
