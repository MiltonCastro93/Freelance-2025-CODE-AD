using UnityEngine;

public class PuertaConLlave : MonoBehaviour
{
    public GameObject puertaBloqueada;
    public string mensajeSinLlave = "Necesitás una llave";
    public string mensajeConLlave = "Presioná E para abrir";
    public KeyCode teclaInteraccion = KeyCode.E;
    private bool enZona = false;

    void Update()
    {
        if (enZona && Input.GetKeyDown(teclaInteraccion))
        {
            if (LlaveAcceso.tieneLlave)
            {
                puertaBloqueada.SetActive(false);
                Debug.Log("Puerta abierta con llave");
            }
            else
            {
                Debug.Log(mensajeSinLlave);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enZona = true;
            if (LlaveAcceso.tieneLlave)
                Debug.Log(mensajeConLlave);
            else
                Debug.Log(mensajeSinLlave);
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

