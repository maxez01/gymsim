using System.Collections.Generic;
using UnityEngine;

public class LavaMovement : MonoBehaviour
{
    public List<GameObject> lavaBalls;
    private float speedModifier;
    private Vector3 baseScale;

    // Start is called before the first frame update
    void Start()
    {
        float i = -1;
        foreach (GameObject lavaBall in lavaBalls)
        {
            lavaBall.transform.localPosition = new Vector3(lavaBall.transform.localPosition.x, i, lavaBall.transform.localPosition.z);
            i += 2 / lavaBalls.Count;
        }

        speedModifier = 0.5f;
        baseScale = lavaBalls[0].transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float i = 0;
        foreach (GameObject lavaBall in lavaBalls)
        {
            float yPos = Mathf.Sin(Time.time * speedModifier + i);
            Vector3 newPosition = new Vector3(lavaBall.transform.localPosition.x, yPos, lavaBall.transform.localPosition.z);

            lavaBall.transform.localPosition = newPosition;

            if (newPosition.y < -0.5)
                lavaBall.transform.localScale = new Vector3(baseScale.x, baseScale.y * yPos * 2, baseScale.z);
            else
                lavaBall.transform.localScale = baseScale;
            i += Mathf.PI * 2 / lavaBalls.Count;
        }
    }
}
