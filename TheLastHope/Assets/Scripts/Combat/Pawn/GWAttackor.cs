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

public enum GWFormEffect {
    VORTEX = 0, KNOCKBACK = 1, STUN = 3
}

public class GWAttackor : MonoBehaviour {

    private bool isPressingAttack;
    private bool isPressingHeal;
    public List<GWEnemyController> nearbyEnemys;
    //public List<GWLoot> lootBoxes;
    public MeshRenderer visualAttackor;

    public GWControlType control;
    public GWFormType form;
    public GWFormEffect formEffect;


    public GWInventorySlot correspondingInventorySlot;
    private GWSpell spell;


    private float remainingActive;

    private bool onlyOneTimeEffect;
    private float effectInterval;

    public bool isSummoned;
    private bool alreadyUsed;


    private float nextEffect;

    private GWAttackor upbildingAttackorClone;

    public GWAttackor originalAttackor;

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

            this.ManageTransparency();

            this.ManageFormEffect();


            if (!this.alreadyUsed) {

                if (!this.onlyOneTimeEffect) {

                    this.nextEffect -= Time.deltaTime;

                    if (this.nextEffect < 0) {
                        this.Damage();
                        this.nextEffect = this.effectInterval;
                    }
                }/*
                else {
                    this.Damage();
                    this.alreadyUsed = true;
                    return;
                }
                */
            }

            if (this.remainingActive < 0) {

                this.Inactivate();
                return;
            }
            else {
                this.remainingActive -= Time.deltaTime;
            }

        }
    }

    private void ManageTransparency() {

        Material weaponMat = this.visualAttackor.material;
        Color weaponColor = weaponMat.color;
        weaponColor.a = 0.5f + 0.2f * MathF.Sin(this.remainingActive * 5);
        weaponMat.color = weaponColor;
    }

    public void ManageFormEffect() {

        if (this.form == GWFormType.PROJECTILE) {
            return;
        }

        switch (this.formEffect) {

            case GWFormEffect.VORTEX:
                this.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
                break;
            case GWFormEffect.KNOCKBACK:

                break;
            case GWFormEffect.STUN:

                foreach (GWEnemyController nearbyEnemy in this.nearbyEnemys) {
                    nearbyEnemy.agent.isStopped = true;

                    if (!nearbyEnemy.gameObject.GetComponent<GWStun>()) {
                        nearbyEnemy.gameObject.AddComponent<GWStun>();
                    }
                    else {
                        Destroy(nearbyEnemy.gameObject.GetComponent<GWStun>());
                        nearbyEnemy.gameObject.AddComponent<GWStun>();
                    }
                }
               
                break;

        }
    }

    public void Inactivate() {


        try {

            Material weaponMat = this.visualAttackor.material;
            Color weaponColor = weaponMat.color = this.spell.Color;
            weaponColor.a = 0;
            weaponMat.color = weaponColor;
        }
        catch (Exception e) {
            Debug.LogWarning(e.Message);
        }

        foreach (GWEnemyController enemy in this.nearbyEnemys) {
            enemy.agent.isStopped = false;
        }


        this.originalAttackor.upbildingAttackorClone = null;
        GameObject.Destroy(this.gameObject);
    }

    public virtual void Activate(GWInventorySlot inventorySlot) {

        this.upbildingAttackorClone = GameObject.Instantiate(this, GWPoolManager.instance.activeSpellPool).GetComponent<GWAttackor>();
        this.upbildingAttackorClone.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);



        this.upbildingAttackorClone.visualAttackor.material = new Material(this.visualAttackor.material.shader);
        this.upbildingAttackorClone.visualAttackor.material.CopyPropertiesFromMaterial(this.visualAttackor.material);

        this.upbildingAttackorClone.isSummoned = true;

        this.upbildingAttackorClone.correspondingInventorySlot = inventorySlot;

        this.upbildingAttackorClone.nextEffect = inventorySlot.uiSpell.spell.effectInterval;

        this.upbildingAttackorClone.onlyOneTimeEffect = inventorySlot.uiSpell.spellInstance.onlyOneTimeEffect;
        this.upbildingAttackorClone.effectInterval = inventorySlot.uiSpell.spellInstance.effectInterval;
        this.upbildingAttackorClone.remainingActive = inventorySlot.remainingActive;
        this.upbildingAttackorClone.spell = inventorySlot.uiSpell.spellInstance;

        this.upbildingAttackorClone.nearbyEnemys = this.nearbyEnemys;
        this.nearbyEnemys = new List<GWEnemyController>();

        this.upbildingAttackorClone.originalAttackor = this;


        try {

            this.upbildingAttackorClone.Damage();
        }
        catch (Exception e) {

        }

        this.upbildingAttackorClone = null;



    }


    private void Damage() {

        List<GWEnemyController> killedEnemys = new List<GWEnemyController>();

        Debug.Log("damage count: " + this.nearbyEnemys.Count + " enemys");

        foreach (GWEnemyController nearbyEnemy in this.nearbyEnemys) { //throws error "InvalidOperationException: Collection was modified; enumeration operation may not execute." when multiple enemies within collider

            try {
                nearbyEnemy.RecieveElementAttack(this.correspondingInventorySlot.Spell.containedElements);

                //Aus Testing: Slow wird immer applied!!!

                foreach (GWElementEffect effect in this.spell.effects) {
                    Debug.Log("effect: " + effect);
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

                //Apply Form Effect
                if (this.formEffect == GWFormEffect.KNOCKBACK) {

                    nearbyEnemy.rb.AddForce(
                        (
                        (nearbyEnemy.transform.position - GWPawnController.instance.transform.position
                        ).normalized 
                        + Vector3.up) * 10f, 
                        ForceMode.Impulse);
                    nearbyEnemy.agent.enabled = false;
                }

                if (nearbyEnemy.gameObject.GetComponent<GWEnemyStats>().currentHealth <= 0) {
                    killedEnemys.Add(nearbyEnemy);
                }
            }
            catch (Exception e) {
            }
        }


        foreach (GWEnemyController killedEnemy in killedEnemys) {

            GameObject.Destroy(killedEnemy);
            this.nearbyEnemys.Remove(killedEnemy);
        }

        /*

        foreach (GWL) {
            box.Destroy();
        }
        */

        if (this.onlyOneTimeEffect) {
            this.alreadyUsed = true;
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
