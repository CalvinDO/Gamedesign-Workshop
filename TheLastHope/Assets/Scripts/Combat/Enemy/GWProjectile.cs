using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWProjectile : MonoBehaviour {
    [Range(0, 1)]
    public float flySpeed;

    private bool isFlying;

    [Range(0, 100)]
    public float damage;


    void FixedUpdate() {
        if (this.isFlying) {
            this.transform.Translate(Vector3.forward * this.flySpeed);
        }
    }

    public void Shoot() {
        this.isFlying = true;
    }

    void OnTriggerEnter(Collider other) {


        GWPawnController pawnController = other.gameObject.GetComponent<GWPawnController>();
        GWEnemyController enemyController = other.gameObject.GetComponent<GWEnemyController>();

        if (pawnController) {
            pawnController.Hurt(this.damage);
        }
        else {
            enemyController.Hurt(this.damage);
        }



        GameObject.Destroy(this.gameObject);
    }
}