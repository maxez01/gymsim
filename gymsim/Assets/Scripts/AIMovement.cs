using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public MachineManager machineManager;

    private npc[] npcs;
    // Start is called before the first frame update
    void Start()
    {
        machineManager = new MachineManager();

        initializeNPCS();
    }

    // Update is called once per frame
    void Update()
    {
        bool flag = false;

        if(GameObject.Find("npcs/npc").transform.position.x < -10) {
            GameObject.Find("npcs/npc").transform.position += new Vector3(3, 0, 0) * Time.deltaTime;
        } else if (flag.Equals(false)) {
            flag = true;
            int r = Random.Range(0,16);
            machineManager.animatedMachines[r].gameObject.SetActive(true);
            machineManager.unanimatedMachines[r].gameObject.SetActive(false);
            GameObject.Find("npcs/npc").SetActive(false);
        }
    }

    private void initializeNPCS() {
        npcs = new npc[12];
    }

    private class npc {
        public bool isWorkingOut { get; set; }  
        public GameObject gameObject { get; set; }
        }
}
