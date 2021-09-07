using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [Header("OverlayUI")]
    public GameObject MoneyUI;
    public TextMeshProUGUI MoneyText;
    
    
    private void Awake() {
        SingletonManager.Register(this);
    }

    void Start() {
        CameraController cam = SingletonManager.Get<CameraController>();

        WorldCanvas.worldCamera = cam.uiCamera;
        CameraCanvas.worldCamera = cam.uiCamera;

        ShopUI.Evt_Opened += () => MoneyUI.SetActive(false);
        ShopUI.Evt_Closed += () => MoneyUI.SetActive(true);

    }

    public void SetMoneyText(int money) {
        MoneyText.text = money.ToString();
    }
}
