using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWCollectableSpell : GWCollectable {

    public GWSpell spell;
    public MeshRenderer visualMeshRenderer;
    public Material defaultMaterial;


    void OnDrawGizmos() {
        this.SetColor();
    }

    void SetColor() {

        Material mat = this.defaultMaterial;
        mat.color = this.spell.Color;
        Color color = mat.color;
        color.a = 0.25f;
        mat.color = color;
        this.visualMeshRenderer.material = mat;
    }

    void Start() {
        this.SetColor();
    }


    public override void PickUp() {

        //Debug.Log("picked up " + this.spell.name);

        try {

        GWSpellMenu.instance.AddSpell(this.spell);
        GWSpellMenu.instance.cardSounds.PlayPickUp(GWSpellMenu.instance.source);
        }
        catch (System.Exception e) {
            Debug.LogWarning(e.Message);

            return;
        }

        base.PickUp();
    }
}
