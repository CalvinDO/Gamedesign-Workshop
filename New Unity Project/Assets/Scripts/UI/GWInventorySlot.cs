using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GWInventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler {


    public GWUISpell uiSpell;

    private float remainingCooldown;
    private float remainingActive;


    public  GWSpell Spell {
        get { return this.uiSpell.spellInstance; }
    }


    public enum SpellState {
        READY, ACTIVE, COOLDOWN
    }


    public SpellState state = SpellState.READY;

    public KeyCode key;
    public bool isPressed = false;


    public Text cooldownDisplay;
    public Text activeDisplay;

    // TODO: Add this. 


    void Update() {

        /*
        if (Input.GetKeyDown(this.key)) {
            this.isPressed = true;
        }
        else {
            this.isPressed = false;
        }
        */

        switch (this.state) {
            case SpellState.READY:
                
                break;
            case SpellState.ACTIVE:
                if (this.remainingActive > 0) {
                    this.remainingActive -= Time.deltaTime;
                    this.activeDisplay.text = "" + this.remainingActive;
                }
                else {
                    //spell.BeginCooldown(gameObject);
                    this.state = SpellState.COOLDOWN;
                    this.remainingCooldown = this.uiSpell.spellInstance.cooldownTime;
                }
                break;
            case SpellState.COOLDOWN:
                Debug.Log("update GWInventorySlot COOLDOWN State");
                if (this.remainingCooldown > 0) {
                    this.remainingCooldown -= Time.deltaTime;
                    this.cooldownDisplay.text = "" + this.remainingCooldown;

                }
                else {
                    this.state = SpellState.READY;
                }
                break;
        }
    }


    public void SwitchToActive() {
        if (Input.GetKeyDown(this.key)) {
            this.uiSpell.spellInstance.Activate();
            this.state = SpellState.ACTIVE;
            this.remainingActive = this.uiSpell.spellInstance.activeTime;
        }
    }

    void Start() {
        this.Init();
    }

    void Init() {

        this.uiSpell = this.transform.GetComponentInChildren<GWUISpell>();
        


        if (!this.uiSpell) {
            return;
        }

        this.uiSpell.Init();
        this.SetSpellTimes();
    }

    public void SetSpellTimes() {
        this.remainingCooldown = this.uiSpell.spellInstance.cooldownTime;
        this.remainingActive = this.uiSpell.spellInstance.activeTime;
    }

    public void OnPointerEnter(PointerEventData eventData) {

    }
    public void OnPointerExit(PointerEventData eventData) {

    }
    public void OnDrop(PointerEventData eventData) {

        GWUISpell droppedSpell = eventData.pointerDrag.gameObject.GetComponent<GWUISpell>();
        Debug.Log(droppedSpell);

        if (droppedSpell.draggable.originInventorySlot == this.transform) {
            droppedSpell.draggable.originInventorySlot.SetUISpell(droppedSpell);
            return;
        }


        this.Combine(droppedSpell);


    }

    public void SetUISpell(GWUISpell otherSpell) {
        this.uiSpell = otherSpell;
        this.SetSpellTimes();
    }


    public void Combine(GWUISpell otherSpell) {

        if (this.uiSpell == null) {

            this.SetUISpell(otherSpell);

            otherSpell.draggable.originInventorySlot.Reset();

            otherSpell.draggable.originInventorySlot = this;

            return;
        }

        this.uiSpell.Combine(otherSpell);

    }

    public void Reset() {
        this.uiSpell = null;
    }

}
