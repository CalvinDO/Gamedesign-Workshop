using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWWeapon : MonoBehaviour {

    public GWEnemyAttackor enemyAttackor;
    private GWPawnController pawnController;
    public MeshRenderer meshRenderer;



    void OnTriggerEnter(Collider other) {

        //Debug.Log("Weapon collision by: " + this.transform.root.name);

        this.pawnController = other.gameObject.GetComponent<GWPawnController>();
        this.enemyAttackor.reachablePawnController = this.pawnController;

    }

    void OnTriggerStay(Collider other) {
        this.pawnController = other.gameObject.GetComponent<GWPawnController>();
        this.enemyAttackor.reachablePawnController = this.pawnController;
    }

    void OnTriggerExit(Collider other) {
            this.pawnController = null;
    }
    
    public void Attack() {

        if (this.pawnController == null) {
            return;
        }

        this.pawnController.Hurt(5);
        this.pawnController = null;
        this.enemyAttackor.reachablePawnController = null;
        //this.pawnController.GetComponent<GWPawnStats>().currentHealth -= 5;
    }
}
