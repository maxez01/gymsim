using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Season
{
    Spring = 1,
    Summer = 2,
    Autumn = 3,
    Winter = 4
}

public class SunMovement : MonoBehaviour
{
    public int season; 
    public float speed = 0.01f;

    public float xRot;
    public float yRot;

    void Update()
    {
        // TODO change axis
        // TODO time dependent

        // TODO realistic with max in y and 4 versions for each season
        switch(season) {
            case (int)Season.Spring: 
                break;
            case (int)Season.Summer: 
                break;
            case (int)Season.Autumn: 
                break;
            case (int)Season.Winter: 
                break;
        }
        this.transform.Rotate(new Vector3(xRot * Time.deltaTime, -yRot * Time.deltaTime ,0), speed);
    }
}
