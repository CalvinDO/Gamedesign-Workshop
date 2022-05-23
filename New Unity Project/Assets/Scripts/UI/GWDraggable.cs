using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GWDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    private RectTransform rectTransform;
    public GWInventorySlot originInventorySlot;


    void Awake() {
        this.originInventorySlot = this.transform.parent.GetComponent<GWInventorySlot>();
        this.rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData) {

        //this.transform.SetParent(GWAttackSlotContainer.instance.transform);


        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {

        this.transform.SetParent(this.originInventorySlot.transform);
        this.rectTransform.anchoredPosition = Vector2.zero;

        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
