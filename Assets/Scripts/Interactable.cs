using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {

    public UnityEvent Evt_Interacted = new UnityEvent();

    [SerializeField] Transform interactableShowPoint;
    GameObject interactableUI;

    private void Start() {
        interactableUI = SingletonManager.Get<WorldUI>().InteractableUI;
    }

    public void ShowInteractableUI(bool show) {
        interactableUI.SetActive(show);

        interactableUI.transform.position = interactableShowPoint.position;
    }

    public void Interact() {
        interactableUI.SetActive(false);
        Evt_Interacted.Invoke();
    }
}
