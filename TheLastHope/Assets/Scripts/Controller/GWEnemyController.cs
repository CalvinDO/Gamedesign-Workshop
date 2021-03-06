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

    public GameObject mesh;

    public float currentMovementSpeed;

    public bool isFlying;
    public bool isGrounded;

    public AudioSource source;
    public GWEnemySounds enemySounds;

    public GWEType element;

    public bool isDied;



    public virtual void Start() {
        this.stats = this.gameObject.GetComponent<GWEnemyStats>();

        this.currentMovementSpeed = this.agent.speed;
        this.stats.movementSpeed = this.agent.speed;
    }

    public virtual void Update() {

        if (this.isDied) {
            Destroy(this.agent);
            return;
        }

        this.text.text = "" + this.stats.currentHealth;

        this.agent.speed = this.currentMovementSpeed;


        //Debug.Log(this.agent.speed);

        if (this.isStatic) {
            return;
        }

        if (this.isGrounded) {
            this.rb.velocity = Vector3.zero;
        }

        //Debug.Log("agent destination: " + this.agent.destination);
        try {
            float distance = Vector3.Distance(this.transform.position, GWPawnController.instance.transform.position);
            if (distance < this.seeCharacterRange) {

                try {

                    Vector3 connection = GWPawnController.instance.transform.position - this.transform.position;
                    connection -= connection.normalized * 1.5f;
                    this.agent.destination = this.transform.position + connection;
                }
                catch (Exception e) {

                    Debug.LogWarning(e.Message);
                }
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

        //Debug.Log(this.agent.speed);
    }

    public void Knockback(float seconds) {
        this.StartCoroutine(this.SetToGround(seconds));
    }

    IEnumerator SetToGround(float seconds) {
        yield return new WaitForSeconds(seconds);
        this.isGrounded = true;
        this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        //this.isFlying = false;
    }

    public void RecieveElementAttack(List<GWEType> elements) {
        try {

            this.Hurt(this.GetTotalElementDamage(elements));
        }
        catch (Exception e) {
            Debug.LogWarning(e.Message);
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
        //Debug.Log("total Element damage: " + Vector4.Dot(elementAmounts * 10, this.stats.sensibilities));
        return Vector4.Dot(elementAmounts * 10, this.stats.sensibilities);

    }

    public void Hurt(float damage) {
        enemySounds.PlayHurt(source);
        GameObject newRisingDamageText = GameObject.Instantiate(this.risingDamageTextPrefab, GWPoolManager.instance.risingDamageTextPool);
        newRisingDamageText.gameObject.SetActive(true);
        newRisingDamageText.transform.SetPositionAndRotation(this.risingDamageTextPrefab.transform.position, this.risingDamageTextPrefab.transform.rotation);

        newRisingDamageText.GetComponentInChildren<GWUIRisingDamageText>().SetHurt(damage);
        this.stats.currentHealth -= damage;

        this.attackor.attackState = GWAttackState.Roaming;
        this.attackor.weapon.gameObject.SetActive(false);
        try {

            this.agent.isStopped = false;
        }
        catch (Exception e) {
            Debug.LogWarning("EnemyController hurt exception: " + e.Message);
        }


        if (this.stats.currentHealth <= 0) {
            this.Die();
        }
    }

    virtual public void Die() {

        if (this.isDied) {
            return;
        }

        this.isDied = true;

        this.text.gameObject.SetActive(false);

        this.attackor.animator.SetTrigger("die");

        this.gameObject.GetComponent<CapsuleCollider>().gameObject.layer = LayerMask.NameToLayer("DeadEnemy");
        //GameObject.Destroy(this.gameObject);
    }
}
