using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjecutedPuzzle : MonoBehaviour
{
    public GameObject EventPuzzle;
    public Camera cam1;
    public GameObject DesActivar;
    public CameraLookMe camLook;
    private Vector3 oldPos;
    private Quaternion oldRot;
    private Vector3 newPos = new Vector3(-8.38f, -22.1f, -48f);

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            if (Input.GetButtonDown("Interaccion")) {
                EventPuzzle.GetComponent<EventPuzzle>().startTask = true;
                other.GetComponent<PlayerController>().isOcuped = true;
                DesActivar.SetActive(false);
                camLook.enabled = false;
                if (oldPos == Vector3.zero) {
                    oldPos = cam1.transform.position;
                    oldRot = cam1.transform.rotation;
                    cam1.transform.position = newPos;
                    cam1.transform.rotation = Quaternion.identity;
                }
            }
            if (Input.GetButtonDown("Cancel") || EventPuzzle.GetComponent<EventPuzzle>().TaskCompleted) {
                EventPuzzle.GetComponent<EventPuzzle>().startTask = false;
                other.GetComponent<PlayerController>().isOcuped = false;
                if(oldPos != Vector3.zero) {
                    cam1.transform.position = oldPos;
                    cam1.transform.rotation = oldRot;
                    oldPos = Vector3.zero;
                    oldRot = Quaternion.identity;
                    DesActivar.SetActive(true);
                    camLook.enabled = true;
                }

            }
        }
    }

}
