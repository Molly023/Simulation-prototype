using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    new Camera camera;
    public float Speed;
    public Camera uiCamera;

    private void Awake() {
        SingletonManager.Register(this);
    }

    private void Start() {
        
        camera = Camera.main;
        
    }

    public void FollowPlayer(Transform player) {

        camera.transform.position = player.position - new Vector3(0,0,10);
    }


}
