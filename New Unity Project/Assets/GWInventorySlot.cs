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



    public enum SpellState {
        READY, ACTIVE, COOLDOWN
    }


    public SpellState state = SpellState.READY;

    public KeyCode key;

    public Text cooldownDisplay;
    public Text activeDisplay;

    // TODO: Add this. 
    void Update() {
        switch (this.state) {
            case SpellState.READY:
                if (Input.GetKeyDown(key)) {
                    this.uiSpell.spellInstance.Activate();
                    this.state = SpellState.ACTIVE;
                    this.remainingActive = this.uiSpell.spellInstance.activeTime;
                }
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

    void Awake() {
        this.Init();
    }

    void Start() {
        this.Init();
    }
    void Init() {

        this.uiSpell = this.transform.GetComponentInChildren<GWUISpell>();

        if (!this.uiSpell) {
            return;
        }

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
            return;
        }

        droppedSpell.draggable.originInventorySlot.Reset();

        this.Combine(droppedSpell);


    }

    public void SetUISpell(GWUISpell otherSpell) {
        this.uiSpell = otherSpell;
        this.SetSpellTimes();
    }


    public void Combine(GWUISpell otherSpell) {

        if (this.uiSpell == null) {

            this.SetUISpell(otherSpell);

            otherSpell.GetComponent<GWDraggable>().originInventorySlot = this;

            return;
        }

        this.uiSpell.Combine(otherSpell);

    }

    public void Reset() {
        this.uiSpell = null;
    }

}
