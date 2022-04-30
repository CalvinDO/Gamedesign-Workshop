using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "Create Spell", menuName = "Spell")]

public enum GWEType
{
    AIR,EARTH,FIRE,WATER,SAND,IRON,STEAM,ICE,LIGHTNING,PLANT
}
//Class for combinations
//Include EVERYTHING in a ScriptableObject - Completely describable by parameters
public class GWSpell : ScriptableObject
{
    public new string name;
    public GWEType type;
    public float cooldownTime;
    public float activeTime;
    public void Activate(GameObject parent) {}
    public void BeginCooldown(GameObject parent){}
}
