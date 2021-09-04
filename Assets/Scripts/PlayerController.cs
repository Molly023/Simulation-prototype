using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Player Control;
    public CameraController MainCamera;
    Interactable hoveredInteractable;
    WorldUI showableUI;

    void Awake() {
        showableUI = SingletonManager.Get<WorldUI>();
    }

    void Start() {
        StartCoroutine(RaycastInteractables());
        
        
    }

    void FixedUpdate() {

        Control.Movement.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        
    }

    private void LateUpdate() {
        MainCamera.FollowPlayer(Control.transform);
    }

    void Update() {

        if (Input.GetButtonDown("Interact")) {
            hoveredInteractable?.Interact();
        }

        Debug.Log(hoveredInteractable);

    }

    IEnumerator RaycastInteractables() {

        while (true) {

            //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
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