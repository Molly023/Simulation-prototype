using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShopUI : MonoBehaviour {

    public Action<Item> Evt_BoughtItem;
    public Action Evt_Closed;

    public RectTransform content;

    public SellSlot SellSlotPrefab;
    List<SellSlot> SellSlots = new List<SellSlot>();
    Tooltip tooltip;

    private void Start() {
        tooltip = SingletonManager.Get<WorldUI>().Tooltip;
    }

    public void OpenShop(List<Item> itemsSelling) {
        gameObject.SetActive(true);
        for (int i = SellSlots.Count; i < itemsSelling.Count; i++) {
            
            SellSlots.Add(Instantiate(SellSlotPrefab, content));
            
            SellSlots[i].Evt_Hovered.AddListener(ShowTooltip);
            SellSlots[i].Evt_Exited.AddListener(HideTooltip);
           // SellSlots[i].Evt_RightClickBought
            //SellSlots[i].Evt_RightClicked.AddListener()
        }

        for (int i = 0; i < itemsSelling.Count; i++) {

            SellSlots[i].SetItem(itemsSelling[i]);
        }

        for(int i = itemsSelling.Count;  i < SellSlots.Count; i++) {

            SellSlots[i].gameObject.SetActive(false);
        }
    }

  
    public void BoughtItem(Item item) {

    }

    public void HideTooltip() {

        tooltip.Hide();
    }

    void ShowTooltip(Item item) { 
        tooltip.Show(item.Name, item.Description);
    }

    public void CloseShop() {
        Evt_Closed?.Invoke();
    }
}
