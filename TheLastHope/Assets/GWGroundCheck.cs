using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWGroundCheck : MonoBehaviour {
    public GWEnemyController enemyController;


    // Update is called once per frame
    void Update() {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        RaycastHit hit;

        int layerMask = LayerMask.GetMask("Street");


        if (Physics.Raycast(ray, out hit, 0.1f, layerMask)) {
            this.enemyController.isGrounded = true;
            this.enemyController.transform.position = new Vector3(this.enemyController.transform.position.x, 0, this.enemyController.transform.position.z);

        }
        else {
            this.enemyController.isGrounded = false;
        }
    }
}
