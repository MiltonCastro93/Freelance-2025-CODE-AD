using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [Header("Opcional")]
    public AudioClip alertSound;

    private AudioSource audioSource;
    private bool playerCaught = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerCaught) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Te atraparon!");

            if (alertSound != null && audioSource != null)
                audioSource.PlayOneShot(alertSound);

            playerCaught = true;

            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
                gm.Derrota();
            else
                Debug.LogWarning("No se encontró GameManager. No se pudo ejecutar Derrota().");
        }
    }
}


