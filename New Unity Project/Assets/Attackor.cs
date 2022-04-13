using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackor : MonoBehaviour
{
    private bool isPressingAttack;
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

        
    }

    void Attack() {
        if (this.nearbyEnemy == null) {
            return;
        }
        GameObject.Destroy(this.nearbyEnemy.gameObject);
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
