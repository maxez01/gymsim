using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineManager : MonoBehaviour
{
    public Machine[] unanimatedMachines;
    public Machine[] animatedMachines;

    public MachineManager() {
        initializeGameObjects();
    }

    private void initializeGameObjects() {
        int k = 0;
        unanimatedMachines = new Machine[17];
        animatedMachines = new Machine[17];        

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
                
                unanimatedMachines[k] = unanimatedMachine;
                animatedMachines[k] = animatedMachine;

                k++;

                // enable if neccessary

                // Debug.Log(unanimatedMachines[k-1].gameObject.name);
                // Debug.Log(animatedMachines[k-1].gameObject.name);
            }
        }
    }

    public class Machine
    {
        public GameObject gameObject;

        public void SetGameObject(GameObject value) {
            gameObject = value;
        }
    }
}
