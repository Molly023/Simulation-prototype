using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableDetection : MonoBehaviour {

    List<Interactable> interactables = new List<Interactable>();

    void OnTriggerEnter2D(Collider2D col) {

        Interactable tempInt;
        if (tempInt = col.GetComponent<Interactable>()) {

            interactables.Add(tempInt);
        }
    }

    private void OnTriggerExit2D(Collider2D col) {

        Interactable existingInteractable;
        if (existingInteractable = col.GetComponent<Interactable>()) {

            interactables.Remove(existingInteractable);
        }
    }

    public bool Contains(Interactable interactable) {
        return interactables.Contains(interactable);
    }
}
