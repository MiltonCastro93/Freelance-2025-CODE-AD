using UnityEngine;

public class TarjetaAcceso : MonoBehaviour
{
    public static bool tieneTarjeta = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tieneTarjeta = true;
            Debug.Log("Tarjeta de acceso obtenida");
            gameObject.SetActive(false); 
        }
    }
}

