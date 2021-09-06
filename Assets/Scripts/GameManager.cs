using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private void Awake() {
        SingletonManager.Register(this);
    }

    private void Start() {
        StartCoroutine(Utilities.LoadAdditiveSceneAsync("StartingScene"));
    }

    public void StartGame() {
        StartCoroutine(Utilities.LoadAdditiveSceneAsync("LoadingScene"));
        StartCoroutine(Utilities.UnloadSceneAsync("StartingScene"));
        StartCoroutine(Utilities.LoadMultipleAdditiveSceneAsync(new string[] { "UI", "GameScene" }, () => StartCoroutine(Utilities.UnloadSceneAsync("LoadingScene"))));
    }
}
