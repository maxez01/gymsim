using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    public float theta = 0.01f;
    void Update()
    {
        // TODO change axis
        // TODO time dependent
        this.transform.RotateAround(Vector3.zero, Vector3.right, theta);
    }
}
