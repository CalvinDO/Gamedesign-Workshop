using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GWUISpell : MonoBehaviour {

    
    public GWSpell spellInstance;


    public GWSpell spell;
    public Image image;
    public GWDraggable draggable;

    public Text elementsDisplay;
    public Text resultDisplay;

    public void SetDataInGenerated(GWSpell spell) {
        this.spell = spell;

        this.Init();
    }

    public void Init() {

        Debug.Log("GWUISpell Init!");

        this.spellInstance = GameObject.Instantiate(this.spell);

        this.image.sprite = this.spell.sprite;
        this.elementsDisplay.text = "";
        this.spellInstance.containedElements.ForEach(element => this.elementsDisplay.text += element + "  ");
        this.spellInstance.element = this.spellInstance.containedElements[0];
        this.resultDisplay.text = this.spellInstance.element + "";
    }


    public void Combine(GWUISpell otherSpell) {

        if (otherSpell.spellInstance.element == this.spellInstance.element) {
            return;
        }

        try {
            this.spellInstance.element = GWCombinationManager.GetCombination(this.spellInstance.element, otherSpell.spellInstance.element);

            otherSpell.spellInstance.containedElements.ForEach(element => this.elementsDisplay.text += element + "  ");
            this.spellInstance.containedElements.AddRange(otherSpell.spellInstance.containedElements);
        }
        catch (System.Exception e) {
            Debug.LogWarning(e.Message);
            return;
        }

        this.resultDisplay.text = this.spellInstance.element + "";

        otherSpell.draggable.originInventorySlot.Reset();


        GameObject.Destroy(otherSpell.gameObject);
    }
}
