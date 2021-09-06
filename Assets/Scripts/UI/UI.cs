using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    [Header("World UI")]
    public Canvas WorldCanvas;
    public GameObject InteractableUI;
    public Tooltip Tooltip;
    public Image DraggedImage;

    [Header("Camera UI")]
    public Canvas CameraCanvas;
    public ShopUI ShopUI;
    public InventoryUI InventoryUI;
   
    
    
    
    private void Awake() {
        SingletonManager.Register(this);
    }

    void Start() {
        CameraController cam = SingletonManager.Get<CameraController>();

        WorldCanvas.worldCamera = cam.uiCamera;
        CameraCanvas.worldCamera = cam.uiCamera;
    }
}
