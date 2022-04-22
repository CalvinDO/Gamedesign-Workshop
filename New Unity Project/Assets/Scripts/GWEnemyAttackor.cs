using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GWAttackState {
    Roaming = 0,
    Loading = 1,
    Attacking = 2
}

public class GWEnemyAttackor : MonoBehaviour {

    public float cooldownTime;
    public float remainingTime;

    public float loadTime;
    public float remainingLoadTime;

    public float attackTime;
    public float remainingAttackTime;


    public GWAttackState attackState;

    public GWEnemyController enemy;

    public GWPawnController pawnController;

    public GWWeapon weapon;

    public Vector3 futureAttackPos;


    void Start() {
        this.remainingTime = this.cooldownTime;
        this.remainingLoadTime = this.loadTime;
        this.remainingAttackTime = this.attackTime;
    }

    // Update is called once per frame
    void Update() {

        //this.remainingTime -= Time.deltaTime;

        /*
        if (this.remainingTime <= 0) {
            this.Attack();
        }
        */




        switch (this.attackState) {

            case GWAttackState.Roaming:

                if (this.pawnController != null) {
                    this.attackState = GWAttackState.Loading;
                }

                this.weapon.gameObject.SetActive(false);

                break;
            case GWAttackState.Loading:

                this.remainingLoadTime -= Time.deltaTime;

                if (this.remainingLoadTime <= 0) {

                    this.attackState = GWAttackState.Attacking;

                    this.remainingLoadTime = this.loadTime;
                }


                this.futureAttackPos = GWPawnController.instance.transform.position + GWPawnController.instance.velocity * this.attackTime * 50;
               
                this.transform.LookAt(this.futureAttackPos);


                //this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.y,  0);

                this.weapon.gameObject.SetActive(false);

                break;
            case GWAttackState.Attacking:

                this.enemy.agent.isStopped = true;

                this.remainingAttackTime -= Time.deltaTime;
                this.weapon.gameObject.SetActive(true);


                float factor = this.remainingAttackTime / this.attackTime;

                Material weaponMat = this.weapon.meshRenderer.material;
                Color weaponColor = weaponMat.color;
                weaponColor.a = 1 - factor;
                weaponMat.color = weaponColor;

                this.weapon.meshRenderer.material.color = weaponColor;


                if (this.remainingAttackTime <= 0) {

                    this.Attack();

                    this.attackState = GWAttackState.Roaming;
                    this.enemy.agent.isStopped = false;

                    this.remainingAttackTime = this.attackTime;
                }

                break;
            default:
                break;

        }
    }
    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(this.futureAttackPos, 2);
    }

    void OnTriggerEnter(Collider other) {
        this.pawnController = other.gameObject.GetComponent<GWPawnController>();

    }

    void OnTriggerStay(Collider other) {
        this.pawnController = other.gameObject.GetComponent<GWPawnController>();

        if (this.pawnController == null) {
            Debug.Log(other.name);
        }

    }

    void OnTriggerExit(Collider other) {


        this.pawnController = null;


    }

    void Attack() {

        this.weapon.Attack();

        this.remainingTime = this.cooldownTime;
    }
}
