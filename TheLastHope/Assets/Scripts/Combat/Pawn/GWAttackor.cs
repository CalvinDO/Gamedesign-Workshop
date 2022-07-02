using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GWControlType {
    ROTATION = 0, CLICKCAST = 1, CLICKCENTERED = 2, SWIPECAST = 3, ROTATIONSCROLLER = 4
}

public enum GWFormType {
    BALL = 0, TORNADO = 1, PUSH = 2, QUAKE = 3, DISTRIBUTION = 4, SPIKES = 5, SHOOT_UP = 6
}

public enum GWFormEffect {
    VORTEX = 0, KNOCKBACK = 1, STUN = 3
}

public class GWAttackor : MonoBehaviour {

    private bool isPressingAttack;
    private bool isPressingHeal;
    public List<GWEnemyController> nearbyEnemys;
    public List<GWLoot> lootBoxes;
    public MeshRenderer visualAttackor;
    public SkinnedMeshRenderer skinnedVisualAttackor;


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

    public AudioSource source;

    public float ProgressFactor {
        get { return (this.correspondingInventorySlot.Spell.activeTime - this.remainingActive) / this.correspondingInventorySlot.Spell.activeTime; }
    }


    void Awake() {

        //Debug.Log("attackor awake");


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
        /*
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
        */

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

        Material weaponMat;

        if (this.skinnedVisualAttackor) {
            weaponMat = this.skinnedVisualAttackor.material;
        }
        else {
            weaponMat = this.visualAttackor.material;
        }

        Color weaponColor = weaponMat.color;
        weaponColor.a = 0.5f + 0.2f * MathF.Sin(this.remainingActive * 5);
        weaponMat.color = weaponColor;
    }

    public void ManageFormEffect() {

        switch (this.formEffect) {

            case GWFormEffect.VORTEX:
                this.transform.Rotate(Vector3.up * 100 * Time.deltaTime);

                foreach (GWEnemyController nearbyEnemy in this.nearbyEnemys) {

                    //nearbyEnemy.agent.isStopped = true;
                    try {


                        nearbyEnemy.isFlying = true;

                        nearbyEnemy.transform.parent = this.transform;
                        nearbyEnemy.transform.position = new Vector3(nearbyEnemy.transform.position.x, 10 * this.ProgressFactor, nearbyEnemy.transform.position.z);
                    }
                    catch (Exception e) {

                    }
                }



                break;
            case GWFormEffect.KNOCKBACK:

                break;
            case GWFormEffect.STUN:

                foreach (GWEnemyController nearbyEnemy in this.nearbyEnemys) {

                    try {

                        if (!nearbyEnemy.gameObject.GetComponent<GWStun>()) {
                            nearbyEnemy.gameObject.AddComponent<GWStun>();
                        }
                        else {
                            Destroy(nearbyEnemy.gameObject.GetComponent<GWStun>());
                            nearbyEnemy.gameObject.AddComponent<GWStun>();
                        }

                        nearbyEnemy.agent.isStopped = true;
                    }
                    catch (Exception e) {
                        this.nearbyEnemys.Remove(nearbyEnemy);
                    }


                }

                break;

        }
    }

    public void Inactivate() {


        try {

            Material weaponMat;

            if (this.skinnedVisualAttackor) {
                weaponMat = this.skinnedVisualAttackor.material;
            }
            else {
                weaponMat = this.visualAttackor.material;
            }

            Color weaponColor = weaponMat.color = this.spell.Color;
            weaponColor.a = 0;
            weaponMat.color = weaponColor;

        }
        catch (Exception e) {
            Debug.LogWarning(e.Message);
        }

        foreach (GWEnemyController enemy in this.nearbyEnemys) {

            try {

                enemy.agent.isStopped = false;
                enemy.transform.parent = null;
            }
            catch (Exception e) {
                this.nearbyEnemys.Remove(enemy);
            }
        }


        this.originalAttackor.upbildingAttackorClone = null;
        GameObject.Destroy(this.gameObject);
    }

    public virtual void Activate(GWInventorySlot inventorySlot) {

        if(this.spell.element==GWEType.FIRE || this.spell.element==GWEType.WATER || this.spell.element==GWEType.EARTH || this.spell.element==GWEType.AIR){
            GWElementSounds.instance.PlayElement(this.source, this.spell.element, 0);
        }

        this.upbildingAttackorClone = GameObject.Instantiate(this, GWPoolManager.instance.activeSpellPool).GetComponent<GWAttackor>();
        this.upbildingAttackorClone.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);


        if (this.skinnedVisualAttackor) {

            this.upbildingAttackorClone.skinnedVisualAttackor.material = new Material(this.skinnedVisualAttackor.material.shader);
            this.upbildingAttackorClone.skinnedVisualAttackor.material.CopyPropertiesFromMaterial(this.skinnedVisualAttackor.material);
        }
        else {
            this.upbildingAttackorClone.visualAttackor.material = new Material(this.visualAttackor.material.shader);
            this.upbildingAttackorClone.visualAttackor.material.CopyPropertiesFromMaterial(this.visualAttackor.material);
        }




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

        foreach (GWLoot loot in this.lootBoxes) {
            try {

                loot.destroy();
            }
            catch (Exception e) {

            }
        }

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


                    Vector3 knockback =
                        (
                        (nearbyEnemy.transform.position - GWPawnController.instance.transform.position
                        ).normalized
                        + Vector3.up) * 1f;
                    nearbyEnemy.rb.velocity = knockback * 5;
                    /*
                    nearbyEnemy.rb.AddForce(
                        (
                        (nearbyEnemy.transform.position - GWPawnController.instance.transform.position
                        ).normalized
                        + Vector3.up) * 1f,
                        ForceMode.Impulse);

                    */

                    nearbyEnemy.isFlying = true;
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

        try {
            if (!this.lootBoxes.Contains(other.gameObject.GetComponent<GWLoot>())) {
                this.lootBoxes.Add(other.gameObject.GetComponent<GWLoot>());
            }


            if (!this.nearbyEnemys.Contains(other.gameObject.GetComponent<GWEnemyController>())) {
                this.nearbyEnemys.Add(other.gameObject.GetComponent<GWEnemyController>());
            }

        }
        catch (Exception e) {
            Debug.LogWarning("OnTriggerSay Attackor Exception: " + e.Message);
        }
    }

    void OnTriggerExit(Collider other) {

        try {

            GWEnemyController otherEnemyController = other.gameObject.GetComponent<GWEnemyController>();

            if (this.nearbyEnemys.Contains(otherEnemyController)) {
                this.nearbyEnemys.Remove(otherEnemyController);
            }

            if (this.lootBoxes.Contains(other.gameObject.GetComponent<GWLoot>())) {
                this.lootBoxes.Remove(other.gameObject.GetComponent<GWLoot>());
                return;
            }
        }
        catch (Exception e) {

        }
    }
}
