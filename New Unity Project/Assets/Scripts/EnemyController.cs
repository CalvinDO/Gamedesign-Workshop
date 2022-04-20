using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    private PlayerController playerController;
    public float seeCharacterRange;
    public EnemyStats stats;
    public Text text;

    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        this.playerController = GameObject.Find("Character").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

        
        this.text.text = "" + stats.currentHealth;
        this.agent.speed = this.stats.movementSpeed;
        if (Vector3.Distance(this.transform.position, this.playerController.transform.position) < this.seeCharacterRange) {
            this.agent.destination = this.playerController.transform.position;
        }
    }
}
