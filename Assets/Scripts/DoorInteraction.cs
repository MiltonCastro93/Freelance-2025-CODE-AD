using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public GameObject puertaBloqueada; 
    public string mensaje = "Presioná E para abrir";
    public KeyCode teclaInteraccion = KeyCode.E;
    private bool enZona = false;

    void Update()
    {
        if (enZona && Input.GetKeyDown(teclaInteraccion))
        {
            puertaBloqueada.SetActive(false); 
            Debug.Log("Puerta abierta");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enZona = true;
            
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

