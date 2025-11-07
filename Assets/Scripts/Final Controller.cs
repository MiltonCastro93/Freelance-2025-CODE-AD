using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalController : MonoBehaviour
{
    public AudioSource audioFinal;         
    public GameObject panelOpciones;       
    private bool finalActivado = false;

    void Start()
    {
        if (panelOpciones != null)
            panelOpciones.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!finalActivado && other.CompareTag("Player"))
        {
            finalActivado = true;

            if (audioFinal != null && audioFinal.clip != null)
            {
                audioFinal.Play();
                Invoke(nameof(ActivarOpciones), audioFinal.clip.length);
            }
            else
            {
               
                Invoke(nameof(ActivarOpciones), 2f);
            }
        }
    }

    void ActivarOpciones()
    {
        if (panelOpciones != null)
            panelOpciones.SetActive(true);
    }

    public void FinalA()
    {
        Debug.Log("Final A: señal apagada");
        SceneManager.LoadScene("FinalA");
    }

    public void FinalB()
    {
        Debug.Log("Final B: señal sigue activa");
        SceneManager.LoadScene("FinalB"); 
    }
}

