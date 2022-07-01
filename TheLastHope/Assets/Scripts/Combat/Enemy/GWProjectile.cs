using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWProjectile : MonoBehaviour {
    [Range(0, 1)]
    public float flySpeed;

    private bool isFlying;

    [Range(0, 100)]
    public float damage;

    public GWInventorySlot correspondingInventorySlot;

    public MeshRenderer meshRenderer;

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
        GWEnemyController nearbyEnemy = other.gameObject.GetComponent<GWEnemyController>();

        if (pawnController) {
            pawnController.Hurt(this.damage);
        }
        else {
            //enemyController.Hurt(this.damage);

            try {

                if (!nearbyEnemy.gameObject.GetComponent<GWStun>()) {
                    nearbyEnemy.gameObject.AddComponent<GWStun>();
                }
                else {
                    Destroy(nearbyEnemy.gameObject.GetComponent<GWStun>());
                    nearbyEnemy.gameObject.AddComponent<GWStun>();
                }

                nearbyEnemy.RecieveElementAttack(this.correspondingInventorySlot.Spell.containedElements);

                
                //Aus Testing: Slow wird immer applied!!!

                foreach (GWElementEffect effect in this.correspondingInventorySlot.Spell.effects) {
                    //Debug.Log("effect: " + effect);
                    switch (effect) {
                        case GWElementEffect.SLOW:
                            nearbyEnemy.gameObject.AddComponent<GWSlow>();
                            break;
                        case GWElementEffect.BURNING:
                            nearbyEnemy.gameObject.AddComponent<GWBurn>();
                            break;
                        case GWElementEffect.DISARMED:

                            if (!nearbyEnemy.gameObject.GetComponent<GWDisarm>()) {
                                nearbyEnemy.gameObject.AddComponent<GWDisarm>();
                            }
                            else {
                                Destroy(nearbyEnemy.gameObject.GetComponent<GWDisarm>());
                                nearbyEnemy.gameObject.AddComponent<GWDisarm>();
                            }

                            break;
                    }
                }
            }
            catch (Exception e) {
            }
        }



        GameObject.Destroy(this.gameObject);
    }
}