using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//[CreateAssetMenu(fileName = "Create Spell"= , menuName = "Spell")]

public enum GWEType {
    EARTH = 0, FIRE = 1, WATER = 2, AIR = 3, SAND = 4, IRON = 5, STEAM = 6, ICE = 7, LIGHTNING = 8, PLANT = 9
}

public enum GWElementEffect {
    SLOW = 0, BURNING = 1, DISARMED = 2
}

//Class for combinations
//Include EVERYTHING in a ScriptableObject - Completely describable by parameters
[CreateAssetMenu]
public class GWSpell : ScriptableObject {

    public GWEType element;
    public List<GWEType> containedElements;

    public float buildUpTime;
    public float activeTime;
    public float cooldownTime;

    public bool onlyOneTimeEffect;
    public float effectInterval;

    public Sprite sprite;
    public Sprite formSprite;


    public GWElementEffect[] effects;


    public GWFormType form;

    public Color Color {
        get {
            //GWElementColorManager.instance.Awake();
            return GWElementColorManager.instance.GetColor(this.element); }
    }



    public void Activate() { /*Debug.Log(this.element + " Spell activated! Name: " + this.name); */ }
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
