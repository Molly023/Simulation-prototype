using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

    public Action<int> Evt_MoneyChange;

    [SerializeField] SpriteRenderer body = null;
    Sprite defaultBody;

    public InteractableDetection InteractableInRange;
    public Movement Movement { get; private set; }
    public Inventory Inventory { get; private set; }

    public int Money;

    private void Start() {
        Movement = GetComponent<Movement>();
        Inventory = GetComponent<Inventory>();

        Inventory.Evt_OnEquip += Equip;
        defaultBody = body.sprite;

        UI ui = SingletonManager.Get<UI>();
        Evt_MoneyChange += ui.SetMoneyText;
        ui.SetMoneyText(Money);
    }

    public bool Buy(int price) {
        
        if(Money >= price) {
            Money -= price;
            Evt_MoneyChange?.Invoke(Money);
            return true;
        }
        
        return false;
    }

    public void Sell(int Price) {
        Money += Price;
        Evt_MoneyChange?.Invoke(Money);
    }

    public void Equip(Equippable equippable) {
        body.sprite = equippable?.Sprite ?? defaultBody;
    }
}
