using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUI : MonoBehaviour {

    public GameObject InteractableUI;

    private void Awake() {
        SingletonManager.Register(this);
    }
}
