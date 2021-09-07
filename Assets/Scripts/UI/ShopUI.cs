using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ShopUI : MonoBehaviour {

    public Action<Item> Evt_BoughtItem;
    public Action Evt_Closed;
    public Action Evt_Opened;
    public Action<int> Evt_SellItem;

    public RectTransform content;
    public TextMeshProUGUI moneyText;

    public SellSlot SellSlotPrefab;
    List<SellSlot> SellSlots = new List<SellSlot>();
    Tooltip tooltip;
    Image draggableImage;
    public InventoryUI InventoryUI;

    private void Start() {
        UI ui = SingletonManager.Get<UI>();
        tooltip = ui.Tooltip;
        draggableImage = ui.DraggedImage;

        foreach (Slot slot in InventoryUI.ItemSlots) {
            slot.Evt_RightClicked.AddListener(SellItem);
        }
    }

    public void OpenShop(List<Item> itemsSelling) {
        
        gameObject.SetActive(true);

        for (int i = SellSlots.Count; i < itemsSelling.Count; i++) {
            
            SellSlots.Add(Instantiate(SellSlotPrefab, content));
            
            SellSlots[i].Evt_RightClicked.AddListener(BoughtItem);
            //SellSlots[i].Evt_RightClicked.AddListener()
        }

        for (int i = 0; i < itemsSelling.Count; i++) {

            SellSlots[i].SetItem(itemsSelling[i]);
        }

        for(int i = itemsSelling.Count;  i < SellSlots.Count; i++) {

            SellSlots[i].gameObject.SetActive(false);
        }

        Evt_Opened?.Invoke();
    }

  
    public void BoughtItem(Slot itemSlot) {
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

    public void SellItem(Slot itemSlot) {
        Evt_SellItem?.Invoke(InventoryUI.ItemSlots.IndexOf(itemSlot));
    }

    public void UpdateInventory(Inventory inventory) {
        InventoryUI.UpdateData(inventory);
    }
}
