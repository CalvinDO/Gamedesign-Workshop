using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackor : MonoBehaviour
{
    private bool isPressingAttack;
    private bool isPressingHeal;
    private EnemyController nearbyEnemy;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
            return;
        }
        nearbyEnemy.gameObject.GetComponent<EnemyStats>().currentHealth -= 20;
        nearbyEnemy.gameObject.AddComponent<Burn>();
    }

    void Heal(){
       
        if (!this.transform.root.gameObject.TryGetComponent<Heal>(out Heal healing)){
            this.transform.root.gameObject.AddComponent<Heal>();
            
        }
    }

    void OnTriggerEnter(Collider other) {
        this.nearbyEnemy = other.gameObject.GetComponent<EnemyController>();

        Debug.Log(this.nearbyEnemy);
    }

    void OnTriggerExit(Collider other) {

        if (this.nearbyEnemy == other.gameObject.GetComponent<EnemyController>()) {
            this.nearbyEnemy = null;
        }
    }
}
