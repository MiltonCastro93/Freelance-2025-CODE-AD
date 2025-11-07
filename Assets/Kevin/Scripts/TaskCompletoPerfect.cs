using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCompletoPerfect : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Flecha")) {
            GetComponentInParent<EventPuzzle>().TaskCompleted = true;
        }

    }

}
