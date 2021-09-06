using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenUI : MonoBehaviour {

    public Button startButton;

    private void Start() {
        GameManager gameManager = SingletonManager.Get<GameManager>();

        startButton.onClick.AddListener(gameManager.StartGame);
    }
}
