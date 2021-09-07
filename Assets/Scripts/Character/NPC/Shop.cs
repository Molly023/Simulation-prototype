using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shop : MonoBehaviour {

    public Action Evt_Open;
    public Action Evt_Close;
    public List<Item> ItemsToSell;

    ShopUI shopUI;

    Coroutine moneyCoroutine;
    
    Player playerInShop;
    Inventory playerInventory => playerInShop.Inventory;

    private void Start() {
        
        PlayerController pController= SingletonManager.Get<PlayerController>();
        Evt_Open += () => pController.State = ControlState.Shop;
        Evt_Close += () => pController.State = ControlState.Player;
        
    }

    public void OpenShop(Player player) {
     
        Utilities.Pause();

        if (!shopUI)
            SetShopUI();

        playerInShop = player;
        shopUI.OpenShop(ItemsToSell);
        shopUI.SetPlayerMoney(player.Money);
        shopUI.UpdateInventory(playerInventory);
        Evt_Open?.Invoke();
    }

    public void CloseShop() {
        shopUI.gameObject.SetActive(false);
        shopUI.HideTooltip();
        //playerController.State = ControlState.Player;
        playerInShop = null;
        Utilities.Resume();
        Evt_Close?.Invoke();
    }

    #region ShopFunctionalities
    void Buy(Item item) {

        if (!playerInShop.Buy(item.Price)) {
            if (moneyCoroutine != null)
                StopCoroutine(moneyCoroutine);

            moneyCoroutine = StartCoroutine(FlickerMoney());
            return;
        }


        if (playerInventory.AddItem(item)) {

            shopUI.SetPlayerMoney(playerInShop.Money);

            shopUI.InventoryUI.UpdateData(playerInventory);
        }
        else playerInShop.Sell(playerInShop.Money);
        
    }

    void Buy(Item item, int index) {

        if (!playerInShop.Buy(item.Price)) {
            if (moneyCoroutine != null)
                StopCoroutine(moneyCoroutine);

            moneyCoroutine = StartCoroutine(FlickerMoney());
            return;
        }

        if (playerInventory.AddItem(item, index)) {
            shopUI.SetPlayerMoney(playerInShop.Money);

            shopUI.InventoryUI.UpdateData(playerInventory);
        }
        else playerInShop.Sell(playerInShop.Money);

    }



    void Sell(int index) {

        playerInShop.Sell(playerInventory.Items[index].Price / 2);
        playerInventory.Items[index] = null;

        shopUI.SetPlayerMoney(playerInShop.Money);
        shopUI.InventoryUI.UpdateData(playerInventory);
    }

    void RearrangeInventory(int index1, int index2){
        playerInventory.SwapItems(index1, index2);
        shopUI.InventoryUI.UpdateData(playerInventory);

    }
#endregion


    #region Auxilliary

    void SetShopUI() {
        shopUI = SingletonManager.Get<UI>().ShopUI;
        shopUI.Evt_Closed += CloseShop;
        shopUI.Evt_BoughtItem += Buy;
        shopUI.Evt_SellItem += Sell;
        shopUI.InventoryUI.Evt_Swapping.AddListener(RearrangeInventory);
        shopUI.Evt_BoughtItemWithIndex += Buy;

        PlayerController pController = SingletonManager.Get<PlayerController>();
        pController.Evt_CloseShopUI += shopUI.CloseShop;
    }

    IEnumerator FlickerMoney() {

        for (int i = 0; i < 3; i++) {
            shopUI.ShowPlayerMoneyText(false);
            yield return new WaitForSecondsRealtime(.1f);
            shopUI.ShowPlayerMoneyText(true);
            yield return new WaitForSecondsRealtime(.1f);
        }

        shopUI.ShowPlayerMoneyText(true);
        moneyCoroutine = null;
    }
    #endregion
}
