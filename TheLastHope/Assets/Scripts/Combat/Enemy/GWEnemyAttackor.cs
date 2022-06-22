using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GWAttackState {
    Roaming = 0,
    Loading = 1,
    Active = 2
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

    public GWPawnController reachablePawnController;

    public GWWeapon weapon;

    public Vector3 futureAttackPos;

    public Animator animator;

    void Start() {
        this.remainingTime = this.cooldownTime;
        this.remainingLoadTime = this.loadTime;
        this.remainingAttackTime = this.attackTime;
    }

    public virtual void Update() {

        switch (this.attackState) {

            case GWAttackState.Roaming:

                if (this.reachablePawnController != null) {
                    this.attackState = GWAttackState.Loading;
                }

                this.weapon.gameObject.SetActive(false);

                break;
            case GWAttackState.Loading:

                this.remainingLoadTime -= Time.deltaTime;

                if (this.remainingLoadTime <= 0) {

                    this.attackState = GWAttackState.Active;

                    this.remainingLoadTime = this.loadTime;
                }


                this.futureAttackPos = GWPawnController.instance.transform.position + GWPawnController.instance.velocity * this.attackTime * 50;

                this.transform.LookAt(this.futureAttackPos);


                //this.transform.rotation = Quaternion.Euler(0, this.transform.rotation.y,  0);

                this.weapon.gameObject.SetActive(false);

                break;
            case GWAttackState.Active:

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
        this.reachablePawnController = other.gameObject.GetComponent<GWPawnController>();

    }

    void OnTriggerStay(Collider other) {

        this.reachablePawnController = other.gameObject.GetComponent<GWPawnController>();
        /*
        if (this.reachablePawnController == null) {
            Debug.Log(other.name);
        }
        */
    }

    void OnTriggerExit(Collider other) {
        this.reachablePawnController = null;
    }

    public virtual void Attack() {

        this.animator.SetTrigger("meleeAttack");

        this.weapon.Attack();

        this.remainingTime = this.cooldownTime;
    }
}
