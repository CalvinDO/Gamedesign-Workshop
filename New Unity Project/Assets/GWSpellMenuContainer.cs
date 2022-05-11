using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWSpellMenuContainer : MonoBehaviour
{
    public static GWSpellMenuContainer instance;

    public GWInventorySlot primaryAttack;
    public GWInventorySlot secondaryAttack;
    public GWInventorySlot primaryUtility;
    public GWInventorySlot secondaryUtility;



    void Awake() {
        GWSpellMenuContainer.instance = this;
    }
}
