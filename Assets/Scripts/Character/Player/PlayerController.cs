using UnityEngine;
using System.Collections;
using System;

public enum ControlState {
    Player, Shop, Dialogue, Inventory
}

public class PlayerController : MonoBehaviour {

    public Action Evt_CloseShopUI;
    public Action Evt_Next;
    public Player Control;
    CameraController mainCamera;
    
    Interactable hoveredInteractable;
    
    GameObject showableUI;
   
    ControlState currentState = ControlState.Player;
    public ControlState State {
        get => currentState;
        set {
            currentState = value;
        }
    }

    void Awake() {

        showableUI = SingletonManager.Get<UI>().InteractableUI;
        SingletonManager.Register(this);
        mainCamera = SingletonManager.Get<CameraController>();
    }

    void Start() {
        StartCoroutine(RaycastInteractables());
    }

    void FixedUpdate() {

        if (currentState == ControlState.Player) {
            Control.Movement.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

        }
    }

    private void LateUpdate() {
        mainCamera.FollowPlayer(Control.transform);
    }

    void Update() {

        if (Input.GetButtonDown("Interact") && currentState == ControlState.Player) {
            hoveredInteractable?.Interact(Control);
        }

        if (Input.GetButtonDown("Cancel")) {
            if(currentState == ControlState.Player) {
                Control.Inventory.ToggleInventory();
                currentState = ControlState.Inventory;
            }
            else if(currentState == ControlState.Inventory) {
                Control.Inventory.ToggleInventory();
                currentState = ControlState.Player;
            }
            else if(currentState == ControlState.Shop) {
                Evt_CloseShopUI?.Invoke();
            }
        }

        if (State == ControlState.Dialogue) {
            if (Input.GetButtonDown("Next")) {
                
                Evt_Next?.Invoke();
            }
        }

    }

    IEnumerator RaycastInteractables() {

        while (true) {

            if (currentState != ControlState.Player) {
                yield return null;
                continue;
            }

            Physics2D.queriesHitTriggers = false;
            Collider2D col = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            if (col) {
                if (hoveredInteractable = col.GetComponent<Interactable>()) {

                    if (!Control.InteractableInRange.Contains(hoveredInteractable)) {
                        hoveredInteractable = null;
                    }

                }

            }
            else {
                hoveredInteractable = null;
            }

            if (hoveredInteractable) 
                hoveredInteractable.ShowInteractableUI(true);
            else 
                showableUI.SetActive(false);


            yield return new WaitForSeconds(.1f);
        }
    }
}