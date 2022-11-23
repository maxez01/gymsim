using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SunMovement : MonoBehaviour
{
    public float theta = 0.01f;

    public float xRot;
    public float yRot;

    public GameObject timeText;

    void Update()
    {
        // TODO change axis
        // TODO time dependent
        // TODO realistic with max in y and 4 versions for each season
        this.transform.RotateAround(Vector3.zero, new Vector3(xRot, -yRot ,0), theta);

        TextMeshProUGUI text = timeText.GetComponent<TextMeshProUGUI>();
        float angle = 0.0f;
        Vector3 axis = Vector3.zero;
        this.transform.rotation.ToAngleAxis(out angle, out axis);
        text.text = "Sonnenwinkel: " + angle + "°";
    }
}
