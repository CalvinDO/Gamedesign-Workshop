using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GWUISpell : MonoBehaviour {


    public GWSpell spellInstance;


    public GWSpell spell;
    public Image image;
    public Image formImage;

    public GWDraggable draggable;

    public Text elementsDisplay;
    public Text formDisplay;



    public void Awake() {
        this.Init();
    }
    public void SetDataInGenerated(GWSpell spell) {
        this.spell = spell;

        this.Init();
    }

    public void Init() {

        Debug.Log("GWUISpell Init!");

        this.spellInstance = GameObject.Instantiate(this.spell);

        this.image.sprite = this.spellInstance.sprite;
        this.formImage.sprite = this.spellInstance.formSprite;

        //this.elementsDisplay.text = "";

        //this.spellInstance.containedElements.ForEach(element => this.elementsDisplay.text += element + "  ");

        this.spellInstance.element = this.spellInstance.containedElements[0];

        this.elementsDisplay.text = this.spellInstance.element + "";
        this.elementsDisplay.color = GWElementColorManager.instance.GetColor(this.spellInstance.element);
        Color color = this.elementsDisplay.color;
        color.a = 1;
        this.elementsDisplay.color = color;


        this.formDisplay.text = "- " + this.spellInstance.form + " -";
    }


    public void Combine(GWUISpell otherSpell) {

        if (otherSpell.spellInstance.element == this.spellInstance.element) {
            return;
        }

        try {
            this.spellInstance.element = GWCombinationManager.GetCombination(this.spellInstance.element, otherSpell.spellInstance.element);

            //otherSpell.spellInstance.containedElements.ForEach(element => this.elementsDisplay.text += element + "  ");
            this.spellInstance.containedElements.AddRange(otherSpell.spellInstance.containedElements);
        }
        catch (System.Exception e) {
            Debug.LogWarning(e.Message);
            return;
        }

        this.elementsDisplay.text = this.spellInstance.element + "";
        this.elementsDisplay.color = GWElementColorManager.instance.GetColor(this.spellInstance.element);
        Color color = this.elementsDisplay.color;
        color.a = 1;
        this.elementsDisplay.color = color;


        this.formDisplay.text = "- " + this.spellInstance.form + " -";


        otherSpell.draggable.originInventorySlot.Reset();


        GWElementEffect[] newEffectArray = new GWElementEffect[2];

        try {
            newEffectArray[0] = this.spellInstance.effects[0];
        }
        catch (Exception e) {
            newEffectArray[0] = otherSpell.spellInstance.effects[0];
        }

        try {
            newEffectArray[1] = otherSpell.spellInstance.effects[0];
        }
        catch (Exception e) {
            newEffectArray[1] = this.spellInstance.effects[0];
        }


        this.spellInstance.effects = newEffectArray;


        GameObject.Destroy(otherSpell.gameObject);


    }
}
