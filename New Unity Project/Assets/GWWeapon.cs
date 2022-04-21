using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWWeapon : MonoBehaviour {

    public GWEnemyAttackor enemyAttackor;
    private GWPawnController pawnController;
    public MeshRenderer meshRenderer;


    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    
    void OnTriggerEnter(Collider other) {

        this.pawnController = other.gameObject.GetComponent<GWPawnController>();
        this.enemyAttackor.pawnController = this.pawnController;

    }

    void OnTriggerStay(Collider other) {
        this.pawnController = other.gameObject.GetComponent<GWPawnController>();
        this.enemyAttackor.pawnController = this.pawnController;
    }

    void OnTriggerExit(Collider other) {

        
            this.pawnController = null;
    }
    
    public void Attack() {

        if (this.pawnController == null) {
            return;
        }

        this.pawnController.GetComponent<GWPawnStats>().currentHealth -= 5;
    }
}
