using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWElementColorManager : MonoBehaviour {
    public GWElementColorTable colorTable;

    public static GWElementColorManager instance;

    void OnDrawGizmos() {
        this.Awake();
    }

    void Awake() {
        GWElementColorManager.instance = this;
    }
    public Color GetColor(GWEType elementType) {
        return this.colorTable.color[(int)elementType];
    }
}
