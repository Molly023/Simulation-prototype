using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    public UnityEvent<int, int> Evt_Swapping = new UnityEvent<int, int>();
    public UnityEvent<int> Evt_Equip = new UnityEvent<int>();
    public UnityEvent Evt_Unequip = new UnityEvent();

    [SerializeField] List<Slot> itemSlots = null;
    public List<Slot> ItemSlots => itemSlots;
    Slot draggedSlot;

    public GameObject InventoryObject;
    Image draggedImage; 

    [SerializeField] EquippableSlot equippableSlot = null;

    void Start() {

        foreach (Slot slot in ItemSlots) {
            slot.Evt_Dragged.AddListener(Drag);
            slot.Evt_EndDragged.AddListener(EndDrag);
            slot.Evt_Dropped.AddListener(Drop);
            slot.Evt_RightClicked.AddListener(Equip);
        }

        equippableSlot?.Evt_Dragged.AddListener(Drag);
        equippableSlot?.Evt_EndDragged.AddListener(EndDrag);
        equippableSlot?.Evt_Dropped.AddListener(Drop);
        equippableSlot?.Evt_RightClicked.AddListener(Unequip);

        draggedImage = SingletonManager.Get<UI>().DraggedImage;
       
    }

    void Drag(Slot slot) {
        draggedSlot = slot;
        draggedImage.sprite = slot.ItemSprite.sprite;
        draggedImage.gameObject.SetActive(true);
        draggedImage.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, -10);
    }

    void Drop(Slot droppedSlot) {
        if (droppedSlot is EquippableSlot)
            Equip(droppedSlot);

        else if (draggedSlot is EquippableSlot)
            Unequip(droppedSlot);

        else if(draggedSlot is Slot)
            Evt_Swapping?.Invoke(ItemSlots.IndexOf(draggedSlot), ItemSlots.IndexOf(droppedSlot));
        
    }

    void EndDrag() {
  
        draggedSlot = null;
        draggedImage.sprite = null;
        draggedImage.gameObject.SetActive(false);
    }

    public void UpdateData(Inventory inventory) {

        for(int i = 0; i < inventory.Items.Length; i++) {
            ItemSlots[i].SetItem(inventory.Items[i]);
        }

        equippableSlot?.SetItem(inventory.EquippedItem);
    }

    public bool ToggleInventory() {
        InventoryObject.SetActive(!InventoryObject.activeSelf);
        return InventoryObject.activeSelf;
    }

    void Equip(Slot droppedEquippable) {

        if (draggedSlot) {
            if (draggedSlot.Item is Equippable)
                Evt_Equip.Invoke(ItemSlots.IndexOf(draggedSlot));
        }

        else if(droppedEquippable.Item is Equippable && equippableSlot) {
            Evt_Equip.Invoke(ItemSlots.IndexOf(droppedEquippable));
        }
    }

    void Unequip(Slot droppedSlot) {
        if(droppedSlot is EquippableSlot) {
            Evt_Unequip.Invoke();
        }
        else
            Evt_Equip?.Invoke(ItemSlots.IndexOf(droppedSlot));
    }

}
