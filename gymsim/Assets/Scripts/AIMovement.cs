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
            npcs[i] = new NPC(npcObjects[i]);
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
                setAnimatedObject(true, r);

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

                        setAnimatedObject(false, npcs[j].machineKey);
                        
                        setAnimatedObject(true, rr);

                        npcs[j].machineKey = rr;
                        npcs[j].machineCount++;
                    }            
                } else if (npcs[i].isWalkingOut) {
                    npcObjects[i].transform.position += new Vector3(-6, 0, 0) * Time.deltaTime;
                    
                } else if (npcs[i].machineCount > maxMachines) {
                    setAnimatedObject(false, npcs[i].machineKey);

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

    private void setAnimatedObject(bool animate, int machineKey) {
        machineManager.animatedMachines[machineKey].gameObject.SetActive(animate);
        machineManager.unanimatedMachines[machineKey].gameObject.SetActive(!animate);
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

        public NPC(GameObject gObject) {
            isWorkingOut = false;
            isWalkingOut = false;
            gameObject = gObject;
            machineKey = -1;
            machineCount = 0;
        }

        public bool isWorkingOut { get; set; }
        public bool isWalkingOut { get; set; }    
        public GameObject gameObject { get; set; }
        public int machineKey { get; set; }
        public int machineCount { get; set; }
    }
}
