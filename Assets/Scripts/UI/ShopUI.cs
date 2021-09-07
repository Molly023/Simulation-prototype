using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ShopUI : MonoBehaviour {

    public Action<Item> Evt_BoughtItem;
    public Action<Item, int> Evt_BoughtItemWithIndex;
    public Action Evt_Closed;
    public Action Evt_Opened;
    public Action<int> Evt_SellItem;

    public RectTransform content;
    public TextMeshProUGUI moneyText;

    public SellSlot SellSlotPrefab;
    List<SellSlot> SellSlots = new List<SellSlot>();
    Tooltip tooltip;
    Image draggedImage;
    Slot draggedSlot;

    public InventoryUI InventoryUI;

    private void Start() {
        UI ui = SingletonManager.Get<UI>();
        tooltip = ui.Tooltip;
        draggedImage = ui.DraggedImage;

        foreach (Slot slot in InventoryUI.ItemSlots) {
            slot.Evt_RightClicked.AddListener(SellItem);
            slot.Evt_Dropped.AddListener(Drop);
        }
    }

    public void OpenShop(List<Item> itemsSelling) {
        
        gameObject.SetActive(true);

        for (int i = SellSlots.Count; i < itemsSelling.Count; i++) {

            SellSlots.Add(Instantiate(SellSlotPrefab, content));
            SellSlots[i].Evt_RightClicked.AddListener(BoughtItem);
            SellSlots[i].Evt_Dragged.AddListener(Drag);
            SellSlots[i].Evt_EndDragged.AddListener(EndDrag);
            
        }

        for (int i = 0; i < itemsSelling.Count; i++) {

            SellSlots[i].SetItem(itemsSelling[i]);
        }

        for(int i = itemsSelling.Count;  i < SellSlots.Count; i++) {

            SellSlots[i].gameObject.SetActive(false);
        }

        Evt_Opened?.Invoke();
    }

  
    void BoughtItem(Slot itemSlot) {
        Evt_BoughtItem?.Invoke(itemSlot.Item);

    }

    public void CloseShop() {
        Evt_Closed?.Invoke();
        HideTooltip();
    }

    public void HideTooltip() {

        tooltip.Hide();
    }

    public void SetPlayerMoney(int money) {
        moneyText.text = money.ToString();
    }

    public void ShowPlayerMoneyText(bool show) {


        moneyText.gameObject.SetActive(show);
    }

    void SellItem(Slot itemSlot) {
        Evt_SellItem?.Invoke(InventoryUI.ItemSlots.IndexOf(itemSlot));
    }

    public void UpdateInventory(Inventory inventory) {
        InventoryUI.UpdateData(inventory);
    }

    void Drag(Slot slot) {
        draggedSlot = slot;
        draggedImage.sprite = slot.ItemSprite.sprite;
        draggedImage.gameObject.SetActive(true);
        draggedImage.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, -10);
    }

    void Drop(Slot droppedSlot) {
        if(draggedSlot is SellSlot) {
            Evt_BoughtItemWithIndex?.Invoke(draggedSlot.Item, InventoryUI.ItemSlots.IndexOf(droppedSlot));
        }

    }

    void EndDrag() {

        draggedSlot = null;
        draggedImage.sprite = null;
        draggedImage.gameObject.SetActive(false);
    }

}
