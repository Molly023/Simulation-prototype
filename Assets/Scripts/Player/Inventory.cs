﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour {

    public Action<Equippable> Evt_OnEquip;
    public Item[] Items { get; private set; }
    [SerializeField] int itemsCanStore;

    public Equippable EquippedItem { get; private set; }

    InventoryUI inventoryUI;

    private void Start() {
        
        if (Items == null)
            Items = new Item[itemsCanStore];
    }

    public bool AddItem(Item itemToAdd) {
        
        for(int i = 0; i < Items.Length; i++) {

            if (!Items[i]) {

                Items[i] = itemToAdd;
                return true;
            }
        }

        inventoryUI?.UpdateData(this);

        return false;
    }

    public bool Remove(Item itemToRemove) {
        if (!Contains(itemToRemove)) return false;

        RemoveItem(itemToRemove);
        inventoryUI?.UpdateData(this);
        return true;
    }

    public void SwapItems(int index1, int index2) {

        Item tempItem = Items[index1];
        Items[index1] = Items[index2];
        Items[index2] = tempItem;

        inventoryUI?.UpdateData(this);
    }

    public bool Contains(Item item) {
        
        for(int i = 0; i < Items.Length; i++) {
            if (item.Id == Items[i].Id) 
                return true;
        }
        return false;
    }

    public void RemoveItem(Item item) {
        
        for (int i = 0; i < Items.Length; i++) {
            if (item.Id == Items[i].Id) {
                Items[i] = null;
                inventoryUI?.UpdateData(this);
                return;
            }
        }
    }

    public void RemoveItem(int index) {
        Items[index] = null;
    }

    public void Equip(int index) {

        Equippable temp = Items[index] as Equippable ;
        Items[index] = EquippedItem;
        EquippedItem = temp;

        Evt_OnEquip?.Invoke(EquippedItem);
        inventoryUI?.UpdateData(this);
    }

    public bool ToggleInventory() {
        if (!inventoryUI)
            SetInventoryUI();

        bool toggled = inventoryUI.ToggleInventory();
        inventoryUI.UpdateData(this);
        return toggled;
    }

    void SetInventoryUI() {
        inventoryUI = SingletonManager.Get<UI>().InventoryUI;
        inventoryUI.Evt_Swapping.AddListener(SwapItems);
        inventoryUI.Evt_Equip.AddListener(Equip);
    }
}
