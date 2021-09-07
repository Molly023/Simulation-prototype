using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour {

    Movement movement;
    public float Speed = 150f;
    public float WanderLength = 1f;
    public float WaitBeforeWander = 10f;

    void Start()
    {
        if (!(movement = GetComponent<Movement>())) {
            movement = gameObject.AddComponent<Movement>();
            movement.Speed = Speed;
        }

        StartCoroutine(Utilities.LoadEndOfFrame(() => StartCoroutine(WanderCoroutine())));
    }

    IEnumerator WanderCoroutine() {
        
        while (true) {
            yield return new WaitForSeconds(WaitBeforeWander);
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            StartCoroutine(Move(randomDirection));
            
        }
    }

    IEnumerator Move(Vector2 direction) {
        float timer = 0;

        while (timer < WanderLength) {
            movement.Move(direction);
            timer += Time.deltaTime;
            yield return null;
        }
        
        movement.Move(Vector2.zero);

    }

}
