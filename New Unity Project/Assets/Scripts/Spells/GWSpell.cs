using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//[CreateAssetMenu(fileName = "Create Spell"= , menuName = "Spell")]

public enum GWEType {
    AIR= 0, EARTH= 1, FIRE= 2, WATER= 3, SAND= 4, IRON= 5, STEAM= 6, ICE= 7, LIGHTNING= 8, PLANT = 9
}

//Class for combinations
//Include EVERYTHING in a ScriptableObject - Completely describable by parameters
[CreateAssetMenu]
public class GWSpell : ScriptableObject {

    public GWForm form;
    public GWEType element;
    public List<GWEType> containedElements;
    public float cooldownTime;
    public float activeTime;

    public Sprite sprite;

    

    public void Activate() { Debug.Log(this.element + " Spell activated!"); }
    public void BeginCooldown() { }
    /*
    public GWEType GetCombined(GWEType first, GWEType second) {

        switch (first) {
            case GWEType.AIR:
                break;
            case GWEType.EARTH:
                break;
            case GWEType.FIRE:
                break;
        }
    }
    */
}
