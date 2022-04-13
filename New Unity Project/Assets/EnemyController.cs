using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    private CharacterController characterController;
    public float seeCharacterRange;


    void Start()
    {
        this.characterController = GameObject.Find("Character").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(this.transform.position, this.characterController.transform.position) < this.seeCharacterRange) {
            this.agent.destination = this.characterController.transform.position;
        }
    }
}
