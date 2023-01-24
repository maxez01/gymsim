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
    private float timeCounter;
    private List<int> machineIndices;

    private int[] npcCountMap = new int [24] {
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

        machineIndices = new List<int>();
        for (int i = 0; i <= 16; i++)
        {
            machineIndices.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        walkAndExercise();
    }

    private void walkAndExercise() {
        timeCounter += Time.deltaTime;
        for(int i = 0; i < npcs.Length; i++) {

            switch (npcs[i].state)
            {
                case NPCState.waiting:
                    if (IsNobodyWalkingIn() && IsRoomInside())
                        npcs[i].state = NPCState.walkingIn;
                    break;
                case NPCState.walkingIn:
                    moveIn(npcs[i]);
                    break;
                case NPCState.workingOut:
                    workout(npcs[i]);
                    break;
                case NPCState.walkingOut:
                    moveOut(npcs[i]);
                    break;
            }
        }

        if(timeCounter >= timeInterval)
            timeCounter = 0f;
    }

    private bool IsRoomInside()
    {
        int count = 0;
        foreach(NPC npc in npcs)
        {
            if (npc.state == NPCState.walkingIn || npc.state == NPCState.workingOut)
                count++;
        }

        return count < npcCountMap[DayTimeManager.currentDateTime.Hour];
    }

    private bool IsNobodyWalkingIn()
    {
        foreach (NPC npc in npcs)
            if (npc.state == NPCState.walkingIn)
                return false;
        return true;
    }

    private void setAnimatedObject(bool animate, int machineKey, NPC npc) {
        if(animate)
            setShirtColorByMachine(machineKey, npc);
        machineManager.animatedMachines[machineKey].gameObject.SetActive(animate);
        machineManager.unanimatedMachines[machineKey].gameObject.SetActive(!animate);
    }

    private void moveIn(NPC npc)
    {
        if (npc.gameObject.transform.position.x < -10)
        {
            npc.gameObject.transform.position += new Vector3(6, 0, 0) * Time.deltaTime;
        }
        else
        {
            int unusedMachineKey = getRandomUnusedMachineKey();

            setAnimatedObject(true, unusedMachineKey, npc);

            npc.machineKey = unusedMachineKey;
            npc.machineCount++;
            npc.gameObject.SetActive(false);
            npc.state = NPCState.workingOut;
        }
    }

    private void workout(NPC npc)
    {
        if (npc.machineCount > maxMachines)
        {
            setAnimatedObject(false, npc.machineKey, npc);

            npc.gameObject.SetActive(true);
            npc.state = NPCState.walkingOut;
            npc.gameObject.transform.Rotate(new Vector3(0, -180, 0));
        }

        if (timeCounter >= timeInterval && npc.machineCount <= maxMachines)
        {
            switchMachine(npc);
        }
    }

    private int getRandomUnusedMachineKey() {
        List<int> usedMachines = new List<int>();
        List<int> unusedMachines = new List<int>();

        foreach (NPC usingNpc in npcs)
        {
            usedMachines.Add(usingNpc.machineKey);
        }

        foreach (int i in machineIndices)
        {
            if (!usedMachines.Contains(i))
                unusedMachines.Add(i);
        }

        return unusedMachines[Random.Range(0, unusedMachines.Count)];
    }

    private void switchMachine(NPC npc)
    {
        int unusedMachineKey = getRandomUnusedMachineKey();

        setAnimatedObject(false, npc.machineKey, npc);
        setAnimatedObject(true, unusedMachineKey, npc);

        npc.machineCount++;
        npc.machineKey = unusedMachineKey;
    }

    private void moveOut(NPC npc)
    {
        if (npc.gameObject.transform.position.x > -55)
        {
            npc.gameObject.transform.position += new Vector3(-6, 0, 0) * Time.deltaTime;
        }
        else
        {
            npc.gameObject.transform.Rotate(new Vector3(0, 180, 0));
            npc.state = NPCState.waiting;
            npc.machineKey = -1;
            npc.machineCount = 0;
        }
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

    public class NPC {

        public NPC(GameObject gObject, Color shirtColor) {
            state = NPCState.waiting;
            gameObject = gObject;
            machineKey = -1;
            machineCount = 0;
            this.shirtColor = shirtColor;
        }
 
        public GameObject gameObject { get; set; }
        public int machineKey { get; set; }
        public int machineCount { get; set; }
        public Color shirtColor;
        public NPCState state;
    }

    public enum NPCState
    {
        walkingIn, workingOut, walkingOut, waiting
    }
}
