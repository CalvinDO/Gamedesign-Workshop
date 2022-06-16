using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWTargetedAttackor : GWAttackor {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    public override void Update() {
        base.Update();

        if (this.isSummoned) {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int layerMask = LayerMask.GetMask("Default");


        if (Physics.Raycast(ray, out hit, 1000, layerMask)) {

            //draw invisible ray cast/vector
            // Debug.DrawLine(ray.origin, hit.point);
            //log hit area to the console
            //Debug.Log(hit.point);

            this.transform.position = hit.point;
        }
    }

}

