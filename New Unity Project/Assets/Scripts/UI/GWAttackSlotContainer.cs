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


        if (Input.GetKey(GWAttackSlotContainer.instance.primaryAttack.key)) {
            return GWAttackSlotContainer.instance.primaryAttack;
        }
        if (Input.GetKey(GWAttackSlotContainer.instance.secondaryAttack.key)) {
            return GWAttackSlotContainer.instance.secondaryAttack;
        }

        if (Input.GetKey(GWAttackSlotContainer.instance.primaryUtility.key)) {
            return GWAttackSlotContainer.instance.primaryUtility;
        }

        if (Input.GetKey(GWAttackSlotContainer.instance.secondaryUtility.key)) {
            return GWAttackSlotContainer.instance.secondaryUtility;
        }

        return null;
    }
}
