using UnityEngine;
using TMPro;

public class DayTimeDisplay : MonoBehaviour
{
    public GameObject sunObject;

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        sunObject.transform.rotation.ToAngleAxis(out float angle, out _);
        text.SetText("Sonnenwinkel: " + angle + "°");
    }
}
