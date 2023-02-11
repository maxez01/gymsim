using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    private AIMovement aiMovement;
    private Bounds fitnessStudio;
    private float lookAwayTimeEnd = 0;
    private MachineManager.Machine machineToLookAt;
    private bool wantsToLook;

    private static readonly float lookProbability = 0.5f;
    private static readonly float rotationSpeed = 7;
    private static readonly RangeInt lookAwayTimeRange = new(2, 1);

    // Start is called before the first frame update
    void Start()
    {
        fitnessStudio = GameObject.Find("Fitnessstudio").GetComponent<MeshRenderer>().bounds;
        aiMovement = GameObject.Find("npcs").GetComponent<AIMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        AIMovement.NPC walkingNPC = aiMovement.npcs.Find(npc => (npc.state == AIMovement.NPCState.walkingIn || npc.state == AIMovement.NPCState.walkingOut) && fitnessStudio.Contains(npc.gameObject.transform.position));

        // looking at walking people has top priority
        if (walkingNPC != null)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(walkingNPC.gameObject.transform.position - transform.position), Time.deltaTime * rotationSpeed);
            lookAwayTimeEnd = 0;
        }
        else
        {
            List<AIMovement.NPC> workingOutNPCs = aiMovement.npcs.FindAll(npc => npc.state == AIMovement.NPCState.workingOut);

            if (workingOutNPCs.Count != 0 && Time.time > lookAwayTimeEnd)
            {
                lookAwayTimeEnd = Time.time + Random.Range(lookAwayTimeRange.start, lookAwayTimeRange.end);
                machineToLookAt = aiMovement.machineManager.animatedMachines[workingOutNPCs[Random.Range(0, workingOutNPCs.Count - 1)].machineKey];
                wantsToLook = Random.value > lookProbability;
            }

            if (wantsToLook)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(machineToLookAt.gameObject.transform.position - transform.position), Time.deltaTime * rotationSpeed);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * rotationSpeed);
            }
        }
    }
}
