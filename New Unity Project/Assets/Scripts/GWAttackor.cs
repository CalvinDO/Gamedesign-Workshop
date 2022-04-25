using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWAttackor : MonoBehaviour
{
    private bool isPressingAttack;
    private bool isPressingHeal;
    private GWEnemyController nearbyEnemy;


    virtual public void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {

            if (!this.isPressingAttack) {
                this.Attack();
            }

            this.isPressingAttack = true;
        }
        else {
            this.isPressingAttack = false;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            this.isPressingAttack = false;
        }

        // just copied for Mouse 2 (heal testing)
        if (Input.GetKey(KeyCode.Mouse1)) {

            if (!this.isPressingHeal) {
                this.Heal();
            }

            this.isPressingHeal = true;
        }
        else {
            this.isPressingHeal = false;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1)) {
            this.isPressingHeal = false;
        }

        
    }
    void Attack() {

        if (this.nearbyEnemy == null) {
            Debug.Log("attack but enemy null");

            return;
        }
        this.nearbyEnemy.gameObject.GetComponent<GWEnemyStats>().currentHealth -= 20;
        this.nearbyEnemy.gameObject.AddComponent<GWBurn>();
    }

    void Heal(){
       
        if (!GWPawnController.instance.gameObject.TryGetComponent<GWHeal>(out GWHeal healing)){
            GWPawnController.instance.gameObject.AddComponent<GWHeal>();
            
        }
    }



    void OnTriggerStay(Collider other) {
        this.nearbyEnemy = other.gameObject.GetComponent<GWEnemyController>();

    }

    void OnTriggerExit(Collider other) {

        if (this.nearbyEnemy == other.gameObject.GetComponent<GWEnemyController>()) {
            this.nearbyEnemy = null;
        }
    }
}
