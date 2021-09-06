using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    public UnityEvent<Player> Evt_Interacted = new UnityEvent<Player>();

    [SerializeField] Transform interactableShowPoint;
    GameObject interactableUI;

    private void Start() {
        interactableUI = SingletonManager.Get<UI>().InteractableUI;
    }

    public void ShowInteractableUI(bool show) {
        interactableUI.SetActive(show);

        interactableUI.transform.position = interactableShowPoint.position;
    }

    public void Interact(Player player) {
        interactableUI.SetActive(false);
        Evt_Interacted.Invoke(player);
    }
}
