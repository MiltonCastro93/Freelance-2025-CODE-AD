using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetivoFinal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().Victoria();
            gameObject.SetActive(false);
        }
    }
}

