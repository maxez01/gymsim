using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    private AIMovement aiMovement;
    private Bounds fitnessStudio;

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

        if (walkingNPC != null) 
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(walkingNPC.gameObject.transform.position - transform.position), Time.deltaTime * 10);
        }
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.zero), Time.deltaTime * 10);
    }
}
