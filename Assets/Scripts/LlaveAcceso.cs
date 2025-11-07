using UnityEngine;

public class LlaveAcceso : MonoBehaviour
{
    public static bool tieneLlave = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tieneLlave = true;
            Debug.Log("🔑 Llave obtenida");
            gameObject.SetActive(false);
        }
    }
}

