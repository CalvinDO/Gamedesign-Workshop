using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GWEnemyController : MonoBehaviour {

    public NavMeshAgent agent;
    public float seeCharacterRange;
    public GWEnemyStats stats;
    public Text text;
    public Rigidbody rb;

    public bool isStatic;

    void Start() {
        this.stats = gameObject.GetComponent<GWEnemyStats>();
    }

    void Update() {

        this.text.text = "" + this.stats.currentHealth;

        this.agent.speed = this.stats.movementSpeed;

        if (this.isStatic) {
            return;
        }

        if (Vector3.Distance(this.transform.position, GWPawnController.instance.transform.position) < this.seeCharacterRange) {
            this.agent.destination = GWPawnController.instance.transform.position;
        }
    }
}
