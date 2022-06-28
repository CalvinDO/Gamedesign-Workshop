using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GWEnemyController : MonoBehaviour {

    public NavMeshAgent agent;
    public float seeCharacterRange;
    public GWEnemyStats stats;
    public Text text;
    public Rigidbody rb;

    public bool isStatic;

    public GWEnemyAttackor attackor;

    public GameObject risingDamageTextPrefab;


    public float currentMovementSpeed;

    public bool isFlying;


    public virtual void Start() {
        this.stats = this.gameObject.GetComponent<GWEnemyStats>();

        this.currentMovementSpeed = this.agent.speed;

        Debug.Log(this.currentMovementSpeed);
    }

    public virtual void Update() {

        this.text.text = "" + this.stats.currentHealth;

        this.agent.speed = this.currentMovementSpeed;

        Debug.Log(this.agent.speed);

        if (this.isStatic) {
            return;
        }


        //Debug.Log("agent destination: " + this.agent.destination);
        try {
            if (Vector3.Distance(this.transform.position, GWPawnController.instance.transform.position) < this.seeCharacterRange) {
                this.agent.destination = GWPawnController.instance.transform.position;
                //Debug.Log("agent destination: " + this.agent.destination);
            }

        }
        catch (Exception e) {
            Debug.LogWarning(e.Message);
        }

        //this.meshParent.transform.LookAt(GWPawnController.instance.transform);

        this.attackor.animator.SetBool("running", this.agent.velocity.magnitude >= 0.001f);

        if (this.isFlying) {
            this.agent.enabled = false;
            if (this.transform.position.y <= 0.01) {
                this.isFlying = false;
                this.agent.enabled = true;
            }
        }

        Debug.Log(this.agent.speed);
    }

    public void RecieveElementAttack(List<GWEType> elements) {
        try {

            this.Hurt(this.GetTotalElementDamage(elements));
        }
        catch (Exception e) {
            return;
        }
    }


    public float GetTotalElementDamage(List<GWEType> elements) {


        Vector4 elementAmounts = Vector4.zero;

        foreach (GWEType element in elements) {

            switch (element) {

                case GWEType.EARTH:

                    elementAmounts[0] += 1;
                    break;

                case GWEType.FIRE:

                    elementAmounts[1] += 1;
                    break;

                case GWEType.WATER:

                    elementAmounts[2] += 1;
                    break;

                case GWEType.AIR:

                    elementAmounts[3] += 1;
                    break;
            }
        }
        Debug.Log("total Element damage: " + Vector4.Dot(elementAmounts * 10, this.stats.sensibilities));
        return Vector4.Dot(elementAmounts * 10, this.stats.sensibilities);

    }

    public void Hurt(float damage) {

        GameObject newRisingDamageText = GameObject.Instantiate(this.risingDamageTextPrefab, GWPoolManager.instance.risingDamageTextPool);
        newRisingDamageText.gameObject.SetActive(true);
        newRisingDamageText.transform.SetPositionAndRotation(this.risingDamageTextPrefab.transform.position, this.risingDamageTextPrefab.transform.rotation);

        newRisingDamageText.GetComponentInChildren<GWUIRisingDamageText>().SetHurt(damage);
        this.stats.currentHealth -= damage;

        this.attackor.attackState = GWAttackState.Roaming;
        this.attackor.weapon.gameObject.SetActive(false);
        this.agent.isStopped = false;
        

        if (this.stats.currentHealth <= 0) {
            this.Die();
        }
    }

    public void Die() {
        GameObject.Destroy(this.gameObject);
    }
}
