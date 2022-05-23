using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWSpellMenu : MonoBehaviour
{

    public GameObject hidableInventorySlots;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I)){

            this.hidableInventorySlots.gameObject.SetActive(!this.hidableInventorySlots.gameObject.activeSelf);
        }
    }
}
