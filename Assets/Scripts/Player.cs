using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    SpriteRenderer head;
    SpriteRenderer body;

    public InteractableDetection InteractableInRange;
    [System.NonSerialized] public Movement Movement;

    private void Start() {
        Movement = GetComponent<Movement>();
    }
}
