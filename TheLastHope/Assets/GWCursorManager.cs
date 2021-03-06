using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GWCursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    void OnMouseEnter() {
        Cursor.SetCursor(cursorTexture, Input.mousePosition, cursorMode);
    }

    void OnMouseExit() {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
