using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPuzzle : MonoBehaviour {
    public GameObject PointStart;
    public bool TaskCompleted = false, startTask = false;
    public int Lifes = 3;
    public bool restarPoint = false;
    public GameObject Flecha;
    public Rigidbody _rb;
    public float speed = 5f;
    public GameObject Door;

    void Update() {
        if (startTask) {
            if (!TaskCompleted) {
                if (restarPoint) {
                    Flecha.transform.position = PointStart.transform.position;
                    Flecha.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    restarPoint = false;
                }

                if (Lifes >= 1) {
                    if (Input.GetKeyDown(KeyCode.A)) {
                        Flecha.transform.Rotate(0, 0, 90f);
                    }
                    if (Input.GetKeyDown(KeyCode.D)) {
                        Flecha.transform.Rotate(0, 0, -90f);
                    }
                } else {
                    Flecha.SetActive(false);
                    Invoke("restarPuzzle", 4f);
                }
            } else {
                Flecha.SetActive(false);
            }
        }
        if (TaskCompleted) {
            Door.transform.position = new Vector3(-6.7f, 3f, -20.5f);
        }
    }

    private void FixedUpdate() {
        Vector2 direction = Flecha.transform.up;
        _rb.velocity = direction * speed;
    }

    private void restarPuzzle() {
        Flecha.SetActive(true);
        Lifes = 3;
    }

}
