using UnityEngine;

public class PuertaConTarjeta : MonoBehaviour
{
    public GameObject puertaBloqueada; 
    public string mensajeSinTarjeta = "Necesitás una tarjeta de acceso";
    public string mensajeConTarjeta = "Presioná E para abrir";
    public KeyCode teclaInteraccion = KeyCode.E;
    private bool enZona = false;

    void Update()
    {
        if (enZona && Input.GetKeyDown(teclaInteraccion))
        {
            if (TarjetaAcceso.tieneTarjeta)
            {
                puertaBloqueada.SetActive(false);
                Debug.Log("Puerta abierta con tarjeta");
            }
            else
            {
                Debug.Log(mensajeSinTarjeta);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enZona = true;
          
            if (TarjetaAcceso.tieneTarjeta)
                Debug.Log(mensajeConTarjeta);
            else
                Debug.Log(mensajeSinTarjeta);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enZona = false;
        }
    }
}

