using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWHurtSounds : MonoBehaviour {

    public void SetMovementBlocked(int value) {
        GWPawnController.instance.isMovementBlocked = value != 0;
    }
}
