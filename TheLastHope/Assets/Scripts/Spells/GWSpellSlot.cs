using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWSpellSlot : MonoBehaviour
{

    /*
    public GWSpell spell;
    public float cooldownTime;
    float activeTime;
    public enum SpellState {
        READY, ACTIVE, COOLDOWN
    }
    SpellState state = SpellState.READY;

    public KeyCode key;

    // TODO: Add this.
    void Update()
    {
        switch (state)
        {
            case SpellState.READY:
                if(Input.GetKeyDown(key)) {
                    spell.Activate(gameObject);
                    state = SpellState.ACTIVE;
                    activeTime = spell.activeTime;
                }
            break;
            case SpellState.ACTIVE:
                if(activeTime>0){
                    activeTime -= Time.deltaTime;
                }
                else {
                    spell.BeginCooldown(gameObject);
                    state = SpellState.COOLDOWN;
                    cooldownTime = spell.activeTime;
                }
            break;
            case SpellState.COOLDOWN:
                if(activeTime>0){
                    activeTime -= Time.deltaTime;
                }
                else {
                    state = SpellState.READY;
                }
            break;
        }
    }
    */
}
