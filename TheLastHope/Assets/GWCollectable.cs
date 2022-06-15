using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWCollectable : MonoBehaviour
{
    

    void OnTriggerEnter() {

        this.PickUp();
    }

    public virtual void PickUp() {
        GameObject.Destroy(this.gameObject);
    }
}
