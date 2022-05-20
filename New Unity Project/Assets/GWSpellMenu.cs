using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWSpellMenu : MonoBehaviour
{

    public GWAttackSlotContainer spellMenuContainer;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I)){

            this.spellMenuContainer.gameObject.SetActive(!this.spellMenuContainer.gameObject.activeSelf);
        }
    }
}
