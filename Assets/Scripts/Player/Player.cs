using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] SpriteRenderer body;
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
    }

    public bool Buy(int price) {
        
        if(Money >= price) {
            Money -= price;
            return true;
        }

        return false;
    }

    public void Sell(int Price) {
        Money += Price;
    }

    public void Equip(Equippable equippable) {
        body.sprite = equippable?.Sprite ?? defaultBody;
    }
}
