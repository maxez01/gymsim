using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightManager : MonoBehaviour
{
    public List<Light> lights;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DayTimeManager.currentDateTime.TimeOfDay < new TimeSpan(8, 15, 0) || DayTimeManager.currentDateTime.TimeOfDay > new TimeSpan(20, 15, 0))
            lights.Find(light => light.name == "Tresenlicht").gameObject.SetActive(true);
        else
            lights.Find(light => light.name == "Tresenlicht").gameObject.SetActive(false);

        if (DayTimeManager.currentDateTime.TimeOfDay < new TimeSpan(8, 0, 0) || DayTimeManager.currentDateTime.TimeOfDay > new TimeSpan(20, 0, 0))
            lights.Find(light => light.name == "Sun").intensity = 0;
        else
            lights.Find(light => light.name == "Sun").intensity = 1;
    }
}
