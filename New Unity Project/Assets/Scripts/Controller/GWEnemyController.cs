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




    void Start() {
        this.stats = this.gameObject.GetComponent<GWEnemyStats>();
    }

    void Update() {

        this.text.text = "" + this.stats.currentHealth;

        this.agent.speed = this.stats.movementSpeed;

        if (this.isStatic) {
            return;
        }

        if (Vector3.Distance(this.transform.position, GWPawnController.instance.transform.position) < this.seeCharacterRange) {
            this.agent.destination = GWPawnController.instance.transform.position;
        }
    }

    public void RecieveElementAttack(List<GWEType> elements) {
        this.Hurt(this.GetTotalElementDamage(elements));
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

        return Vector4.Dot(elementAmounts * 10, this.stats.sensibilities);

    }

    public void Hurt(float damage) {
        this.stats.currentHealth -= damage;
    }
}
