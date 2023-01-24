using TMPro;
using UnityEngine;

public class DayTimeDisplay : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.SetText(DayTimeManager.currentDateTime.Hour.ToString("00") + ":" + DayTimeManager.currentDateTime.Minute.ToString("00"));
    }
}
