using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWAttackSlotContainer : MonoBehaviour
{
    public static GWAttackSlotContainer instance;

    public GWInventorySlot primaryAttack;
    public GWInventorySlot secondaryAttack;
    public GWInventorySlot primaryUtility;
    public GWInventorySlot secondaryUtility;


     
    void Start() {
        GWAttackSlotContainer.instance = this;
    }


    public static GWInventorySlot GetPressedAttackSlot() {


        if (GWAttackSlotContainer.instance.primaryAttack.isPressed) {
            return GWAttackSlotContainer.instance.primaryAttack;
        }
        if (GWAttackSlotContainer.instance.secondaryAttack.isPressed) {
            return GWAttackSlotContainer.instance.secondaryAttack;
        }

        if (GWAttackSlotContainer.instance.primaryUtility.isPressed) {
            return GWAttackSlotContainer.instance.primaryUtility;
        }

        if (GWAttackSlotContainer.instance.secondaryUtility.isPressed) {
            return GWAttackSlotContainer.instance.secondaryUtility;
        }

        return null;
    }
}
