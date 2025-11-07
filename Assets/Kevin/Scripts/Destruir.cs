using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruir : MonoBehaviour
{
    EventPuzzle manager;

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Flecha")) {
            manager = GetComponentInParent<EventPuzzle>();
            manager.Lifes--;
            manager.restarPoint = true;
        }

    }

}
