using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GWControlType {
    ROTATION = 0, CLICKCAST = 1, CLICKCENTERED = 2, SWIPECAST = 3, ROTATIONSCROLLER = 4
}

public enum GWFormType {
    PROJECTILE = 0, AOE = 1, CONE = 2, ROUNDHOUSE = 3, DISTRIBUTION = 4, HORIZONTAL_BEAM = 5, SHOOT_UP = 6
}


public class GWAttackor : MonoBehaviour {

    private bool isPressingAttack;
    private bool isPressingHeal;
    public List<GWEnemyController> nearbyEnemys;

    public MeshRenderer visualAttackor;

    public GWControlType control;
    public GWFormType form;

    private GWInventorySlot correspondingInventorySlot;
    private GWSpell spell;


    private float remainingActive;

    private bool onlyOneTimeEffect;
    private float effectInterval;

    private bool isSummoned;
    private bool alreadyUsed;


    private float nextEffect;

    private GWAttackor summonedAttackorClone;


    void Awake() {

        Debug.Log("attackor awake");


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


        if (this.isSummoned) {

            if (!this.alreadyUsed) {

                if (!this.onlyOneTimeEffect) {

                    if (this.nextEffect < 0) {

                        this.Damage();
                        this.nextEffect = this.effectInterval;
                    }
                }
                else {
                    this.Damage();
                    this.alreadyUsed = true;
                }
            }

            if (this.remainingActive < 0) {

                this.Inactivate();
            }

            this.remainingActive -= Time.deltaTime;
        }

        Debug.Log("spell: " + this.spell);
        
    }

    public void Inactivate() {

        try {

            Material weaponMat = this.visualAttackor.material;
            Color weaponColor = weaponMat.color = this.spell.color;
            weaponColor.a = 0;
            weaponMat.color = weaponColor;
        }
        catch (Exception e) {
            Debug.LogWarning(e.Message);
        }


        GameObject.Destroy(this.gameObject);
    }

    public void Activate(GWInventorySlot inventorySlot) {

        Debug.Log("attack " + this.nearbyEnemys.Count + " enemys");


        this.summonedAttackorClone = GameObject.Instantiate(this.gameObject, null).GetComponent<GWAttackor>();
        this.summonedAttackorClone.isSummoned = true;
        this.summonedAttackorClone.nextEffect = this.correspondingInventorySlot.uiSpell.spell.effectInterval;

        this.summonedAttackorClone.correspondingInventorySlot = inventorySlot;

        this.summonedAttackorClone.onlyOneTimeEffect = inventorySlot.uiSpell.spellInstance.onlyOneTimeEffect;
        this.summonedAttackorClone.effectInterval = inventorySlot.uiSpell.spellInstance.effectInterval;
        this.summonedAttackorClone.remainingActive = inventorySlot.remainingActive;
        this.summonedAttackorClone.spell = inventorySlot.uiSpell.spellInstance;
    }

    private void Damage() {
        foreach (GWEnemyController nearbyEnemy in this.nearbyEnemys) { //throws error "InvalidOperationException: Collection was modified; enumeration operation may not execute." when multiple enemies within collider

            nearbyEnemy.RecieveElementAttack(this.correspondingInventorySlot.Spell.containedElements);
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
