using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "Create Spell", menuName = "Spell")]

public enum GWEType
{
    WIND,EARTH,FIRE,WATER,SAND,IRON,STEAM,ICE,LIGHTNING,PLANT
}

public class GWSpell : ScriptableObject
{
    public new string name;
    public GWEType type;
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent) {}
    public virtual void BeginCooldown(GameObject parent){}
}
