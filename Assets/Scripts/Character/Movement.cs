using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    Rigidbody2D rb2d;
    public float Speed = 250;
    public Vector2 Direction=> rb2d.velocity.normalized;

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 direction) {

        rb2d.velocity = direction * Speed * Time.deltaTime;
    }
}
