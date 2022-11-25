using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dayTimeDisplay : MonoBehaviour
{
    public GameObject sunObject;

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI text = this.GetComponent<TextMeshProUGUI>();
        sunObject.transform.rotation.ToAngleAxis(out float angle, out _);
        text.SetText("Sonnenwinkel: " + angle + "°");
    }
}
