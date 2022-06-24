using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWPawnMeshRootZeroSetter : MonoBehaviour {
    public bool stop;


    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (this.stop) {
            return;
        }
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
    }
}
