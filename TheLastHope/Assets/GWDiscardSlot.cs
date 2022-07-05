using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GWDiscardSlot : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData) {

        GWUISpell droppedSpell = eventData.pointerDrag.gameObject.GetComponent<GWUISpell>();

        float healthGain = droppedSpell.spellInstance.containedElements.Count;
        healthGain *= 40;

        GWPawnController.instance.stats.currentHealth += healthGain;

        if (GWPawnController.instance.stats.currentHealth > GWPawnController.instance.stats.maxHealth) {
            GWPawnController.instance.stats.currentHealth = GWPawnController.instance.stats.maxHealth;
        }


        droppedSpell.draggable.originInventorySlot.Reset();

        GameObject.Destroy(droppedSpell.gameObject);
    }
}
