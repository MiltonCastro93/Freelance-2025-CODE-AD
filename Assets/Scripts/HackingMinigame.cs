using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HackingMinigame : MonoBehaviour
{
    public GameObject hackUI;
    private bool playerNearby;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            hackUI.SetActive(true);
            Debug.Log("Iniciando hackeo...");
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerNearby = false;
    }
}

