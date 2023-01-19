using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    private Dictionary<int, Machine> machines;
    // Start is called before the first frame update
    void Start()
    {
        machines = new Dictionary<int, Machine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private class Machine
    {
        public string name;
        public Boolean isUsed;
    }
}
