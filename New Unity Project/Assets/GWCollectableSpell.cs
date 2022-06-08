using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWCollectableSpell : GWCollectable {
    public GWSpell spell;
    public MeshRenderer visualMeshRenderer;



    void OnDrawGizmos() {
        this.SetColor();
    }

    void SetColor() {

        Material mat = this.visualMeshRenderer.material;
        mat.color = this.spell.color;
    }

    void Start() {
        this.SetColor();
    }


    public override void PickUp() {

        Debug.Log("picked up " + this.spell.name);

        GWSpellMenu.instance.AddSpell(this.spell);

        base.PickUp();
    }

}
