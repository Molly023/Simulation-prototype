using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    Rigidbody2D rb2d;
    [SerializeField] float speed = 500;
    public Vector2 Direction=> rb2d.velocity.normalized;

    private void Start() {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 direction) {

        //if (direction.x != 0) {
        //    direction.y = 0;
        //}

        //bool isMoving = direction.x != 0 || direction.y != 0;
        //if (anim) {
        //    if (isMoving) {
        //        anim?.SetFloat("Horizontal", direction.x);
        //        anim?.SetFloat("Vertical", direction.y);
        //    }
        //    anim?.SetBool("isMoving", isMoving);
        //}
        rb2d.velocity = direction * speed * Time.deltaTime;

    }
}
