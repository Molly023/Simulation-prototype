using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class SellSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Item itemData;
    public UnityEvent<Item> Evt_Hovered = new UnityEvent<Item>();
    public UnityEvent Evt_Exited = new UnityEvent();
    public UnityEvent<Item> Evt_RightClicked = new UnityEvent<Item>();
    public UnityEvent<Item> Evt_LeftClicked = new UnityEvent<Item>();

    [SerializeField] public Image ItemSprite;
    [SerializeField] public TextMeshProUGUI ItemName;
    [SerializeField] public TextMeshProUGUI ItemPrice;

    public void OnPointerEnter(PointerEventData eventData) {
        Evt_Hovered.Invoke(itemData);
        Debug.Log("Hovered");
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(eventData.button == PointerEventData.InputButton.Left)
            Evt_LeftClicked.Invoke(itemData);

        if (eventData.button == PointerEventData.InputButton.Right)
            Evt_RightClicked.Invoke(itemData);
    }

    public void OnPointerExit(PointerEventData eventData) {
        Evt_Exited.Invoke();
    }
    
    public void SetItem(Item item) {
        itemData = item;

        ItemSprite.sprite = itemData.Sprite;
        ItemName.text = itemData.Name;
        ItemPrice.text = itemData.Price.ToString();
    }
}
