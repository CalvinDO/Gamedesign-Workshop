using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWAttackor : MonoBehaviour {
    private bool isPressingAttack;
    private bool isPressingHeal;
    public List<GWEnemyController> nearbyEnemys;

    public MeshRenderer visualAttackor;

    void Awake() {
        this.nearbyEnemys = new List<GWEnemyController>();
    }

    virtual public void Update() {
        /*
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
        */

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

    public void Attack(GWInventorySlot inventorySlot) {

        foreach (GWEnemyController nearbyEnemy in this.nearbyEnemys) { //throws error "InvalidOperationException: Collection was modified; enumeration operation may not execute." when multiple enemies within collider


            nearbyEnemy.RecieveElementAttack(inventorySlot.Spell.containedElements);
            nearbyEnemy.gameObject.AddComponent<GWSlow>();

            if (nearbyEnemy.gameObject.GetComponent<GWEnemyStats>().currentHealth <= 0) {

                this.nearbyEnemys.Remove(nearbyEnemy);
                GameObject.Destroy(nearbyEnemy);
            }
        }
    }

    void Heal() {

        if (!GWPawnController.instance.gameObject.TryGetComponent<GWHeal>(out GWHeal healing)) {
            GWPawnController.instance.gameObject.AddComponent<GWHeal>();

        }
    }



    void OnTriggerStay(Collider other) {

        if (this.nearbyEnemys.Contains(other.gameObject.GetComponent<GWEnemyController>())) {
            return;
        }

        this.nearbyEnemys.Add(other.gameObject.GetComponent<GWEnemyController>());

    }

    void OnTriggerExit(Collider other) {

        GWEnemyController otherEnemyController = other.gameObject.GetComponent<GWEnemyController>();

        if (this.nearbyEnemys.Contains(otherEnemyController)) {
            this.nearbyEnemys.Remove(otherEnemyController);
        }
    }
}
