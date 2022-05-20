using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GWUISpell : MonoBehaviour {

    [HideInInspector]
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
        this.spell.containedElements.ForEach(element => this.elementsDisplay.text += element + "  ");
        this.spell.element = this.spell.containedElements[0];
    }
    public void Combine(GWUISpell otherSpell) {

        otherSpell.spellInstance.containedElements.ForEach(element => this.elementsDisplay.text += element + "  ");


        this.spellInstance.containedElements.AddRange(otherSpell.spellInstance.containedElements);
        this.spellInstance.element = GWCombinationManager.GetCombination(this.spellInstance.element, otherSpell.spellInstance.element);

        this.resultDisplay.text = this.spellInstance.element + "";

        GameObject.Destroy(otherSpell.gameObject);
    }
}
