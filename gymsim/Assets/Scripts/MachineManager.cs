using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    private Dictionary<int, Machine> unanimatedMachines;
    private Dictionary<int, Machine> animatedMachines;
    // Start is called before the first frame update
    void Start()
    {
        initializeGameObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initializeGameObjects() {
        int k = 0;
        unanimatedMachines = new Dictionary<int, Machine>();
        animatedMachines = new Dictionary<int, Machine>();        

        // TODO only hardcoded now, could use better code style
        string[] machineListNames = new string[] {"Laufband", "Butterfly", "Brustpresse", "Rudermaschine_", "Beinpresse", "Kurzhantel"};
        int[] machineListCount = new int[] {4, 3, 2, 3, 3, 2};

        for(int i = 0; i < machineListNames.Length; i++) {
            for (int j = 0; j < machineListCount[i]; j++) {
                Machine unanimatedMachine = new Machine();
                Machine animatedMachine = new Machine();

                if (j > 0) {
                    unanimatedMachine.SetGameObject(GameObject.Find("Geräte/" + machineListNames[i] + " (" + j + ")"));
                    animatedMachine.SetGameObject(GameObject.Find("Geräte/" + machineListNames[i] + " - animated (" + j + ")"));
                } else {
                    unanimatedMachine.SetGameObject(GameObject.Find("Geräte/" + machineListNames[i]));
                    animatedMachine.SetGameObject(GameObject.Find("Geräte/" + machineListNames[i] + " - animated"));
                }

                animatedMachine.gameObject.SetActive(false);
                
                unanimatedMachines.Add(k, unanimatedMachine);
                animatedMachines.Add(k, animatedMachine);

                k++;

                // enable if neccessary

                // Debug.Log(unanimatedMachines.GetValueOrDefault(k-1).gameObject.name);
                // Debug.Log(animatedMachines.GetValueOrDefault(k-1).gameObject.name);
            }
        }
    }

    private class Machine
    {
        public string name;
        public Boolean isUsed;
        public GameObject gameObject;

        public void SetGameObject(GameObject value) {
            gameObject = value;
        }
    }
}
