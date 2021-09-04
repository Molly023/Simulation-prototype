using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    SpriteRenderer body;

    public InteractableDetection InteractableInRange;
    public Movement Movement { get; private set; }
    public Inventory Inventory { get; private set; }

    public int Money;

    private void Start() {
        Movement = GetComponent<Movement>();
        Inventory = GetComponent<Inventory>();
    }
}
