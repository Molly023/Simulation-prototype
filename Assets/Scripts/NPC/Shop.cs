using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public List<Item> ItemsToSell;

    public ShopUI ShopUI;
    PlayerController playerController;

    private void Start() {
        ShopUI.Evt_Closed += CloseShop;
        playerController = SingletonManager.Get<PlayerController>();
        
    }

    public void OpenShop() {
        Debug.Log("OpenShop");
        ShopUI.OpenShop(ItemsToSell);
        Utilities.Pause();
        playerController.State = ControlState.Shop;
    }

    public void CloseShop() {
        ShopUI.gameObject.SetActive(false);
        playerController.State = ControlState.Player;
        Utilities.Resume();
    }
}
