using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;


public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IEndDragHandler,IDropHandler{

    protected Item itemData;
    public Item Item => itemData;

    public UnityEvent<Slot> Evt_RightClicked = new UnityEvent<Slot>();
    public UnityEvent<Slot> Evt_LeftClicked = new UnityEvent<Slot>();
    public UnityEvent<Slot> Evt_Dragged = new UnityEvent<Slot>();
    public UnityEvent Evt_EndDragged = new UnityEvent();
    public UnityEvent<Slot> Evt_Dropped = new UnityEvent<Slot>();

    [SerializeField] public Image ItemSprite;

    protected Tooltip tooltip;

    public enum SlotTooltipState {
        Description, Sale
    }

    public SlotTooltipState SlotState;

    private void Start() {
        tooltip = SingletonManager.Get<UI>().Tooltip;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if(SlotState == SlotTooltipState.Description) {
            ShowDefaultTooltip();
        }
        else if (SlotState == SlotTooltipState.Sale) {
            ShowSellTooltip();
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left)
            Evt_LeftClicked.Invoke(this);

        if (eventData.button == PointerEventData.InputButton.Right)
            Evt_RightClicked.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        HideTooltip();
    }

    public virtual void SetItem(Item item) {
        itemData = item;

        ItemSprite.sprite = itemData?.Sprite;
        ItemSprite.color = itemData ? Color.white : Color.clear;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Evt_EndDragged.Invoke();
    }

    public void OnDrag(PointerEventData eventData) {
        if (itemData != null && eventData.button == PointerEventData.InputButton.Left) {
            Evt_Dragged.Invoke(this);
            HideTooltip();
        }
    }

    public void OnDrop(PointerEventData eventData) {
        Evt_Dropped.Invoke(this);
    }

    public void ShowSellTooltip() {
        if(Item)
            tooltip.Show(Item.Name, "Sell For " + (Item.Price /2f).ToString("F1"));
    }

    public void ShowDefaultTooltip() {
        if(Item)
            tooltip.Show(Item.Name, Item.Description);
    }

    public void HideTooltip() {
        tooltip.Hide();
    }
}
