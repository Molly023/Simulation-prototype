using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class SellSlot : Slot {
    
    [SerializeField] public TextMeshProUGUI ItemName;
    [SerializeField] public TextMeshProUGUI ItemPrice;

    public override void SetItem(Item item) {

        base.SetItem(item);

        ItemName.text = itemData.Name;
        ItemPrice.text = itemData.Price.ToString();
    }
}
