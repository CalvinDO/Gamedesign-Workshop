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



    public void Init() {

        Debug.Log("GWUISpell Init!");

        this.image.sprite = this.spell.sprite;
        this.spellInstance = GameObject.Instantiate(this.spell);
        this.elementsDisplay.text = "";
        this.spell.containedElements.ForEach(element => this.elementsDisplay.text += element + "  ");
        this.spell.element = this.spell.containedElements[0];
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
