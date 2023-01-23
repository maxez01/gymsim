using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public MachineManager machineManager;
    public float timeInterval = 8f;
    public int maxMachines = 5; 
    public GameObject[] npcObjects;
    public NPC[] npcs;
    public Color[] colors;
    private bool countTime = false;
    private float timeCounter;

    private int[] npcCountMap1 = new int [24] {
        1, 1, 1, 1, 1, 2, 2, 3, 4, 5, 6, 6, 6, 5, 5, 6, 7, 10, 12, 11, 8, 5, 3, 2
    };

    // Start is called before the first frame update
    void Start()
    {
        machineManager = new MachineManager();

        npcs = new NPC[npcObjects.Length];
        for (int i = 0; i < npcs.Length; i++)
        {
            npcs[i] = new NPC(npcObjects[i], colors[i % colors.Length]);
            // materials[0] ist das Shirt
            npcObjects[i].GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = npcs[i].shirtColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        walkAndExercise();
    }

    private void walkAndExercise() {
        if (countTime) {
            timeCounter += Time.deltaTime;
        }
        for(int i = 0; i < npcObjects.Length; i++) {
            if(!moveForward(i) && !npcs[i].isWorkingOut && !npcs[i].isWalkingOut) {
                int r = Random.Range(0,16);

                npcs[i].machineKey = r;
                setAnimatedObject(true, r, npcs[i]);

                npcs[i].machineKey = r;
                npcs[i].machineCount++;

                countTime = true;

                npcObjects[i].SetActive(false);
                npcs[i].isWorkingOut = true;
            } else if (npcs[i].isWorkingOut || npcs[i].isWalkingOut) {
                int workoutCount = 0;

                if(timeCounter > timeInterval && npcs[i].machineCount <= maxMachines) {
                    timeCounter = 0f;

                    foreach(GameObject npc in npcObjects) {
                        if (npc.activeSelf.Equals(false)) workoutCount++; 
                    }

                    for (int j = 0; j < workoutCount; j++) {
                        int rr = Random.Range(0,16);

                        setAnimatedObject(false, npcs[j].machineKey, npcs[j]);
                        
                        setAnimatedObject(true, rr, npcs[j]);

                        npcs[j].machineKey = rr;
                        npcs[j].machineCount++;
                    }            
                } else if (npcs[i].isWalkingOut) {
                    npcObjects[i].transform.position += new Vector3(-6, 0, 0) * Time.deltaTime;
                    
                } else if (npcs[i].machineCount > maxMachines) {
                    setAnimatedObject(false, npcs[i].machineKey, npcs[i]);

                    if (!npcObjects[i].activeSelf) {
                        npcObjects[i].SetActive(true);
                        npcs[i].isWorkingOut = false;
                        npcs[i].isWalkingOut = true;
                        npcObjects[i].transform.Rotate(new Vector3(0, -180, 0));
                    }
                }
            } else {
                break;
            }
        }
    }   

    private void setAnimatedObject(bool animate, int machineKey, NPC npc) {
        if(animate)
            setShirtColorByMachine(machineKey, npc);
        machineManager.animatedMachines[machineKey].gameObject.SetActive(animate);
        machineManager.unanimatedMachines[machineKey].gameObject.SetActive(!animate);
    }

    private void setShirtColorByMachine(int machineKey, NPC npc)
    {
        MachineManager.Machine machine = machineManager.animatedMachines[machineKey];
        SkinnedMeshRenderer skinnedMeshRenderer;

        switch (machine.machineType)
        {
            case MachineManager.MachineType.Laufband:
                skinnedMeshRenderer = machine.gameObject.transform.Find("Cube.002").GetComponent<SkinnedMeshRenderer>();
                break;
            case MachineManager.MachineType.Butterfly:
                skinnedMeshRenderer = machine.gameObject.transform.Find("Cube.003").GetComponent<SkinnedMeshRenderer>();
                break;
            case MachineManager.MachineType.Brustpresse:
                skinnedMeshRenderer = machine.gameObject.transform.Find("Cube.002").GetComponent<SkinnedMeshRenderer>();
                break;
            case MachineManager.MachineType.Rudermaschine_:
                skinnedMeshRenderer = machine.gameObject.transform.Find("Cube.003").GetComponent<SkinnedMeshRenderer>();
                break;
            case MachineManager.MachineType.Beinpresse:
                skinnedMeshRenderer = machine.gameObject.transform.Find("Cube.002").GetComponent<SkinnedMeshRenderer>();
                break;
            case MachineManager.MachineType.Kurzhantel:
                skinnedMeshRenderer = machine.gameObject.transform.Find("Cube.001").GetComponent<SkinnedMeshRenderer>();
                break;
            default:
                // default case will recolor part of the machine to indicate malfunction
                skinnedMeshRenderer = machine.gameObject.transform.Find("Cube").GetComponent<SkinnedMeshRenderer>();
                break;
        }

        skinnedMeshRenderer.materials[0].color = npc.shirtColor;
    }

    private bool moveForward(int key) {
        if(npcObjects[key].transform.position.x < -10 && key < npcCountMap1[DayTimeManager.currentDateTime.Hour] && !npcs[key].isWalkingOut) {
            npcObjects[key].transform.position += new Vector3(6, 0, 0) * Time.deltaTime;
            return true;
        } else if (key >= npcCountMap1[DayTimeManager.currentDateTime.Hour]) {
            return true;
        } else {
            return false;
        }
    }

    public class NPC {

        public NPC(GameObject gObject, Color shirtColor) {
            isWorkingOut = false;
            isWalkingOut = false;
            gameObject = gObject;
            machineKey = -1;
            machineCount = 0;
            this.shirtColor = shirtColor;
        }

        public bool isWorkingOut { get; set; }
        public bool isWalkingOut { get; set; }    
        public GameObject gameObject { get; set; }
        public int machineKey { get; set; }
        public int machineCount { get; set; }
        public Color shirtColor;
    }
}
