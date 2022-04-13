using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackor : MonoBehaviour {
    public float cooldownTime;
    public float remainingTime;

    private CharacterController characterController;

    void Start() {
        this.remainingTime = cooldownTime;
    }

    // Update is called once per frame
    void Update() {

        this.remainingTime -= Time.deltaTime;

        if (this.characterController == null) { 
            return;
        }
        if (this.remainingTime <= 0) {
            this.Attack();
        }
    }


    void OnTriggerEnter(Collider other) {
        this.characterController = other.gameObject.GetComponent<CharacterController>();

        Debug.Log(this.characterController);
    }

    void OnTriggerExit(Collider other) {
        this.characterController = null;
       
    }

    void Attack() {

        Debug.Log("enemy attack");
        if (this.characterController == null) {
            return;
        }
        this.characterController.health -= 5;

        this.remainingTime = this.cooldownTime;
    }
}
