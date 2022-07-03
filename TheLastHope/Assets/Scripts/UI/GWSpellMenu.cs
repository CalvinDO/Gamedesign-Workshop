using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWSpellMenu : MonoBehaviour {

    public GameObject hidableInventorySlots;
    public GameObject hotbarInventorySlots;

    public GameObject uiSpellPrefab;


    public static GWSpellMenu instance;
    public bool cardMovingBlocked;
    public AudioSource source;
    public GWCardSounds cardSounds;

    void Awake() {
        GWSpellMenu.instance = this;
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.I)) {

            this.hidableInventorySlots.gameObject.SetActive(!this.hidableInventorySlots.gameObject.activeSelf);
            GWPawnController.instance.isAttackingBlocked = !GWPawnController.instance.isAttackingBlocked;
        }
    }

    public void AddSpell(GWSpell spell) {

        try {
            this.FillInChildren(spell, this.hidableInventorySlots.transform);
            this.FillInChildren(spell, this.hotbarInventorySlots.transform);

            throw new Exception("no empty Inventory Slot found!");
        }
        catch (FilledException fe) {
            //Debug.Log(fe.Message);
        }
    }


    public void FillInChildren(GWSpell spell, Transform container) {

        foreach (Transform child in container) {

            GWInventorySlot inventorySlot = child.GetComponent<GWInventorySlot>();

            if (inventorySlot != null) {

                if (!inventorySlot.uiSpell) {

                    //Debug.Log(inventorySlot.name + " is empty and will be filled with " + spell.element);

                    inventorySlot.FillWithGeneratedUISpell(spell);

                    throw new FilledException();
                }
            }
        }
    }
}

public class FilledException : Exception {
    public FilledException() : base("Inventory slot successfully filled!") {
    }
}
