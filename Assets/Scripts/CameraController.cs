using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    new Camera camera;
    public float Speed;
    [SerializeField] Vector2 cameraMinViewportThreshold;
    [SerializeField] Vector2 cameraMaxViewportThreshold;

    public Camera uiCamera;

    private void Awake() {
        SingletonManager.Register(this);
    }

    private void Start() {
        
        camera = Camera.main;
        
    }

    public void FollowPlayer(Transform player) {
        ////Debug.Log(camera.WorldToViewportPoint(transform.position).x < cameraMinViewportThreshold.x);
        //if (camera.WorldToViewportPoint(player.transform.position).x < cameraMinViewportThreshold.x || camera.WorldToViewportPoint(player.transform.position).y < cameraMinViewportThreshold.y ||
        //                camera.WorldToViewportPoint(player.transform.position).x > cameraMaxViewportThreshold.x || camera.WorldToViewportPoint(player.transform.position).y > cameraMaxViewportThreshold.y) {

        //    camera.transform.Translate(player.Movement.Direction * Time.deltaTime * Speed);
        //    Debug.Log("Hello");
        //}

        camera.transform.position = player.position - new Vector3(0,0,10);
    }


}
