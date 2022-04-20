using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackor : MonoBehaviour {
    public float cooldownTime;
    public float remainingTime;

    private GWPawnController playerController;

    void Start() {
        this.remainingTime = cooldownTime;
    }

    // Update is called once per frame
    void Update() {

        this.remainingTime -= Time.deltaTime;

        if (this.playerController == null) { 
            return;
        }
        if (this.remainingTime <= 0) {
            this.Attack();
        }
    }


    void OnTriggerEnter(Collider other) {
        this.playerController = other.gameObject.GetComponent<GWPawnController>();

        Debug.Log(this.playerController);
    }

    void OnTriggerExit(Collider other) {
        this.playerController = null;
       
    }

    void Attack() {

        Debug.Log("enemy attack");
        if (this.playerController == null) {
            return;
        }
        playerController.GetComponent<GWPawnStats>().currentHealth -= 5;

        this.remainingTime = this.cooldownTime;
    }
}
