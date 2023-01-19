using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    private bool dragging = false;
    public SunMovement sunMovement;
    private Slider slider;
    public GameObject sunObject;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dragging)
            slider.value = sunMovement.angle;
        else
        {
            sunMovement.angle = slider.value;
            sunMovement.transform.rotation = Quaternion.AngleAxis(sunMovement.angle, sunMovement.axis);
        }
    }

    public void SliderDragging()
    {
        dragging = true;
    }

    public void SliderNotDragging()
    {
        dragging = false;
    }
}
