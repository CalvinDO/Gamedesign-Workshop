using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GWInventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler {


    public GWUISpell uiSpell;

    private float remainingCooldown;
    public float remainingActive;


    public GWSpell Spell {
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

    public void FillWithGeneratedUISpell(GWSpell spell) {

        this.uiSpell = GameObject.Instantiate(GWSpellMenu.instance.uiSpellPrefab, this.transform).GetComponent<GWUISpell>();
        this.uiSpell.SetDataInGenerated(spell);

        this.SetSpellTimes();
    }

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

                float factor =  this.remainingActive / this.Spell.activeTime;
                this.uiSpell.overlay.fillAmount = factor;

                break;
            case SpellState.COOLDOWN:
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

        this.uiSpell.spellInstance.Activate();
        this.state = SpellState.ACTIVE;
        this.remainingActive = this.uiSpell.spellInstance.activeTime;
    }

    public void AbortAttack() {

        this.state = GWInventorySlot.SpellState.READY;

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
