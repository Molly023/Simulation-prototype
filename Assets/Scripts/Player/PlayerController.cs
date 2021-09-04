using UnityEngine;
using System.Collections;

public enum ControlState {
    Player, Shop, Dialogue
}

public class PlayerController : MonoBehaviour {

    public Player Control;
    public CameraController MainCamera;
    Interactable hoveredInteractable;
    WorldUI showableUI;

   
    ControlState currentState = ControlState.Player;
    public ControlState State {
        set {
            currentState = value;
        }
    }

    void Awake() {
        showableUI = SingletonManager.Get<WorldUI>();
        SingletonManager.Register(this);
    }

    void Start() {
        StartCoroutine(RaycastInteractables());
    }

    void FixedUpdate() {

        if (currentState == ControlState.Player) {
            Control.Movement.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

        }

        Debug.Log(currentState);
    }

    private void LateUpdate() {
        MainCamera.FollowPlayer(Control.transform);
    }

    void Update() {

        if (Input.GetButtonDown("Interact") && currentState == ControlState.Player) {
            hoveredInteractable?.Interact();
        }

    }

    IEnumerator RaycastInteractables() {

        while (true) {

            if (currentState != ControlState.Player) yield return new WaitForSeconds(.1f);

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
                showableUI.InteractableUI.SetActive(false);


            yield return new WaitForSeconds(.1f);
        }
    }
}